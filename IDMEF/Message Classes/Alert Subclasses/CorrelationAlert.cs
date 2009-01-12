using System;
using System.Xml;

namespace idmef
{
	public class CorrelationAlert
	{
		private string name = null;
		private AlertIdent[] alertIdent = null;

		public CorrelationAlert(string name, AlertIdent[] alertIdent)
		{
			if ((name == null) || (name.Length == 0))
				throw new ArgumentException("CorrelationAlert must have a name node.");
			if ((alertIdent == null) || (alertIdent.Length == 0))
				throw new ArgumentException("CorrelationAlert must have at least one alertident node.");
			this.name = name;
			this.alertIdent = alertIdent;
		}

		public XmlElement ToXml(XmlDocument document)
		{
			XmlElement alertNode = document.CreateElement("idmef:CorrelationAlert", "http://iana.org/idmef");
			XmlElement subNode = document.CreateElement("idmef:name", "http://iana.org/idmef");
			XmlNode subNodeText = document.CreateNode(XmlNodeType.Text, "idmef", "name", "http://iana.org/idmef");

			subNodeText.Value = name;

			subNode.AppendChild(subNodeText);
			alertNode.AppendChild(subNode);
			foreach (AlertIdent ai in alertIdent)
				if (ai != null) alertNode.AppendChild(ai.ToXml(document));

			return alertNode;
		}
	}
}