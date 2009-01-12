using System;
using System.Xml;

namespace idmef
{
	public class AdditionalData
	{
		public string meaning = null;

		private ADEnum type;
		private bool data_boolean;
		private byte data_byte;
		private char data_character;
		private DateTime data_datetime;
		private long data_integer;
		private NtpStamp data_ntpstamp;
		private PortList data_portlist;
		private double data_real;
		private string data_string;
		private byte[] data_bytestring;
		private XmlElement data_xml;

		public AdditionalData(string meaning, bool data)
		{
			this.meaning = meaning;
			data_boolean = data;
			type = ADEnum.boolean;
		}
		public AdditionalData(string meaning, byte data)
		{
			this.meaning = meaning;
			data_byte = data;
			type = ADEnum.adByte;
		}
		public AdditionalData(string meaning, char data)
		{
			this.meaning = meaning;
			data_character = data;
			type = ADEnum.character;
		}
		public AdditionalData(string meaning, DateTime data)
		{
			this.meaning = meaning;
			data_datetime = data;
			type = ADEnum.datetime;
		}
		public AdditionalData(string meaning, long data)
		{
			this.meaning = meaning;
			data_integer = data;
			type = ADEnum.integer;
		}
		public AdditionalData(string meaning, NtpStamp data)
		{
			this.meaning = meaning;
			data_ntpstamp = data;
			type = ADEnum.ntpstamp;
		}
		public AdditionalData(string meaning, PortList data)
		{
			this.meaning = meaning;
			data_portlist = data;
			type = ADEnum.portlist;
		}
		public AdditionalData(string meaning, double  data)
		{
			this.meaning = meaning;
			data_real = data;
			type = ADEnum.real;
		}
		public AdditionalData(string meaning, string data)
		{
			this.meaning = meaning;
			data_string = data;
			type = ADEnum.adString;
		}
		public AdditionalData(string meaning, byte[] data)
		{
			this.meaning = meaning;
			data_bytestring = data;
			type = ADEnum.byteString;
		}
		public AdditionalData(string meaning, XmlElement data)
		{
			this.meaning = meaning;
			data_xml = data;
			type = ADEnum.xml;
		}

		public XmlElement ToXml(XmlDocument document)
		{
			XmlElement adNode = document.CreateElement("idmef:AdditionalData", "http://iana.org/idmef");
			XmlNode adSubNode = document
				.CreateNode(XmlNodeType.Text, "idmef", EnumDescription.GetEnumDescription(type), "http://iana.org/idmef");

			switch (type)
			{
				case ADEnum.boolean:
					adSubNode.Value = data_boolean.ToString();
					break;
				case ADEnum.adByte:
					adSubNode.Value = data_byte.ToString();
					break;
				case ADEnum.character:
					adSubNode.Value = data_character.ToString();
					break;
				case ADEnum.datetime:
					adSubNode.Value = data_datetime.ToString("o");
					break;
				case ADEnum.integer:
					adSubNode.Value = data_integer.ToString();
					break;
				case ADEnum.ntpstamp:
					adSubNode.Value = data_ntpstamp.ToString();
					break;
				case ADEnum.portlist:
					adSubNode.Value = data_portlist.ToString();
					break;
				case ADEnum.real:
					adSubNode.Value = data_real.ToString();
					break;
				case ADEnum.adString:
					adSubNode.Value = data_string;
					break;
				case ADEnum.byteString:
					adSubNode.Value = Convert.ToBase64String(data_bytestring);
					break;
				case ADEnum.xml:
					try
					{
						adSubNode = document.CreateElement("idmef:xml", "http://iana.org/idmef");
						//adSubNode.AppendChild(data_xml);
						adSubNode.InnerXml = data_xml.OuterXml;
					}
					catch (Exception e)
					{
						Console.WriteLine(e);
					}
					break;
			}

			adNode.SetAttribute("type", EnumDescription.GetEnumDescription(type));
			if ((meaning != null) && (meaning.Length > 0))
				adNode.SetAttribute("meaning", meaning);

			adNode.AppendChild(adSubNode);

			return adNode;
		}
	}
}