using System;
using System.Xml;

namespace idmef
{
	public class AlertIdent
	{
		private readonly string alertIdent;
		public string analyzerId;

		public AlertIdent(string value)
		{
			if (string.IsNullOrEmpty(value)) throw new ArgumentException("AlertIdent mustn't be empty.");
			alertIdent = value;
		}

		public AlertIdent(string value, string analyzerId): this(value)
		{
			this.analyzerId = analyzerId;
		}

		public XmlElement ToXml(XmlDocument document)
		{
			XmlElement alertIdentNode = document.CreateElement("idmef:alertident", "http://iana.org/idmef");
			if (!string.IsNullOrEmpty(analyzerId)) alertIdentNode.SetAttribute("analyzerid", analyzerId);

			XmlNode subNodeText = document.CreateNode(XmlNodeType.Text, "idmef", "alertident", "http://iana.org/idmef");
			subNodeText.Value = alertIdent;
			alertIdentNode.AppendChild(subNodeText);

			return alertIdentNode;
		}
	}
}