using System;
using System.Xml;

namespace idmef
{
	public class Heartbeat
	{
		private string messageId = "0";

		private Analyzer analyzer = null;
		private CreateTime createTime = new CreateTime();
		public Int64? heartbeatInterval = null;
		public AnalyzerTime analyzerTime = null;
		public AdditionalData[] additionalData = null;

		public Heartbeat(Analyzer analyzer)
		{
			if (analyzer == null)
				throw new ArgumentException("Heartbeat must have an Analyzer node.");
			this.analyzer = analyzer;
		}
		public Heartbeat(Analyzer analyzer, Int64? heartbeatInterval,AnalyzerTime analyzerTime,
			AdditionalData[] additionalData, string messageId)
			:this(analyzer)
		{
			this.messageId = ((messageId == null) || (messageId.Length == 0)) ? "0" : messageId;

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
				XmlElement subNode = document
					.CreateElement("idmef:HeartbeatInterval","http://iana.org/idmef");
				XmlNode subNodeText = document
					.CreateNode(XmlNodeType.Text, "idmef", "HeartbeatInterval", "http://iana.org/idmef");

				subNodeText.Value = heartbeatInterval.ToString();

				subNode.AppendChild(subNodeText);
				alertNode.AppendChild(subNode);
			}
			if (analyzerTime != null) alertNode.AppendChild(analyzerTime.ToXml(document));
			if (additionalData != null)
				foreach (AdditionalData ad in additionalData)
					if (ad != null) alertNode.AppendChild(ad.ToXml(document));

			return alertNode;
		}
	}
}