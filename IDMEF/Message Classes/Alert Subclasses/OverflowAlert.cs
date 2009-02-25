using System;
using System.Xml;

namespace idmef
{
	public class OverflowAlert
	{
		private readonly string program;
		public byte[] buffer;
		public Int64? size;

		public OverflowAlert(string program)
		{
			if (string.IsNullOrEmpty(program)) throw new ArgumentException("OverflowAlert must have a program node.");
			this.program = program;
		}

		public OverflowAlert(string program, Int64? size, byte[] buffer)
			: this(program)
		{
			this.size = size;
			this.buffer = buffer;
		}


		public XmlElement ToXml(XmlDocument document)
		{
			if (string.IsNullOrEmpty(program)) throw new InvalidOperationException("There must be a program node.");

			XmlElement alertNode = document.CreateElement("idmef:OverflowAlert", "http://iana.org/idmef");

			XmlElement subNode = document.CreateElement("idmef:program", "http://iana.org/idmef");
			XmlNode subNodeText = document
				.CreateNode(XmlNodeType.Text, "idmef", "program", "http://iana.org/idmef");
			subNodeText.Value = program;
			subNode.AppendChild(subNodeText);
			alertNode.AppendChild(subNode);
			if (size != null)
			{
				subNode = document.CreateElement("idmef:size", "http://iana.org/idmef");
				subNodeText = document.CreateNode(XmlNodeType.Text, "idmef", "size", "http://iana.org/idmef");
				subNodeText.Value = size.ToString();
				subNode.AppendChild(subNodeText);
				alertNode.AppendChild(subNode);
			}
			if ((buffer != null) && (buffer.Length > 0))
			{
				subNode = document.CreateElement("idmef:buffer", "http://iana.org/idmef");
				subNodeText = document.CreateNode(XmlNodeType.Text, "idmef", "buffer", "http://iana.org/idmef");
				subNodeText.Value = Convert.ToBase64String(buffer);
				subNode.AppendChild(subNodeText);
				alertNode.AppendChild(subNode);
			}

			return alertNode;
		}
	}
}