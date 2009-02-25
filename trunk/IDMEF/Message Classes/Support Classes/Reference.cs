using System;
using System.Xml;

namespace idmef
{
	public class Reference
	{
		private readonly string meaning;
		private readonly string name;
		private readonly OriginEnum origin = OriginEnum.unknown;
		private readonly string url;

		public Reference(string name, string url)
		{
			if (string.IsNullOrEmpty(name)) throw new ArgumentException("Reference must have a name node.");
			if (string.IsNullOrEmpty(url)) throw new ArgumentException("Reference must have an url node.");

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
			if (((origin == OriginEnum.userSpecific) || (origin == OriginEnum.vendorSpecific)) && !string.IsNullOrEmpty(meaning))
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