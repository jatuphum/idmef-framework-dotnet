using System.Xml;

namespace idmef
{
	public class Source
	{
		private readonly string ident = "0";
		public string i_face;

		public Node node;
		public Process process;
		public Service service;
		public UynEnum spoofed = UynEnum.unknown;
		public User user;

		public Source()
		{
		}

		public Source(Node node, User user, Process process, Service service, string ident, UynEnum spoofed, string i_face)
		{
			this.ident = string.IsNullOrEmpty(ident) ? "0" : ident;
			this.spoofed = spoofed;
			this.i_face = i_face;

			this.node = node;
			this.user = user;
			this.process = process;
			this.service = service;
		}

		public XmlElement ToXml(XmlDocument document)
		{
			XmlElement sourceNode = document.CreateElement("idmef:Source", "http://iana.org/idmef");

			sourceNode.SetAttribute("ident", ident);
			sourceNode.SetAttribute("spoofed", spoofed.ToString());
			if (i_face != null) sourceNode.SetAttribute("interface", i_face);

			if (node != null) sourceNode.AppendChild(node.ToXml(document));
			if (user != null) sourceNode.AppendChild(user.ToXml(document));
			if (process != null) sourceNode.AppendChild(process.ToXml(document));
			if (service != null) sourceNode.AppendChild(service.ToXml(document));

			return sourceNode;
		}
	}
}