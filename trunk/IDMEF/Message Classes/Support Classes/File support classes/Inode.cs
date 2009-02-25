using System;
using System.Xml;

namespace idmef
{
	public class Inode
	{
		private readonly DateTime? changeTime;
		private Int16? cMajorDevice;
		private Int16? cMinorDevice;
		private Int16? majorDevice;
		private Int16? minorDevice;
		private Int64? number;

		public Inode(DateTime? changeTime, Int64? number, Int16? majorDevice, Int16? minorDevice, Int16? cMajorDevice, Int16? cMinorDevice)
		{
			this.changeTime = changeTime;
			if ((number != null) && (majorDevice != null) && (minorDevice != null))
			{
				this.number = number;
				this.majorDevice = majorDevice;
				this.minorDevice = minorDevice;
			}
			else if ((number != null) || (majorDevice != null) || (minorDevice != null))
				throw new ArgumentException("Inode class must have either number, major-device and minor-device nodes together or neither of them at all.");
			if ((cMajorDevice != null) && (cMinorDevice != null))
			{
				this.cMajorDevice = cMajorDevice;
				this.cMinorDevice = cMinorDevice;
			}
			else if ((cMajorDevice != null) || (cMinorDevice != null))
				throw new ArgumentException("Inode class must have either c-major-device and c-minor-device nodes together or neither of them at all.");
		}

		public XmlElement ToXml(XmlDocument document)
		{
			XmlElement inodeNode = document.CreateElement("idmef:Inode", "http://iana.org/idmef");

			if (changeTime != null)
			{
				XmlElement inodeSubNode = document.CreateElement("idmef:change-time", "http://iana.org/idmef");
				XmlNode subNode = document.CreateNode(XmlNodeType.Text, "idmef", "change-time", "http://iana.org/idmef");
				subNode.Value = ((DateTime)changeTime).ToString("o");
				inodeSubNode.AppendChild(subNode);
				inodeNode.AppendChild(inodeSubNode);
			}
			if (number != null)
			{
				XmlElement inodeSubNode = document.CreateElement("idmef:number", "http://iana.org/idmef");
				XmlNode subNode = document.CreateNode(XmlNodeType.Text, "idmef", "number", "http://iana.org/idmef");
				subNode.Value = number.ToString();
				inodeSubNode.AppendChild(subNode);
				inodeNode.AppendChild(inodeSubNode);
			}
			if (majorDevice != null)
			{
				XmlElement inodeSubNode = document.CreateElement("idmef:major-device", "http://iana.org/idmef");
				XmlNode subNode = document.CreateNode(XmlNodeType.Text, "idmef", "major-device", "http://iana.org/idmef");
				subNode.Value = majorDevice.ToString();
				inodeSubNode.AppendChild(subNode);
				inodeNode.AppendChild(inodeSubNode);
			}
			if (minorDevice != null)
			{
				XmlElement inodeSubNode = document.CreateElement("idmef:minor-device", "http://iana.org/idmef");
				XmlNode subNode = document.CreateNode(XmlNodeType.Text, "idmef", "minor-device", "http://iana.org/idmef");
				subNode.Value = minorDevice.ToString();
				inodeSubNode.AppendChild(subNode);
				inodeNode.AppendChild(inodeSubNode);
			}
			if (cMajorDevice != null)
			{
				XmlElement inodeSubNode = document.CreateElement("idmef:c-major-device", "http://iana.org/idmef");
				XmlNode subNode = document.CreateNode(XmlNodeType.Text, "idmef", "c-major-device", "http://iana.org/idmef");
				subNode.Value = cMajorDevice.ToString();
				inodeSubNode.AppendChild(subNode);
				inodeNode.AppendChild(inodeSubNode);
			}
			if (cMinorDevice != null)
			{
				XmlElement inodeSubNode = document.CreateElement("idmef:c-minor-device", "http://iana.org/idmef");
				XmlNode subNode = document.CreateNode(XmlNodeType.Text, "idmef", "c-minor-device", "http://iana.org/idmef");
				subNode.Value = cMinorDevice.ToString();
				inodeSubNode.AppendChild(subNode);
				inodeNode.AppendChild(inodeSubNode);
			}

			return inodeNode;
		}
	}
}