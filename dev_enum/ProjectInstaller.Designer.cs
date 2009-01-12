using System.ComponentModel;
using System.ServiceProcess;

namespace dev_enum
{
	partial class ProjectInstaller
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.dev_enum_process_installer = new System.ServiceProcess.ServiceProcessInstaller();
			this.dev_enum_svc_installer = new System.ServiceProcess.ServiceInstaller();
			// 
			// dev_enum_process_installer
			// 
			this.dev_enum_process_installer.Account = System.ServiceProcess.ServiceAccount.NetworkService;
			this.dev_enum_process_installer.Password = null;
			this.dev_enum_process_installer.Username = null;
			// 
			// dev_enum_svc_installer
			// 
			this.dev_enum_svc_installer.Description = "Device change analyzer";
			this.dev_enum_svc_installer.DisplayName = "Device Enumerator";
			this.dev_enum_svc_installer.ServiceName = "device_enumerator";
			this.dev_enum_svc_installer.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
			// 
			// ProjectInstaller
			// 
			this.Installers.AddRange(new System.Configuration.Install.Installer[]
			                         	{
			                         		this.dev_enum_process_installer,
			                         		this.dev_enum_svc_installer
			                         	});
		}

		#endregion

		private ServiceProcessInstaller dev_enum_process_installer;
		private ServiceInstaller dev_enum_svc_installer;
	}
}