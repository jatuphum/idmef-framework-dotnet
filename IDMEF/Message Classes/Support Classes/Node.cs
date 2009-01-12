using System;
using System.Xml;

namespace idmef
{
	public class Node
	{
		private string ident = "0";
		public NodeCategoryEnum category = NodeCategoryEnum.unknown;

		public string location = null;
		public string name = null;
		public Address[] address;

		public Node(string name)
		{
			if ((name == null) || (name.Length == 0))
				throw new ArgumentException("Node must have at least one either name or Address node.");
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
			if ((name == null) || (name.Length == 0))
				if ((address == null) || (address.Length == 0))
					throw new ArgumentException("Node must have at least one either name or Address node.");
			this.location = location;
			this.name = name;
			this.address = address;
		}

		public Node(string location, string name, Address[] address, string ident, NodeCategoryEnum category)
			: this(location, name, address)
		{
			if ((ident == null) || (ident.Length == 0))
				this.ident = "0";
			else
				this.ident = ident;
			this.category = category;
		}

		public XmlElement ToXml(XmlDocument document)
		{
			XmlElement nodeNode = document.CreateElement("idmef:Node", "http://iana.org/idmef");

			nodeNode.SetAttribute("ident", ident);
			nodeNode.SetAttribute("category", EnumDescription.GetEnumDescription(category));

			if ((location != null) && (location.Length > 0))
			{
				XmlElement nodeSubNode = document.CreateElement("idmef:location", "http://iana.org/idmef");
				XmlNode subNode = document.CreateNode(XmlNodeType.Text, "idmef", "location", "http://iana.org/idmef");
				subNode.Value = location;
				nodeSubNode.AppendChild(subNode);
				nodeNode.AppendChild(nodeSubNode);
			}
			if ((name != null) && (name.Length > 0))
			{
				XmlElement nodeSubNode = document.CreateElement("idmef:name", "http://iana.org/idmef");
				XmlNode subNode = document.CreateNode(XmlNodeType.Text, "idmef", "name", "http://iana.org/idmef");
				subNode.Value = name;
				nodeSubNode.AppendChild(subNode);
				nodeNode.AppendChild(nodeSubNode);
			}
			if ((address != null) && (address.Length > 0))
				foreach (Address a in address)
					if (a != null) nodeNode.AppendChild(a.ToXml(document));

			return nodeNode;
		}
	}
}