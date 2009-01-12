using System.Xml;

namespace idmef
{
	public class Assessment
	{
		public Impact impact;
		public Action[] action;
		public Confidence confidence;

		public Assessment()
		{
		}

		public Assessment(Impact impact, Action[] action, Confidence confidence)
		{
			this.impact = impact;
			this.action = action;
			this.confidence = confidence;
		}

		public Assessment(Impact impact, Action action, Confidence confidence)
			: this(impact, new Action[] {action}, confidence)
		{
		}

		public XmlElement ToXml(XmlDocument document)
		{
			XmlElement assessmentNode = document.CreateElement("idmef:Assessment", "http://iana.org/idmef");

			if (impact != null) assessmentNode.AppendChild(impact.ToXml(document));
			if ((action != null) && (action.Length > 0))
				foreach (Action a in action)
					if (a != null) assessmentNode.AppendChild(a.ToXml(document));
			if (confidence != null) assessmentNode.AppendChild(confidence.ToXml(document));

			return assessmentNode;
		}
	}
}