using System;
using System.Xml;

namespace idmef
{
	public class Confidence
	{
		private ConfidenceRatingEnum rating = ConfidenceRatingEnum.numeric;
		private float value = 1.0f;

		public Confidence(float value)
		{
			if ((value < 0.0f) || (value > 1.0f))
				throw new ArgumentException("Confidence level must be in [0..1] range.");
			rating = ConfidenceRatingEnum.numeric;
			this.value = value;
		}

		public Confidence(ConfidenceRatingEnum rating)
		{
			if (rating == ConfidenceRatingEnum.numeric)
				throw new ArgumentException("To set numeric confidence level, use another constructor.");
			this.rating = rating;
		}

		public XmlElement ToXml(XmlDocument document)
		{
			XmlElement actionNode = document.CreateElement("idmef:Confidence", "http://iana.org/idmef");

			actionNode.SetAttribute("rating", EnumDescription.GetEnumDescription(rating));

			if (rating == ConfidenceRatingEnum.numeric)
			{
				XmlNode impactSubNode = document
					.CreateNode(XmlNodeType.Text, "idmef", "Confidence", "http://iana.org/idmef");
				impactSubNode.Value = value.ToString();
				actionNode.AppendChild(impactSubNode);
			}

			return actionNode;
		}
	}
}