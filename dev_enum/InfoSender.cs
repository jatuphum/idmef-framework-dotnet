using System;
using System.IO;
using System.Xml;

namespace dev_enum
{
	internal class InfoSender
	{
		public static void SendAdded(XmlDocument doc)
		{
			StreamWriter log = File.AppendText(string
				.Format("{0}dev_enum-{1}-added.xml", temp, GetFname()));
			log.WriteLine(doc.OuterXml);
			log.Close();
		}
		public static void SendRemoved(XmlDocument doc)
		{
			StreamWriter log = File.AppendText(string
				.Format("{0}dev_enum-{1}-removed.xml", temp, GetFname()));
			log.WriteLine(doc.OuterXml);
			log.Close();
		}
		public static void SendModified(XmlDocument doc)
		{
			StreamWriter log = File.AppendText(string
				.Format("{0}dev_enum-{1}-modified.xml", temp, GetFname()));
			log.WriteLine(doc.OuterXml);
			log.Close();
		}
		public static void SendHeartbeat(XmlDocument doc)
		{
			StreamWriter log = File.AppendText(string
				.Format("{0}dev_enum-{1}-heartbeat.xml", temp, GetFname()));
			log.WriteLine(doc.OuterXml);
			log.Close();
		}

		private static string temp = Path.GetTempPath();
		private static string GetFname()
		{
			return DateTime.Now.ToString("yyyy.MM.dd_HH.mm.ss,fffff");
		}
	}
}