using System.Xml;

namespace idmef
{
	public class PortList
	{
		private readonly string portList;

		public PortList(string portList)
		{
			this.portList = portList;
		}

		public override string ToString()
		{
			return portList;
		}

		public XmlElement ToXml(XmlDocument document)
		{
			XmlElement portlistNode = document.CreateElement("idmef:portlist", "http://iana.org/idmef");
			XmlNode portlistSubNode = document.CreateNode(XmlNodeType.Text, "idmef", "portlist", "http://iana.org/idmef");
			portlistSubNode.Value = ToString();
			portlistNode.AppendChild(portlistSubNode);

			return portlistNode;
		}
	}
}