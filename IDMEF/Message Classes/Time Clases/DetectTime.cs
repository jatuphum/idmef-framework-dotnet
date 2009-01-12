using System;
using System.Xml;

namespace idmef
{
	public class DetectTime
	{
		private DateTime detectTime;
		private NtpStamp ntpStamp;

		public DetectTime()
		{
			detectTime = DateTime.Now;
			ntpStamp = NtpStamp.Convert(detectTime);
		}

		public DetectTime(DateTime detectTime)
		{
			this.detectTime = detectTime;
			ntpStamp = NtpStamp.Convert(detectTime);
		}

		public XmlElement ToXml(XmlDocument document)
		{
			XmlElement createtTimeNode = document.CreateElement("idmef:DetectTime", "http://iana.org/idmef");
			createtTimeNode.SetAttribute("ntpstamp", ntpStamp.ToString());

			XmlNode subNodeText = document
				.CreateNode(XmlNodeType.Text, "idmef", "DetectTime", "http://iana.org/idmef");
			subNodeText.Value = detectTime.ToString("o");
			createtTimeNode.AppendChild(subNodeText);

			return createtTimeNode;
		}
	}
}