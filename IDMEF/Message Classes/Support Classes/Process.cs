using System;
using System.Xml;

namespace idmef
{
	public class Process
	{
		private readonly string ident = "0";

		public string[] arg;
		public string[] env;
		public string name;
		public string path;
		public Int64? pid;

		public Process(string name)
		{
			if (string.IsNullOrEmpty(name)) throw new ArgumentException("Process must have a name node.");
			this.name = name;
		}

		public Process(string name, Int64? pid, string path, string[] arg, string[] env)
			: this(name)
		{
			this.pid = pid;
			this.path = path;
			this.arg = arg;
			this.env = env;
		}

		public Process(string name, Int64? pid, string path, string[] arg, string[] env, string ident)
			: this(name, pid, path, arg, env)
		{
			this.ident = string.IsNullOrEmpty(ident) ? "0" : ident;
		}

		public XmlElement ToXml(XmlDocument document)
		{
			XmlElement processNode = document.CreateElement("idmef:Process", "http://iana.org/idmef");

			processNode.SetAttribute("ident", ident);

			if (string.IsNullOrEmpty(name)) throw new ArgumentException("Process must have a name node.");

			XmlElement processSubNode = document.CreateElement("idmef:name", "http://iana.org/idmef");
			XmlNode subNode = document.CreateNode(XmlNodeType.Text, "idmef", "name", "http://iana.org/idmef");
			subNode.Value = name;
			processSubNode.AppendChild(subNode);
			processNode.AppendChild(processSubNode);
			if (pid != null)
			{
				processSubNode = document.CreateElement("idmef:pid", "http://iana.org/idmef");
				subNode = document.CreateNode(XmlNodeType.Text, "idmef", "pid", "http://iana.org/idmef");
				subNode.Value = pid.ToString();
				processSubNode.AppendChild(subNode);
				processNode.AppendChild(processSubNode);
			}
			if (!string.IsNullOrEmpty(path))
			{
				processSubNode = document.CreateElement("idmef:path", "http://iana.org/idmef");
				subNode = document.CreateNode(XmlNodeType.Text, "idmef", "path", "http://iana.org/idmef");
				subNode.Value = path;
				processSubNode.AppendChild(subNode);
				processNode.AppendChild(processSubNode);
			}
			if ((arg != null) && (arg.Length > 0))
				foreach (var a in arg)
					if (!string.IsNullOrEmpty(a))
					{
						processSubNode = document.CreateElement("idmef:arg", "http://iana.org/idmef");
						subNode = document.CreateNode(XmlNodeType.Text, "idmef", "arg", "http://iana.org/idmef");
						subNode.Value = a;
						processSubNode.AppendChild(subNode);
						processNode.AppendChild(processSubNode);
					}
			if ((env != null) && (env.Length > 0))
				foreach (var e in env)
					if (!string.IsNullOrEmpty(e))
					{
						processSubNode = document.CreateElement("idmef:env", "http://iana.org/idmef");
						subNode = document.CreateNode(XmlNodeType.Text, "idmef", "env", "http://iana.org/idmef");
						subNode.Value = e;
						processSubNode.AppendChild(subNode);
						processNode.AppendChild(processSubNode);
					}

			return processNode;
		}
	}
}