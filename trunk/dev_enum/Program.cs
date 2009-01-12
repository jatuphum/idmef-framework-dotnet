using System.ServiceProcess;

namespace dev_enum
{
	internal static class Program
	{
		private static void Main()
		{
			ServiceBase[] ServicesToRun;

			ServicesToRun = new ServiceBase[] {new device_enumerator()};

			ServiceBase.Run(ServicesToRun);
		}
	}
}