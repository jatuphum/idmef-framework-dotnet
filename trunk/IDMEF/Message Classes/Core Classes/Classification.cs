using System;
using System.Xml;

namespace idmef
{
	public class Classification
	{
		private readonly string ident = "0";
		private readonly string text;

		public Reference[] reference;

		public Classification(string text)
		{
			if (string.IsNullOrEmpty(text)) throw new ArgumentException("Classification must have a text node.");
			this.text = text;
		}

		public Classification(Reference[] reference, string ident, string text): this(text)
		{
			this.ident = string.IsNullOrEmpty(ident) ? "0" : ident;
            this.reference = reference;
		}

		public Classification(Reference reference, string ident, string text): this(new[] {reference}, ident, text)
		{
		}

		public XmlElement ToXml(XmlDocument document)
		{
			XmlElement classificationNode = document.CreateElement("idmef:Classification", "http://iana.org/idmef");

			classificationNode.SetAttribute("ident", ident);
			if (string.IsNullOrEmpty(text)) throw new InvalidOperationException("There must be a test attribute.");
			classificationNode.SetAttribute("text", text);

			if (reference != null)
				foreach (var rf in reference)
					if (rf != null) classificationNode.AppendChild(rf.ToXml(document));

			return classificationNode;
		}
	}
}