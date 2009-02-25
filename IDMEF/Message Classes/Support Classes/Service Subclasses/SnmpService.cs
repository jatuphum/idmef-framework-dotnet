using System;
using System.Xml;

namespace idmef
{
	public class SnmpService
	{
		public string command;
		public string contextEngineID;
		public string contextName;
		public Int64? messageProcessingModel;
		public string oid;
		public Int64? securityLevel;
		public Int64? securityModel;
		public string securityName;

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

			if (!string.IsNullOrEmpty(oid))
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
			if (!string.IsNullOrEmpty(securityName))
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

			if (!string.IsNullOrEmpty(contextName))
			{
				XmlElement snmpServiceSubNode = document.CreateElement("idmef:contextName", "http://iana.org/idmef");
				XmlNode subNode = document.CreateNode(XmlNodeType.Text, "idmef", "contextName", "http://iana.org/idmef");
				subNode.Value = contextName;
				snmpServiceSubNode.AppendChild(subNode);
				snmpServiceNode.AppendChild(snmpServiceSubNode);
			}
			if (!string.IsNullOrEmpty(contextEngineID))
			{
				XmlElement snmpServiceSubNode = document.CreateElement("idmef:contextEngineID", "http://iana.org/idmef");
				XmlNode subNode = document.CreateNode(XmlNodeType.Text, "idmef", "contextEngineID", "http://iana.org/idmef");
				subNode.Value = contextEngineID;
				snmpServiceSubNode.AppendChild(subNode);
				snmpServiceNode.AppendChild(snmpServiceSubNode);
			}
			if (!string.IsNullOrEmpty(command))
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