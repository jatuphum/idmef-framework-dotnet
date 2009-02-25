using System;
using System.Xml;

namespace idmef
{
	public class Node
	{
		private readonly string ident = "0";
		public Address[] address;
		public NodeCategoryEnum category = NodeCategoryEnum.unknown;

		public string location;
		public string name;

		public Node(string name)
		{
			if (string.IsNullOrEmpty(name)) throw new ArgumentException("Node must have at least one either name or Address node.");
			this.name = name;
		}

		public Node(Address[] address)
		{
			if ((address == null) || (address.Length == 0))
				throw new ArgumentException("Node must have at least one either name or Address node.");
			this.address = address;
		}

		public Node(string location, string name, Address[] address)
		{
			if (string.IsNullOrEmpty(name) && ((address == null) || (address.Length == 0)))
				throw new ArgumentException("Node must have at least one either name or Address node.");
			this.location = location;
			this.name = name;
			this.address = address;
		}

		public Node(string location, string name, Address[] address, string ident, NodeCategoryEnum category)
			: this(location, name, address)
		{
			this.ident = string.IsNullOrEmpty(ident) ? "0" : ident;
			this.category = category;
		}

		public XmlElement ToXml(XmlDocument document)
		{
			XmlElement nodeNode = document.CreateElement("idmef:Node", "http://iana.org/idmef");

			nodeNode.SetAttribute("ident", ident);
			nodeNode.SetAttribute("category", EnumDescription.GetEnumDescription(category));

			if (!string.IsNullOrEmpty(location))
			{
				XmlElement nodeSubNode = document.CreateElement("idmef:location", "http://iana.org/idmef");
				XmlNode subNode = document.CreateNode(XmlNodeType.Text, "idmef", "location", "http://iana.org/idmef");
				subNode.Value = location;
				nodeSubNode.AppendChild(subNode);
				nodeNode.AppendChild(nodeSubNode);
			}
			if (!string.IsNullOrEmpty(name))
			{
				XmlElement nodeSubNode = document.CreateElement("idmef:name", "http://iana.org/idmef");
				XmlNode subNode = document.CreateNode(XmlNodeType.Text, "idmef", "name", "http://iana.org/idmef");
				subNode.Value = name;
				nodeSubNode.AppendChild(subNode);
				nodeNode.AppendChild(nodeSubNode);
			}
			if ((address != null) && (address.Length > 0))
				foreach (var a in address)
					if (a != null) nodeNode.AppendChild(a.ToXml(document));

			return nodeNode;
		}
	}
}