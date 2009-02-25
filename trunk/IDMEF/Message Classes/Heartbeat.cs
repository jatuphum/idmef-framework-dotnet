using System;
using System.Xml;

namespace idmef
{
	public class Heartbeat
	{
		private readonly Analyzer analyzer;
		private readonly CreateTime createTime = new CreateTime();
		private readonly string messageId = "0";
		public AdditionalData[] additionalData;
		public AnalyzerTime analyzerTime;
		public Int64? heartbeatInterval;

		public Heartbeat(Analyzer analyzer)
		{
			if (analyzer == null) throw new ArgumentException("Heartbeat must have an Analyzer node.");
			this.analyzer = analyzer;
		}

		public Heartbeat(Analyzer analyzer, Int64? heartbeatInterval, AnalyzerTime analyzerTime, AdditionalData[] additionalData, string messageId)
			: this(analyzer)
		{
			this.messageId = string.IsNullOrEmpty(messageId) ? "0" : messageId;

			this.heartbeatInterval = heartbeatInterval;
			this.analyzerTime = analyzerTime;
			this.additionalData = additionalData;
		}

		public XmlElement ToXml(XmlDocument document)
		{
			XmlElement alertNode = document.CreateElement("idmef:Heartbeat", "http://iana.org/idmef");

			alertNode.SetAttribute("messageId", messageId);

			alertNode.AppendChild(analyzer.ToXml(document));
			alertNode.AppendChild(createTime.ToXml(document));
			if (heartbeatInterval != null)
			{
				XmlElement subNode = document.CreateElement("idmef:HeartbeatInterval", "http://iana.org/idmef");
				XmlNode subNodeText = document.CreateNode(XmlNodeType.Text, "idmef", "HeartbeatInterval", "http://iana.org/idmef");

				subNodeText.Value = heartbeatInterval.ToString();

				subNode.AppendChild(subNodeText);
				alertNode.AppendChild(subNode);
			}
			if (analyzerTime != null) alertNode.AppendChild(analyzerTime.ToXml(document));
			if (additionalData != null)
				foreach (var ad in additionalData)
					if (ad != null) alertNode.AppendChild(ad.ToXml(document));

			return alertNode;
		}
	}
}