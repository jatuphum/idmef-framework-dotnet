using System;
using System.Xml;

namespace idmef
{
	public class Alert
	{
		private readonly Analyzer analyzer;
		private readonly Classification classification;
		private readonly CreateTime createTime = new CreateTime();
		private readonly string messageId = "0";
		public AdditionalData[] additionalData;
		public AnalyzerTime analyzerTime;
		public Assessment assessment;
		public CorrelationAlert correlationAlert;
		public DetectTime detectTime;
		public OverflowAlert overflowAlert;
		public Source[] source;
		public Target[] target;
		public ToolAlert toolAlert;

		public Alert(Analyzer analyzer, Classification classification)
		{
			if (analyzer == null) throw new ArgumentException("Alert must have an Analyzer node.");
			if (classification == null) throw new ArgumentException("Alert must have an Classification node.");
			this.analyzer = analyzer;
			this.classification = classification;
		}

		public Alert(Analyzer analyzer, Classification classification, ToolAlert toolAlert): this(analyzer, classification)
		{
			this.toolAlert = toolAlert;
		}

		public Alert(Analyzer analyzer, Classification classification, OverflowAlert overflowAlert): this(analyzer, classification)
		{
			this.overflowAlert = overflowAlert;
		}

		public Alert(Analyzer analyzer, Classification classification, CorrelationAlert correlationAlert): this(analyzer, classification)
		{
			this.correlationAlert = correlationAlert;
		}

		public Alert(Analyzer analyzer, Classification classification, DetectTime detectTime,
		             AnalyzerTime analyzerTime, Source[] source, Target[] target, Assessment assessment,
		             AdditionalData[] additionalData, string messageId)
			: this(analyzer, classification)
		{
			this.messageId = string.IsNullOrEmpty(messageId) ? "0" : messageId;
			this.detectTime = detectTime;
			this.analyzerTime = analyzerTime;
			this.source = source;
			this.target = target;
			this.assessment = assessment;
			this.additionalData = additionalData;
		}

		public Alert(Analyzer analyzer, Classification classification, DetectTime detectTime,
		             AnalyzerTime analyzerTime, Source[] source, Target[] target, Assessment assessment,
		             ToolAlert toolAlert, AdditionalData[] additionalData, string messageId)
			: this(analyzer, classification, detectTime, analyzerTime, source, target, assessment, additionalData, messageId)
		{
			this.toolAlert = toolAlert;
		}

		public Alert(Analyzer analyzer, Classification classification, DetectTime detectTime,
		             AnalyzerTime analyzerTime, Source[] source, Target[] target, Assessment assessment,
		             OverflowAlert overflowAlert, AdditionalData[] additionalData, string messageId)
			: this(analyzer, classification, detectTime, analyzerTime, source, target, assessment, additionalData, messageId)
		{
			this.overflowAlert = overflowAlert;
		}

		public Alert(Analyzer analyzer, Classification classification, DetectTime detectTime,
		             AnalyzerTime analyzerTime, Source[] source, Target[] target, Assessment assessment,
		             CorrelationAlert correlationAlert, AdditionalData[] additionalData, string messageId)
			: this(analyzer, classification, detectTime, analyzerTime, source, target, assessment, additionalData, messageId)
		{
			this.correlationAlert = correlationAlert;
		}

		public XmlElement ToXml(XmlDocument document)
		{
			XmlElement alertNode = document.CreateElement("idmef:Alert", "http://iana.org/idmef");

			alertNode.SetAttribute("messageId", messageId);

			alertNode.AppendChild(analyzer.ToXml(document));
			alertNode.AppendChild(createTime.ToXml(document));
			alertNode.AppendChild(classification.ToXml(document));
			if (detectTime != null) alertNode.AppendChild(detectTime.ToXml(document));
			if (analyzerTime != null) alertNode.AppendChild(analyzerTime.ToXml(document));
			if (source != null)
				foreach (var src in source)
					if (src != null) alertNode.AppendChild(src.ToXml(document));
			if (target != null)
				foreach (var tgt in target)
					if (tgt != null) alertNode.AppendChild(tgt.ToXml(document));
			if (assessment != null) alertNode.AppendChild(assessment.ToXml(document));

			int subs = 0;
			if (toolAlert != null)
			{
				subs++;
				alertNode.AppendChild(toolAlert.ToXml(document));
			}
			if (overflowAlert != null)
			{
				subs++;
				alertNode.AppendChild(overflowAlert.ToXml(document));
			}
			if (correlationAlert != null)
			{
				subs++;
				alertNode.AppendChild(correlationAlert.ToXml(document));
			}
			if (subs > 1) throw new InvalidOperationException("Only one subclass of Alert can be attached.");

			if (additionalData != null)
				foreach (var ad in additionalData)
					if (ad != null) alertNode.AppendChild(ad.ToXml(document));

			return alertNode;
		}
	}
}