using System;
using System.Xml;

namespace idmef
{
	public class AlertIdent
	{
		private string alertIdent = null;
		public string analyzerId = null;

		public AlertIdent(string value)
		{
			if ((value == null) || (value.Length == 0))
				throw new ArgumentException("AlertIdent mustn't be empty.");
			alertIdent = value;
		}

		public AlertIdent(string value, string analyzerId)
			: this(value)
		{
			this.analyzerId = analyzerId;
		}

		public XmlElement ToXml(XmlDocument document)
		{
			XmlElement alertIdentNode = document.CreateElement("idmef:alertident", "http://iana.org/idmef");
			if ((analyzerId != null) && (analyzerId.Length > 0))
				alertIdentNode.SetAttribute("analyzerid", analyzerId);

			XmlNode subNodeText = document
				.CreateNode(XmlNodeType.Text, "idmef", "alertident", "http://iana.org/idmef");
			subNodeText.Value = alertIdent;
			alertIdentNode.AppendChild(subNodeText);

			return alertIdentNode;
		}
	}
}