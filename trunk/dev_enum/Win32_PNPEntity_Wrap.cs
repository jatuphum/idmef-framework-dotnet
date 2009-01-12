using System;
using System.Management;
using System.Xml;

namespace dev_enum
{
	internal class Win32_PNPEntity_Wrap:IComparable<Win32_PNPEntity_Wrap>
	{
		private ManagementObject obj;

		public string Caption;
		public string ClassGuid;
		public string Description;
		public string DeviceID;
		public string Manufacturer;
		public string Name;
		public string PNPDeviceID;
		public string Status;
		public string SystemName;

		public Win32_PNPEntity_Wrap(ManagementObject obj)
		{
			Caption = (string) obj["Caption"];
			ClassGuid = (string) obj["ClassGuid"];
			Description = (string) obj["Description"];
			DeviceID = (string) obj["DeviceID"];
			Manufacturer = (string) obj["Manufacturer"];
			Name = (string) obj["Name"];
			PNPDeviceID = (string) obj["PNPDeviceID"];
			Status = (string) obj["Status"];
			SystemName = (string) obj["SystemName"];
		}

		public override int GetHashCode()
		{
			return ClassGuid.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			if (!(obj is Win32_PNPEntity_Wrap)) return false;

			Win32_PNPEntity_Wrap o = (Win32_PNPEntity_Wrap) obj;
			return (
				(Caption == o.Caption) &&
				(ClassGuid == o.ClassGuid) &&
				(Description == o.Description) &&
				(DeviceID == o.DeviceID) &&
				(Manufacturer == o.Manufacturer)&&
				(Name == o.Name)&&
				(PNPDeviceID == o.PNPDeviceID)&&
				(Status == o.Status)&&
				(SystemName == o.SystemName)
				);
		}

		public int CompareTo(Win32_PNPEntity_Wrap other)
		{
			return DeviceID.CompareTo(other.DeviceID);
		}

		public override string ToString()
		{
			return DeviceID;
		}

		public XmlElement ToXml()
		{
			XmlDocument doc = new XmlDocument();
			XmlElement element = doc.CreateElement("Win32_PNPEntity");
			element.SetAttribute("key", "DeviceID");

			if ((Caption != null) && (Caption.Length > 0))
			{
				XmlElement subNode = doc.CreateElement("Caption");
				XmlNode subNodeText = doc.CreateNode(XmlNodeType.Text, "Caption", "");
				subNodeText.Value = Caption;
				subNode.AppendChild(subNodeText);
				element.AppendChild(subNode);
			}
			if ((ClassGuid != null) && (ClassGuid.Length > 0))
			{
				XmlElement subNode = doc.CreateElement("ClassGuid");
				XmlNode subNodeText = doc.CreateNode(XmlNodeType.Text, "ClassGuid", "");
				subNodeText.Value = ClassGuid;
				subNode.AppendChild(subNodeText);
				element.AppendChild(subNode);
			}
			if ((Description != null) && (Description.Length > 0))
			{
				XmlElement subNode = doc.CreateElement("Description");
				XmlNode subNodeText = doc.CreateNode(XmlNodeType.Text, "Description", "");
				subNodeText.Value = Description;
				subNode.AppendChild(subNodeText);
				element.AppendChild(subNode);
			}
			if ((DeviceID != null) && (DeviceID.Length > 0))
			{
				XmlElement subNode = doc.CreateElement("DeviceID");
				XmlNode subNodeText = doc.CreateNode(XmlNodeType.Text, "DeviceID", "");
				subNodeText.Value = DeviceID;
				subNode.AppendChild(subNodeText);
				element.AppendChild(subNode);
			}
			if ((Manufacturer != null) && (Manufacturer.Length > 0))
			{
				XmlElement subNode = doc.CreateElement("Manufacturer");
				XmlNode subNodeText = doc.CreateNode(XmlNodeType.Text, "Manufacturer", "");
				subNodeText.Value = Manufacturer;
				subNode.AppendChild(subNodeText);
				element.AppendChild(subNode);
			}
			if ((Name != null) && (Name.Length > 0))
			{
				XmlElement subNode = doc.CreateElement("Name");
				XmlNode subNodeText = doc.CreateNode(XmlNodeType.Text, "Name", "");
				subNodeText.Value = Name;
				subNode.AppendChild(subNodeText);
				element.AppendChild(subNode);
			}
			if ((PNPDeviceID != null) && (PNPDeviceID.Length > 0))
			{
				XmlElement subNode = doc.CreateElement("PNPDeviceID");
				XmlNode subNodeText = doc.CreateNode(XmlNodeType.Text, "PNPDeviceID", "");
				subNodeText.Value = PNPDeviceID;
				subNode.AppendChild(subNodeText);
				element.AppendChild(subNode);
			}
			if ((Status != null) && (Status.Length > 0))
			{
				XmlElement subNode = doc.CreateElement("Status");
				XmlNode subNodeText = doc.CreateNode(XmlNodeType.Text, "Status", "");
				subNodeText.Value = Status;
				subNode.AppendChild(subNodeText);
				element.AppendChild(subNode);
			}
			if ((SystemName != null) && (SystemName.Length > 0))
			{
				XmlElement subNode = doc.CreateElement("SystemName");
				XmlNode subNodeText = doc.CreateNode(XmlNodeType.Text, "SystemName", "");
				subNodeText.Value = SystemName;
				subNode.AppendChild(subNodeText);
				element.AppendChild(subNode);
			}
			return element;
		}
	}
}