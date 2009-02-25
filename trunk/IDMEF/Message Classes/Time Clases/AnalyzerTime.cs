using System;
using System.Xml;

namespace idmef
{
	public class AnalyzerTime
	{
		private readonly NtpStamp ntpStamp;
		private DateTime analyzerTime;

		public AnalyzerTime()
		{
			analyzerTime = DateTime.Now;
			ntpStamp = NtpStamp.Convert(analyzerTime);
		}

		public XmlElement ToXml(XmlDocument document)
		{
			XmlElement createtTimeNode = document.CreateElement("idmef:AnalyzerTime", "http://iana.org/idmef");
			createtTimeNode.SetAttribute("ntpstamp", ntpStamp.ToString());

			XmlNode subNodeText = document.CreateNode(XmlNodeType.Text, "idmef", "AnalyzerTime", "http://iana.org/idmef");
			subNodeText.Value = analyzerTime.ToString("o");
			createtTimeNode.AppendChild(subNodeText);

			return createtTimeNode;
		}
	}
}