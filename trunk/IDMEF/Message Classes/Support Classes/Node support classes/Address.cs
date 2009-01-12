using System;
using System.Xml;

namespace idmef
{
	public class Address
	{
		private string ident = "0";
		public AddressCategoryEnum category = AddressCategoryEnum.unknown;
		public string vlanName = null;
		public Int64? vlanNum = null;

		public string address = null;
		public string netmask = null;

		public Address(string address)
		{
			if ((address == null) || (address.Length == 0))
				throw new ArgumentException("Address must have an address node.");
			this.address = address;
		}

		public Address(string address, string netmask):this(address)
		{
			this.netmask = netmask;
		}

		public Address(string address, string netmask, string ident, AddressCategoryEnum category, string vlanName, Int64? vlanNum)
			:this(address, netmask)
		{
			if ((ident == null) || (ident.Length == 0))
				this.ident = "0";
			else
				this.ident = ident;
			this.category = category;
			this.vlanName = vlanName;
			this.vlanNum = vlanNum;
		}

		public XmlElement ToXml(XmlDocument document)
		{
			XmlElement addressNode = document.CreateElement("idmef:Address", "http://iana.org/idmef");

			addressNode.SetAttribute("ident", ident);
			addressNode.SetAttribute("category", EnumDescription.GetEnumDescription(category));
			if ((vlanName != null) && (vlanName.Length > 0))
				addressNode.SetAttribute("vlan-name", vlanName);
			if (vlanNum != null)
				addressNode.SetAttribute("vlan-num", vlanNum.ToString());

			if ((address == null) || (address.Length == 0))
				throw new InvalidOperationException("Address must have an address node.");
			XmlElement addressSubNode = document.CreateElement("idmef:address", "http://iana.org/idmef");
			XmlNode subNode = document.CreateNode(XmlNodeType.Text, "idmef", "address", "http://iana.org/idmef");
			subNode.Value = address;
			addressSubNode.AppendChild(subNode);
			addressNode.AppendChild(addressSubNode);

			if ((netmask != null) && (netmask.Length > 0))
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