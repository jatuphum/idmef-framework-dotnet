using System;
using System.Xml;

namespace idmef
{
	public class Service
	{
		private readonly string ident = "0";

		private readonly string name;
		private readonly PortList portList;
		public string iana_protocol_name;
		public Int64? iana_protocol_number;
		public Byte? ip_version;
		private Int32? port;
		public string protocol;
		public SnmpService snmpService;
		public WebService webService;

		public Service(string name)
		{
			if (string.IsNullOrEmpty(name))
				throw new ArgumentException("Service must have at least one of the following nodes: name, port or portlist instead.");
			this.name = name;
		}

		public Service(Int32 port)
		{
			this.port = port;
		}

		public Service(string name, Int32 port)
		{
			this.name = name;
			this.port = port;
		}

		public Service(PortList portList)
		{
			if (portList == null)
				throw new ArgumentException("Service must have at least one of the following nodes: name, port or portlist instead.");
			this.portList = portList;
		}

		public Service(string name, Int32? port, PortList portList, string protocol)
		{
			if (string.IsNullOrEmpty(name) && (port == null) && (portList == null))
				throw new ArgumentException("Service must have at least one of the following nodes: name, port or portlist instead.");
			if ((!string.IsNullOrEmpty(name) || (port != null)) && (portList != null))
				throw new ArgumentException("Service must have either: (name and/or port) or portlist node.");

			this.name = name;
			this.port = port;
			this.portList = portList;
			this.protocol = protocol;
		}

		public Service(string name, Int32? port, PortList portList, string protocol, SnmpService snmpService)
			: this(name, port, portList, protocol)
		{
			this.snmpService = snmpService;
		}

		public Service(string name, Int32? port, PortList portList, string protocol, WebService webService)
			: this(name, port, portList, protocol)
		{
			this.webService = webService;
		}

		public Service(string name, Int32? port, PortList portList, string protocol, SnmpService snmpService, WebService webService, string ident)
			: this(name, port, portList, protocol)
		{
			if ((snmpService != null) && (webService != null))
				throw new ArgumentException("Service can have either SNMPService or WebService node.");
			this.snmpService = snmpService;
			this.webService = webService;
			this.ident = string.IsNullOrEmpty(ident) ? "0" : ident;
		}

		public XmlElement ToXml(XmlDocument document)
		{
			XmlElement serviceNode = document.CreateElement("idmef:Service", "http://iana.org/idmef");

			serviceNode.SetAttribute("ident", ident);
			if (ip_version != null) serviceNode.SetAttribute("ip_version", ip_version.ToString());
			if (iana_protocol_number != null) serviceNode.SetAttribute("iana_protocol_number", iana_protocol_number.ToString());
			if (!string.IsNullOrEmpty(iana_protocol_name)) serviceNode.SetAttribute("iana_protocol_name", iana_protocol_name);
			if (!string.IsNullOrEmpty(name))
			{
				XmlElement serviceSubNode = document.CreateElement("idmef:name", "http://iana.org/idmef");
				XmlNode subNode = document.CreateNode(XmlNodeType.Text, "idmef", "name", "http://iana.org/idmef");
				subNode.Value = name;
				serviceSubNode.AppendChild(subNode);
				serviceNode.AppendChild(serviceSubNode);
			}
			if (port != null)
			{
				XmlElement serviceSubNode = document.CreateElement("idmef:port", "http://iana.org/idmef");
				XmlNode subNode = document.CreateNode(XmlNodeType.Text, "idmef", "port", "http://iana.org/idmef");
				subNode.Value = port.ToString();
				serviceSubNode.AppendChild(subNode);
				serviceNode.AppendChild(serviceSubNode);
			}
			if (portList != null) serviceNode.AppendChild(portList.ToXml(document));
			if (!string.IsNullOrEmpty(protocol))
			{
				XmlElement serviceSubNode = document.CreateElement("idmef:protocol", "http://iana.org/idmef");
				XmlNode subNode = document.CreateNode(XmlNodeType.Text, "idmef", "protocol", "http://iana.org/idmef");
				subNode.Value = protocol;
				serviceSubNode.AppendChild(subNode);
				serviceNode.AppendChild(serviceSubNode);
			}
			if (snmpService != null) serviceNode.AppendChild(snmpService.ToXml(document));
			if (webService != null) serviceNode.AppendChild(webService.ToXml(document));

			return serviceNode;
		}
	}
}