using System.Xml;

namespace idmef
{
	public class Analyzer
	{
		private string analyzerId = null;
		public string name = null;
		public string manufacturer = null;
		public string model = null;
		public string version = null;
		public string clazz = null;
		public string osType = null;
		public string osVersion = null;

		public Node node = null;
		public Process process = null;
		public Analyzer analyzer = null;

		public Analyzer(string analyzerId)
		{
			this.analyzerId = analyzerId;
		}

		public Analyzer(Node node, Process process, Analyzer analyzer, string analyzerId)
			: this(analyzerId)
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

			if ((analyzerId != null) && (analyzerId.Length>0))
				analyzerNode.SetAttribute("analyzerid", analyzerId);
			if ((name != null) && (name.Length > 0))
				analyzerNode.SetAttribute("name", name);
			if ((manufacturer != null) && (manufacturer.Length > 0))
				analyzerNode.SetAttribute("manufacturer", manufacturer);
			if ((model != null) && (model.Length > 0))
				analyzerNode.SetAttribute("model", model);
			if ((clazz != null) && (clazz.Length > 0))
				analyzerNode.SetAttribute("class", clazz);
			if ((osType != null) && (osType.Length > 0))
				analyzerNode.SetAttribute("ostype", osType);
			if ((osVersion != null) && (osVersion.Length > 0))
				analyzerNode.SetAttribute("osversion", osVersion);

			if (node != null) analyzerNode.AppendChild(node.ToXml(document));
			if (process != null) analyzerNode.AppendChild(process.ToXml(document));
			if (analyzer != null) analyzerNode.AppendChild(analyzer.ToXml(document));

			return analyzerNode;
		}
	}
}