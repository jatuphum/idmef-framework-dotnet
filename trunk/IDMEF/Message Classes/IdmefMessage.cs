using System;
using System.Xml;

namespace idmef
{
	public class IdmefMessage
	{
		public Alert[] alert;
		public Heartbeat[] heartbeat;
		private const string version = "1.0";

		public IdmefMessage(Alert alert)
		{
			this.alert = new[] {alert};
		}

		public IdmefMessage(Alert[] alert)
		{
			this.alert = alert;
		}

		public IdmefMessage(Heartbeat heartbeat)
		{
			this.heartbeat = new[] {heartbeat};
		}

		public IdmefMessage(Heartbeat[] heartbeat)
		{
			this.heartbeat = heartbeat;
		}

		public XmlDocument ToXml()
		{
			var doc = new XmlDocument();
			XmlDeclaration declaration = doc.CreateXmlDeclaration("1.0", "utf-8", null);
			doc.AppendChild(declaration);
			XmlElement root = doc.CreateElement("idmef", "IDMEF-Message", "http://iana.org/idmef");
			root.SetAttribute("version", version);
			doc.AppendChild(root);

			if ((alert != null) && (heartbeat != null) && (alert.Length > 0) && (heartbeat.Length > 0))
				throw new InvalidOperationException("Only Alert or Heartbeat messages can be stored in one IDMEF-Message.");
			if (alert != null)
				foreach (var a in alert) if (a != null) doc.DocumentElement.AppendChild(a.ToXml(doc));
			if (heartbeat != null)
				foreach (var hb in heartbeat) if (hb != null) doc.DocumentElement.AppendChild(hb.ToXml(doc));

			return doc;
		}
	}
}