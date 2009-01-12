using System;
using System.Xml;

namespace idmef
{
	public class SnmpService
	{
		public string oid = null;
		public Int64? messageProcessingModel = null;
		public Int64? securityModel = null;
		public string securityName = null;
		public Int64? securityLevel = null;
		public string contextName = null;
		public string contextEngineID = null;
		public string command = null;

		public SnmpService()
		{
		}

		public SnmpService(string oid, Int64? messageProcessingModel, Int64? securityModel, string securityName,
						   Int64? securityLevel, string contextName, string contextEngineID, string command)
		{
			this.oid = oid;
			this.messageProcessingModel = messageProcessingModel;
			this.securityModel = securityModel;
			this.securityName = securityName;
			this.securityLevel = securityLevel;
			this.contextName = contextName;
			this.contextEngineID = contextEngineID;
			this.command = command;
		}

		public XmlElement ToXml(XmlDocument document)
		{
			XmlElement snmpServiceNode = document.CreateElement("idmef:SNMPService", "http://iana.org/idmef");

			if ((oid != null) && (oid.Length > 0))
			{
				XmlElement snmpServiceSubNode = document.CreateElement("idmef:oid", "http://iana.org/idmef");
				XmlNode subNode = document.CreateNode(XmlNodeType.Text, "idmef", "oid", "http://iana.org/idmef");
				subNode.Value = oid;
				snmpServiceSubNode.AppendChild(subNode);
				snmpServiceNode.AppendChild(snmpServiceSubNode);
			}
			if (messageProcessingModel != null)
			{
				XmlElement snmpServiceSubNode = document.CreateElement("idmef:messageProcessingModel", "http://iana.org/idmef");
				XmlNode subNode = document.CreateNode(XmlNodeType.Text, "idmef", "messageProcessingModel", "http://iana.org/idmef");
				subNode.Value = messageProcessingModel.ToString();
				snmpServiceSubNode.AppendChild(subNode);
				snmpServiceNode.AppendChild(snmpServiceSubNode);
			}
			if (securityModel != null)
			{
				XmlElement snmpServiceSubNode = document.CreateElement("idmef:securityModel", "http://iana.org/idmef");
				XmlNode subNode = document.CreateNode(XmlNodeType.Text, "idmef", "securityModel", "http://iana.org/idmef");
				subNode.Value = securityModel.ToString();
				snmpServiceSubNode.AppendChild(subNode);
				snmpServiceNode.AppendChild(snmpServiceSubNode);
			}
			if ((securityName != null) && (securityName.Length > 0))
			{
				XmlElement snmpServiceSubNode = document.CreateElement("idmef:securityName", "http://iana.org/idmef");
				XmlNode subNode = document.CreateNode(XmlNodeType.Text, "idmef", "securityName", "http://iana.org/idmef");
				subNode.Value = securityName;
				snmpServiceSubNode.AppendChild(subNode);
				snmpServiceNode.AppendChild(snmpServiceSubNode);
			}
			if (securityLevel != null)
			{
				XmlElement snmpServiceSubNode = document.CreateElement("idmef:securityLevel", "http://iana.org/idmef");
				XmlNode subNode = document.CreateNode(XmlNodeType.Text, "idmef", "securityLevel", "http://iana.org/idmef");
				subNode.Value = securityLevel.ToString();
				snmpServiceSubNode.AppendChild(subNode);
				snmpServiceNode.AppendChild(snmpServiceSubNode);
			}

			if ((contextName != null) && (contextName.Length > 0))
			{
				XmlElement snmpServiceSubNode = document.CreateElement("idmef:contextName", "http://iana.org/idmef");
				XmlNode subNode = document.CreateNode(XmlNodeType.Text, "idmef", "contextName", "http://iana.org/idmef");
				subNode.Value = contextName;
				snmpServiceSubNode.AppendChild(subNode);
				snmpServiceNode.AppendChild(snmpServiceSubNode);
			}
			if ((contextEngineID != null) && (contextEngineID.Length > 0))
			{
				XmlElement snmpServiceSubNode = document.CreateElement("idmef:contextEngineID", "http://iana.org/idmef");
				XmlNode subNode = document.CreateNode(XmlNodeType.Text, "idmef", "contextEngineID", "http://iana.org/idmef");
				subNode.Value = contextEngineID;
				snmpServiceSubNode.AppendChild(subNode);
				snmpServiceNode.AppendChild(snmpServiceSubNode);
			}
			if ((command != null) && (command.Length > 0))
			{
				XmlElement snmpServiceSubNode = document.CreateElement("idmef:command", "http://iana.org/idmef");
				XmlNode subNode = document.CreateNode(XmlNodeType.Text, "idmef", "command", "http://iana.org/idmef");
				subNode.Value = command;
				snmpServiceSubNode.AppendChild(subNode);
				snmpServiceNode.AppendChild(snmpServiceSubNode);
			}

			return snmpServiceNode;
		}
	}
}