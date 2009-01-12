using System;
using System.Xml;

namespace idmef
{
	public class ToolAlert
	{
		private string name = null;
		public string command = null;
		private AlertIdent[] alertIdent = null;

		public ToolAlert(string name, AlertIdent[] alertIdent)
		{
			if ((name == null) || (name.Length == 0))
				throw new ArgumentException("ToolAlert must have a name node.");
			if ((alertIdent == null) || (alertIdent.Length == 0))
				throw new ArgumentException("ToolAlert must have at least one alertident node.");
			this.name = name;
			this.alertIdent = alertIdent;
		}

		public ToolAlert(string name, string command, AlertIdent[] alertIdent)
			: this(name, alertIdent)
		{
			this.command = command;
		}

		public XmlElement ToXml(XmlDocument document)
		{
			XmlElement alertNode = document.CreateElement("idmef:ToolAlert", "http://iana.org/idmef");

			XmlElement subNode = document.CreateElement("idmef:name", "http://iana.org/idmef");
			XmlNode subNodeText = document.CreateNode(XmlNodeType.Text, "idmef", "name", "http://iana.org/idmef");
			subNodeText.Value = name;
			subNode.AppendChild(subNodeText);
			alertNode.AppendChild(subNode);
			if ((command != null) && (command.Length > 0))
			{
				subNode = document.CreateElement("idmef:command", "http://iana.org/idmef");
				subNodeText = document.CreateNode(XmlNodeType.Text, "idmef", "command", "http://iana.org/idmef");

				subNodeText.Value = command;

				subNode.AppendChild(subNodeText);
				alertNode.AppendChild(subNode);
			}
			foreach (AlertIdent ai in alertIdent)
				if (ai != null) alertNode.AppendChild(ai.ToXml(document));

			return alertNode;
		}
	}
}