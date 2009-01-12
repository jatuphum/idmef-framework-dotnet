using System.ServiceProcess;
using System.Threading;

namespace dev_enum
{
	public partial class device_enumerator: ServiceBase
	{
		public device_enumerator()
		{
			InitializeComponent();
			DeviceChangeWatcher deviceChangeWatcher = new DeviceChangeWatcher();
			watcher = new Thread(new ThreadStart(deviceChangeWatcher.Watch));
		}

		protected override void OnStart(string[] args)
		{
			watcher.Start();
		}

		protected override void OnStop()
		{
			DeviceChangeWatcher.isActive = false;
			watcher.Join();
		}

		private Thread watcher;
	}
}