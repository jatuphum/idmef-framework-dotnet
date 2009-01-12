using System;
using System.Xml;

namespace idmef
{
	public class Linkage
	{
		private LinkageCategoryEnum category = LinkageCategoryEnum.unknown;

		private string name = null;
		private string path = null;
		private File file = null;

		public Linkage(string name, string path, File file, LinkageCategoryEnum category)
		{
			if ((name == null) || (name.Length == 0))
				throw new ArgumentException("Linkage must have a name node.");
			if ((path == null) || (path.Length == 0))
				throw new ArgumentException("Linkage must have a path node.");
			if (file == null)
				throw new ArgumentException("Linkage must have a File node.");
			if (category == LinkageCategoryEnum.unknown)
				throw new ArgumentException("Linkage must have a category attribute.");
			this.name = name;
			this.path = path;
			this.file = file;
			this.category = category;
		}


		public XmlElement ToXml(XmlDocument document)
		{
			XmlElement linkageNode = document.CreateElement("idmef:Linkage", "http://iana.org/idmef");

			linkageNode.SetAttribute("category", EnumDescription.GetEnumDescription(category));

			XmlElement linkageSubNode = document.CreateElement("idmef:name", "http://iana.org/idmef");
			XmlNode subNode = document.CreateNode(XmlNodeType.Text, "idmef", "name", "http://iana.org/idmef");
			subNode.Value = name;
			linkageSubNode.AppendChild(subNode);
			linkageNode.AppendChild(linkageSubNode);
			linkageSubNode = document.CreateElement("idmef:path", "http://iana.org/idmef");
			subNode = document.CreateNode(XmlNodeType.Text, "idmef", "path", "http://iana.org/idmef");
			subNode.Value = path;
			linkageSubNode.AppendChild(subNode);
			linkageNode.AppendChild(linkageSubNode);
			linkageNode.AppendChild(file.ToXml(document));

			return linkageNode;
		}
	}
}