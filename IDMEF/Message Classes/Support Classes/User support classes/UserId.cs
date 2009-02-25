using System;
using System.Xml;

namespace idmef
{
	public class UserId
	{
		private readonly string ident = "0";

		public string name;
		public Int64? number;
		public string tty;
		public UserIdTypeEnum type = UserIdTypeEnum.originalUser;

		public UserId(string name, Int64? number)
		{
			this.name = name;
			this.number = number;
		}

		public UserId(string name, Int64? number, string ident, UserIdTypeEnum type, string tty)
			: this(name, number)
		{
			this.ident = string.IsNullOrEmpty(ident) ? "0" : ident;
			this.type = type;
			this.tty = tty;
		}

		public XmlElement ToXml(XmlDocument document)
		{
			XmlElement addressNode = document.CreateElement("idmef:UserId", "http://iana.org/idmef");

			addressNode.SetAttribute("ident", ident);
			addressNode.SetAttribute("type", EnumDescription.GetEnumDescription(type));
			if (!string.IsNullOrEmpty(tty)) addressNode.SetAttribute("tty", tty);

			if (!string.IsNullOrEmpty(name))
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