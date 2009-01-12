using System;
using System.Xml;

namespace idmef
{
	public class Reference
	{
		private OriginEnum origin = OriginEnum.unknown;
		private string meaning = null;
		private string name = null;
		private string url = null;

		public Reference(string name, string url)
		{
			if ((name == null) || (name.Length == 0)) throw new ArgumentException("Reference must have a name node.");
			if ((url == null) || (url.Length == 0)) throw new ArgumentException("Reference must have an url node.");

			this.name = name;
			this.url = url;
		}

		public Reference(OriginEnum origin, string meaning, string name, string url): this(name, url)
		{
			this.origin = origin;
			this.meaning = meaning;
		}

		public XmlElement ToXml(XmlDocument document)
		{
			XmlElement referenceNode = document.CreateElement("idmef:Reference", "http://iana.org/idmef");

			referenceNode.SetAttribute("origin", EnumDescription.GetEnumDescription(origin));
			if ((origin == OriginEnum.userSpecific) || (origin == OriginEnum.vendorSpecific))
				if ((meaning != null) && (meaning.Length > 0))
					referenceNode.SetAttribute("meaning", meaning);

			XmlElement referenceSubNode = document.CreateElement("idmef:name", "http://iana.org/idmef");
			XmlNode impactSubNode = document.CreateNode(XmlNodeType.Text, "idmef", "name", "http://iana.org/idmef");
			impactSubNode.Value = name;
			referenceSubNode.AppendChild(impactSubNode);
			referenceNode.AppendChild(referenceSubNode);
			referenceSubNode = document.CreateElement("idmef:url", "http://iana.org/idmef");
			impactSubNode = document.CreateNode(XmlNodeType.Text, "idmef", "url", "http://iana.org/idmef");
			impactSubNode.Value = url;
			referenceSubNode.AppendChild(impactSubNode);
			referenceNode.AppendChild(referenceSubNode);

			return referenceNode;
		}
	}
}