using System.Xml;

namespace idmef
{
	public class Impact
	{
		public AssessmentTypeEnum assessmentType = AssessmentTypeEnum.other;
		public CompletionEnum completion = CompletionEnum.undefined;
		public string description;
		public SeverityEnum severity = SeverityEnum.undefined;

		public Impact()
		{
		}

		public Impact(SeverityEnum severity, CompletionEnum completion, AssessmentTypeEnum aType, string description)
		{
			this.severity = severity;
			this.completion = completion;
			assessmentType = aType;
			this.description = description;
		}

		public XmlElement ToXml(XmlDocument document)
		{
			XmlElement impactNode = document.CreateElement("idmef:Impact", "http://iana.org/idmef");

			if (severity != SeverityEnum.undefined) impactNode.SetAttribute("severity", severity.ToString());
			if (completion != CompletionEnum.undefined) impactNode.SetAttribute("completion", completion.ToString());
			impactNode.SetAttribute("type", assessmentType.ToString());

			if ((description != null) && (description.Length > 0))
			{
				XmlNode impactSubNode = document
					.CreateNode(XmlNodeType.Text, "idmef", "Impact", "http://iana.org/idmef");
				impactSubNode.Value = description;
				impactNode.AppendChild(impactSubNode);
			}

			return impactNode;
		}
	}
}