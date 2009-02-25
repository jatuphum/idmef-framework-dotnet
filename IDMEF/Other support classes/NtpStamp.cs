using System;
using System.Xml;

namespace idmef
{
	public class NtpStamp
	{
		private readonly UInt32 fraction;
		private readonly UInt32 integer;

		public NtpStamp(): this(DateTime.Now)
		{
		}

		public NtpStamp(DateTime dateTime)
		{
			long time = dateTime.ToUniversalTime().Ticks;
			long root = DateTime.SpecifyKind(new DateTime(1900, 1, 1), DateTimeKind.Utc).Ticks;
			const long tps = TimeSpan.TicksPerSecond;

			long stamp = time - root;
			if (root > time) stamp = -stamp;

			integer = (UInt32)(stamp/tps);
			stamp %= tps;
			fraction = (UInt32)(stamp/(decimal)tps*0x100000000L);
		}

		public static NtpStamp Convert(DateTime dateTime)
		{
			return new NtpStamp(dateTime);
		}

		public override string ToString()
		{
			return string.Format("{0}{1}.{0}{2}", "0x", integer.ToString("X"), fraction.ToString("X")).ToLower();
		}

		public XmlElement ToXml(XmlDocument document)
		{
			XmlElement ntpstampNode = document.CreateElement("idmef:ntpstamp", "http://iana.org/idmef");
			XmlNode ntpstampSubNode = document.CreateNode(XmlNodeType.Text, "idmef", "ntpstamp", "http://iana.org/idmef");
			ntpstampSubNode.Value = ToString();
			ntpstampNode.AppendChild(ntpstampSubNode);

			return ntpstampNode;
		}
	}
}