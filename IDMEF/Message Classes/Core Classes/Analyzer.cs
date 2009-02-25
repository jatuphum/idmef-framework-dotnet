using System.Xml;

namespace idmef
{
	public class Analyzer
	{
		private readonly string analyzerId;
		public Analyzer analyzer;
		public string clazz;
		public string manufacturer;
		public string model;
		public string name;
		public Node node;
		public string osType;
		public string osVersion;

		public Process process;
		public string version;

		public Analyzer(string analyzerId)
		{
			this.analyzerId = analyzerId;
		}

		public Analyzer(Node node, Process process, Analyzer analyzer, string analyzerId): this(analyzerId)
		{
			this.node = node;
			this.process = process;
			this.analyzer = analyzer;
		}

		public Analyzer(Node node, Process process, Analyzer analyzer,
		                string analyzerId, string name, string manufacturer, string model, string version,
		                string clazz, string osType, string osVersion)
			: this(node, process, analyzer, analyzerId)
		{
			this.name = name;
			this.manufacturer = manufacturer;
			this.model = model;
			this.version = version;
			this.clazz = clazz;
			this.osType = osType;
			this.osVersion = osVersion;
		}

		public XmlElement ToXml(XmlDocument document)
		{
			XmlElement analyzerNode = document.CreateElement("idmef:Analyzer", "http://iana.org/idmef");

			if (!string.IsNullOrEmpty(analyzerId)) analyzerNode.SetAttribute("analyzerid", analyzerId);
			if (!string.IsNullOrEmpty(name)) analyzerNode.SetAttribute("name", name);
			if (!string.IsNullOrEmpty(manufacturer)) analyzerNode.SetAttribute("manufacturer", manufacturer);
			if (!string.IsNullOrEmpty(model)) analyzerNode.SetAttribute("model", model);
			if (!string.IsNullOrEmpty(clazz)) analyzerNode.SetAttribute("class", clazz);
			if (!string.IsNullOrEmpty(osType)) analyzerNode.SetAttribute("ostype", osType);
			if (!string.IsNullOrEmpty(osVersion)) analyzerNode.SetAttribute("osversion", osVersion);

			if (node != null) analyzerNode.AppendChild(node.ToXml(document));
			if (process != null) analyzerNode.AppendChild(process.ToXml(document));
			if (analyzer != null) analyzerNode.AppendChild(analyzer.ToXml(document));

			return analyzerNode;
		}
	}
}