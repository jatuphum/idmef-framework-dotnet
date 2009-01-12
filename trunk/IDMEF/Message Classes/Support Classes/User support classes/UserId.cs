using System;
using System.Xml;

namespace idmef
{
	public class UserId
	{
		private string ident = "0";
		public UserIdTypeEnum type = UserIdTypeEnum.originalUser;
		public string tty = null;

		public string name = null;
		public Int64? number = null;

		public UserId(string name, Int64? number)
		{
			this.name = name;
			this.number = number;
		}

		public UserId(string name, Int64? number, string ident, UserIdTypeEnum type, string tty)
			: this(name, number)
		{
			if ((ident == null) || (ident.Length == 0))
				this.ident = "0";
			else
				this.ident = ident;
			this.type = type;
			this.tty = tty;
		}

		public XmlElement ToXml(XmlDocument document)
		{
			XmlElement addressNode = document.CreateElement("idmef:UserId", "http://iana.org/idmef");

			addressNode.SetAttribute("ident", ident);
			addressNode.SetAttribute("type", EnumDescription.GetEnumDescription(type));
			if ((tty != null) && (tty.Length > 0)) addressNode.SetAttribute("tty", tty);

			if ((name != null) && (name.Length > 0))
			{
				XmlElement addressSubNode = document.CreateElement("idmef:name", "http://iana.org/idmef");
				XmlNode subNode = document.CreateNode(XmlNodeType.Text, "idmef", "name", "http://iana.org/idmef");
				subNode.Value = name;
				addressSubNode.AppendChild(subNode);
				addressNode.AppendChild(addressSubNode);
			}
			if (number != null)
			{
				XmlElement addressSubNode = document.CreateElement("idmef:number", "http://iana.org/idmef");
				XmlNode subNode = document.CreateNode(XmlNodeType.Text, "idmef", "number", "http://iana.org/idmef");
				subNode.Value = number.ToString();
				addressSubNode.AppendChild(subNode);
				addressNode.AppendChild(addressSubNode);
			}

			return addressNode;
		}
	}
}