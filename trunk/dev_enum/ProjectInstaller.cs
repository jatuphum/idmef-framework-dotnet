using System.ComponentModel;
using System.Configuration.Install;

namespace dev_enum
{
	[RunInstaller(true)]
	public partial class ProjectInstaller: Installer
	{
		public ProjectInstaller()
		{
			InitializeComponent();
		}
	}
}