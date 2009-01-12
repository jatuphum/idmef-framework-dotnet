using System;
using System.Xml;

namespace idmef
{
	public class WebService
	{
		private string url = null;
		public string cgi = null;
		public string httpMethod = null;
		public string[] arg = null;

		public WebService(string url)
		{
			if ((url == null) || (url.Length == 0))
				throw new ArgumentException("WebService must have an url node.");
			this.url = url;
		}

		public WebService(string url, string cgi, string httpMethod, string[] arg)
			: this(url)
		{
			this.cgi = cgi;
			this.httpMethod = httpMethod;
			this.arg = arg;
		}

		public XmlElement ToXml(XmlDocument document)
		{
			XmlElement webServiceNode = document.CreateElement("idmef:WebService", "http://iana.org/idmef");

			XmlElement webServiceSubNode = document.CreateElement("idmef:url", "http://iana.org/idmef");
			XmlNode subNode = document.CreateNode(XmlNodeType.Text, "idmef", "url", "http://iana.org/idmef");
			subNode.Value = url;
			webServiceSubNode.AppendChild(subNode);
			webServiceNode.AppendChild(webServiceSubNode);
			if ((cgi != null) && (cgi.Length > 0))
			{
				webServiceNode = document.CreateElement("idmef:cgi", "http://iana.org/idmef");
				subNode = document.CreateNode(XmlNodeType.Text, "idmef", "cgi", "http://iana.org/idmef");
				subNode.Value = cgi;
				webServiceNode.AppendChild(subNode);
				webServiceNode.AppendChild(webServiceNode);
			}
			if ((httpMethod != null) && (httpMethod.Length > 0))
			{
				webServiceNode = document.CreateElement("idmef:http-method", "http://iana.org/idmef");
				subNode = document.CreateNode(XmlNodeType.Text, "idmef", "http-method", "http://iana.org/idmef");
				subNode.Value = httpMethod;
				webServiceNode.AppendChild(subNode);
				webServiceNode.AppendChild(webServiceNode);
			}
			if ((httpMethod != null) && (httpMethod.Length > 0))
				foreach (string a in arg)
					if ((a != null) && (a.Length > 0))
					{
						webServiceNode = document.CreateElement("idmef:arg", "http://iana.org/idmef");
						subNode = document.CreateNode(XmlNodeType.Text, "idmef", "arg", "http://iana.org/idmef");
						subNode.Value = a;
						webServiceNode.AppendChild(subNode);
						webServiceNode.AppendChild(webServiceNode);
					}

			return webServiceNode;
		}
	}
}