using System;
using System.Xml;

namespace idmef
{
	public class Checksum
	{
		private string value = null;
		public string key = null;

		private ChecksumAlgorithmEnum algorithm;

		public Checksum(string value, ChecksumAlgorithmEnum algorithm)
		{
			if((value == null)||(value.Length==0))
				throw new ArgumentException("Checksum must have a value node.");
			this.value = value;
			this.algorithm = algorithm;
		}
		public Checksum(string value, string key, ChecksumAlgorithmEnum algorithm)
			:this(value, algorithm)
		{
			this.key = key;
		}

		public XmlElement ToXml(XmlDocument document)
		{
			XmlElement checksumNode = document.CreateElement("idmef:Checksum", "http://iana.org/idmef");

			checksumNode.SetAttribute("category", EnumDescription.GetEnumDescription(algorithm));

			XmlElement checksumSubNode = document.CreateElement("idmef:value", "http://iana.org/idmef");
			XmlNode subNode = document.CreateNode(XmlNodeType.Text, "idmef", "value", "http://iana.org/idmef");
			subNode.Value = value;
			checksumSubNode.AppendChild(subNode);
			checksumNode.AppendChild(checksumSubNode);
			if ((key != null) && (key.Length>0))
			{
				checksumSubNode = document.CreateElement("idmef:key", "http://iana.org/idmef");
				subNode = document.CreateNode(XmlNodeType.Text, "idmef", "key", "http://iana.org/idmef");
				subNode.Value = key;
				checksumSubNode.AppendChild(subNode);
				checksumNode.AppendChild(checksumSubNode);
			}

			return checksumNode;
		}

	}
}