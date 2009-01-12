using System;
using System.Xml;

namespace idmef
{
	internal class CreateTime
	{
		private DateTime createTime;
		private NtpStamp ntpStamp;

		public CreateTime()
		{
			createTime = DateTime.Now;
			ntpStamp = NtpStamp.Convert(createTime);
		}

		public XmlElement ToXml(XmlDocument document)
		{
			XmlElement createtTimeNode = document.CreateElement("idmef:CreateTime", "http://iana.org/idmef");
			createtTimeNode.SetAttribute("ntpstamp", ntpStamp.ToString());

			XmlNode subNodeText = document
				.CreateNode(XmlNodeType.Text, "idmef", "CreateTime", "http://iana.org/idmef");
			subNodeText.Value = createTime.ToString("o");
			createtTimeNode.AppendChild(subNodeText);

			return createtTimeNode;
		}
	}
}