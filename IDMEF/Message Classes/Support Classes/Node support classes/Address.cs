using System;
using System.Xml;

namespace idmef
{
	public class Address
	{
		private readonly string ident = "0";
		public string address;
		public AddressCategoryEnum category = AddressCategoryEnum.unknown;
		public string netmask;
		public string vlanName;
		public Int64? vlanNum;

		public Address(string address)
		{
			if (string.IsNullOrEmpty(address)) throw new ArgumentException("Address must have an address node.");
			this.address = address;
		}

		public Address(string address, string netmask): this(address)
		{
			this.netmask = netmask;
		}

		public Address(string address, string netmask, string ident, AddressCategoryEnum category, string vlanName, Int64? vlanNum)
			: this(address, netmask)
		{
			this.ident = string.IsNullOrEmpty(ident) ? "0" : ident;
			this.category = category;
			this.vlanName = vlanName;
			this.vlanNum = vlanNum;
		}

		public XmlElement ToXml(XmlDocument document)
		{
			XmlElement addressNode = document.CreateElement("idmef:Address", "http://iana.org/idmef");

			addressNode.SetAttribute("ident", ident);
			addressNode.SetAttribute("category", EnumDescription.GetEnumDescription(category));
			if (!string.IsNullOrEmpty(vlanName)) addressNode.SetAttribute("vlan-name", vlanName);
			if (vlanNum != null) addressNode.SetAttribute("vlan-num", vlanNum.ToString());

			if (string.IsNullOrEmpty(address)) throw new InvalidOperationException("Address must have an address node.");
			XmlElement addressSubNode = document.CreateElement("idmef:address", "http://iana.org/idmef");
			XmlNode subNode = document.CreateNode(XmlNodeType.Text, "idmef", "address", "http://iana.org/idmef");
			subNode.Value = address;
			addressSubNode.AppendChild(subNode);
			addressNode.AppendChild(addressSubNode);

			if (!string.IsNullOrEmpty(netmask))
			{
				addressSubNode = document.CreateElement("idmef:netmask", "http://iana.org/idmef");
				subNode = document.CreateNode(XmlNodeType.Text, "idmef", "netmask", "http://iana.org/idmef");
				subNode.Value = netmask;
				addressSubNode.AppendChild(subNode);
				addressNode.AppendChild(addressSubNode);
			}

			return addressNode;
		}
	}
}