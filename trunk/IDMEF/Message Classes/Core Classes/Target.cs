using System.Xml;

namespace idmef
{
	public class Target
	{
		public string ident = "0";
		public UynEnum decoy = UynEnum.unknown;
		public string i_face = null;

		public Node node;
		public User user;
		public Process process;
		public Service service;
		public File[] file;

		public Target()
		{
		}

		public Target(Node node, User user, Process process, Service service, File[] file,
					  string ident, UynEnum decoy, string i_face)
		{
			this.ident = ((ident == null) || (ident.Length == 0)) ? "0" : ident;
			this.decoy = decoy;
			this.i_face = i_face;

			this.node = node;
			this.user = user;
			this.process = process;
			this.service = service;
			this.file = file;
		}

		public Target(Node node, User user, Process process, Service service, File file,
					  string ident, UynEnum decoy, string i_face)
			: this(node, user, process, service, new File[] { file }, ident, decoy, i_face)
		{
		}

		public XmlElement ToXml(XmlDocument document)
		{
			XmlElement targetNode = document.CreateElement("idmef:Target", "http://iana.org/idmef");

			targetNode.SetAttribute("ident", ident);
			targetNode.SetAttribute("decoy", decoy.ToString());
			if (i_face != null)
				targetNode.SetAttribute("interface", i_face);

			if (node != null) targetNode.AppendChild(node.ToXml(document));
			if (user != null) targetNode.AppendChild(user.ToXml(document));
			if (process != null) targetNode.AppendChild(process.ToXml(document));
			if (service != null) targetNode.AppendChild(service.ToXml(document));
			if ((file != null) && (file.Length > 0))
				foreach (File f in file)
					if (f != null) targetNode.AppendChild(f.ToXml(document));

			return targetNode;
		}
	}
}