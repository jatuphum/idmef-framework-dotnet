using System;
using System.Collections.Generic;
using System.IO;
using System.Management;
using System.Threading;
using idmef;
using Process=System.Diagnostics.Process;

namespace dev_enum
{
	internal class DeviceChangeWatcher
	{
		public static bool isActive = true;
		private List<Win32_PNPEntity_Wrap> deviceList;
		private Timer heartBeatingTimer;

		#region IDMEF classes

		private static readonly Node localhost = new Node(Environment.MachineName);

		private readonly Analyzer analyzer = new Analyzer(
			localhost,
			new idmef.Process(
				Process.GetCurrentProcess().MainModule.ModuleName,
				Process.GetCurrentProcess().Id,
				Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName),
				null,
				null,
				Guid.NewGuid().ToString()
				),
			null,
			Guid.NewGuid().ToString(),
			"Device Enumerator",
			"13xforever",
			"Windows service (using .NET Framework)",
			Process.GetCurrentProcess().MainModule.FileVersionInfo.FileVersion,
			"service/information",
			Environment.OSVersion.Platform.ToString(),
			Environment.OSVersion.VersionString
			);

		private readonly Source[] source = new[]
		                                   	{
		                                   		new Source(
		                                   			localhost,
		                                   			new User(
		                                   				new UserId(
		                                   					Environment.UserName,
		                                   					null,
		                                   					Guid.NewGuid().ToString(),
		                                   					UserIdTypeEnum.currentUser,
		                                   					null
		                                   					),
		                                   				Guid.NewGuid().ToString(),
		                                   				UserCategoryEnum.osDevice
		                                   				),
		                                   			null,
		                                   			null,
		                                   			Guid.NewGuid().ToString(),
		                                   			UynEnum.unknown,
		                                   			null
		                                   			)
		                                   	};

		#endregion

		public void Watch()
		{
			#region Loading saved state and configure analyzer

			//todo: Configuration using LDAP
			//load previous state
			deviceList = BuildDeviceList();

			var heartBeater = new HeartBeater(5*60, analyzer);
			TimerCallback timerCallback = heartBeater.SendHeartBeat;
			heartBeatingTimer = new Timer(timerCallback, null, 0, heartBeater.heartBeatInterval*1000);

			#endregion

			#region setting up event watcher to receive device change notifications

			var eventWatcher = new ManagementEventWatcher
			                   	{
			                   		// vista bug: http://forums.microsoft.com/MSDN/ShowPost.aspx?PostID=835398&SiteID=1
			                   		//Query = new WqlEventQuery("Win32_DeviceChangeEvent");
			                   		Query = new WqlEventQuery("Win32_SystemConfigurationChangeEvent")
			                   	};
			eventWatcher.EventArrived += DeviceChangeEventHandler;
			eventWatcher.Start();

			#endregion

			while (isActive) Thread.Sleep(10000);

			heartBeatingTimer.Dispose();
			eventWatcher.Stop();

			//save state
		}

		public void DeviceChangeEventHandler(object obj, EventArrivedEventArgs args)
		{
			if (!isActive) return;

			List<Win32_PNPEntity_Wrap> newDeviceList = BuildDeviceList();
			LookForDifferences(newDeviceList, deviceList);
			deviceList = newDeviceList;
		}

		private static List<Win32_PNPEntity_Wrap> BuildDeviceList()
		{
			var mos = new ManagementObjectSearcher("select * from Win32_PnPEntity");
			ManagementObjectCollection moc = mos.Get();
			var state = new List<Win32_PNPEntity_Wrap>();
			foreach (ManagementObject mo in moc) state.Add(new Win32_PNPEntity_Wrap(mo));
			state.Sort();
			return state;
		}

		private void LookForDifferences(IList<Win32_PNPEntity_Wrap> newList, IList<Win32_PNPEntity_Wrap> oldList)
		{
			int newDeviceIndex = 0;
			int oldDeviceIndex = 0;

			do
			{
				int relation = newList[newDeviceIndex].CompareTo(oldList[oldDeviceIndex]);
				if (relation == 0)
				{
					if (!newList[newDeviceIndex].Equals(oldList[oldDeviceIndex])) SendDeviceChangedInfo(newList[newDeviceIndex]);
					newDeviceIndex++;
					oldDeviceIndex++;
				}
				else if (relation < 0)
				{
					SendDeviceAddedInfo(newList[newDeviceIndex]);
					newDeviceIndex++;
				}
				else
				{
					SendDeviceRemovedInfo(oldList[oldDeviceIndex]);
					oldDeviceIndex++;
				}
			} while ((newDeviceIndex < newList.Count) && (oldDeviceIndex < oldList.Count));
			while (newDeviceIndex < newList.Count) SendDeviceAddedInfo(newList[newDeviceIndex++]);
			while (oldDeviceIndex < oldList.Count) SendDeviceRemovedInfo(oldList[oldDeviceIndex++]);
		}

		private void SendDeviceAddedInfo(Win32_PNPEntity_Wrap device)
		{
			var m = new IdmefMessage(new Alert(
			                         	analyzer,
			                         	new Classification((Reference)null, Guid.NewGuid().ToString(), "Hardware connection"),
			                         	new DetectTime(),
			                         	new AnalyzerTime(),
			                         	source,
			                         	null,
			                         	null,
			                         	new[] {new AdditionalData("Device information", device.ToXml())},
			                         	Guid.NewGuid().ToString()
			                         	));
			m.alert[0].source[0].user.userId[0].name = (new Microsoft.VisualBasic.ApplicationServices.User()).Name;
			InfoSender.SendAdded(m.ToXml());
		}

		private void SendDeviceRemovedInfo(Win32_PNPEntity_Wrap device)
		{
			var m = new IdmefMessage(new Alert(
			                         	analyzer,
			                         	new Classification(
			                         		(Reference)null,
			                         		Guid.NewGuid().ToString(),
			                         		"Hardware disconnection"
			                         		),
			                         	new DetectTime(),
			                         	new AnalyzerTime(),
			                         	source,
			                         	null,
			                         	null,
			                         	new[]
			                         		{
			                         			new AdditionalData(
			                         				"Device information",
			                         				device.ToXml()
			                         				)
			                         		},
			                         	Guid.NewGuid().ToString()
			                         	));
			m.alert[0].source[0].user.userId[0].name = (new Microsoft.VisualBasic.ApplicationServices.User()).Name;
			InfoSender.SendRemoved(m.ToXml());
		}

		private void SendDeviceChangedInfo(Win32_PNPEntity_Wrap device)
		{
			var m = new IdmefMessage(new Alert(
			                         	analyzer,
			                         	new Classification(
			                         		(Reference)null,
			                         		Guid.NewGuid().ToString(),
			                         		"Hardware configuration change"
			                         		),
			                         	new DetectTime(),
			                         	new AnalyzerTime(),
			                         	source,
			                         	null,
			                         	null,
			                         	new[]
			                         		{
			                         			new AdditionalData(
			                         				"Device information",
			                         				device.ToXml()
			                         				)
			                         		},
			                         	Guid.NewGuid().ToString()
			                         	));
			m.alert[0].source[0].user.userId[0].name = (new Microsoft.VisualBasic.ApplicationServices.User()).Name;
			InfoSender.SendModified(m.ToXml());
		}
	}
}