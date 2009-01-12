using System.Xml;

namespace idmef
{
	public class Action
	{
		public ActionCategoryEnum category = ActionCategoryEnum.other;
		public string description;

		public Action()
		{
		}

		public Action(ActionCategoryEnum category, string description)
		{
			this.category = category;
			this.description = description;
		}

		public XmlElement ToXml(XmlDocument document)
		{
			XmlElement actionNode = document.CreateElement("idmef:Action", "http://iana.org/idmef");

			actionNode.SetAttribute("category", EnumDescription.GetEnumDescription(category));

			if ((description != null) && (description.Length > 0))
			{
				XmlNode impactSubNode = document
					.CreateNode(XmlNodeType.Text, "idmef", "Action", "http://iana.org/idmef");
				impactSubNode.Value = description;
				actionNode.AppendChild(impactSubNode);
			}

			return actionNode;
		}
	}
}