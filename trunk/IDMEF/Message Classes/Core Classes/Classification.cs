using System;
using System.Xml;

namespace idmef
{
	public class Classification
	{
		private string ident = "0";
		private string text = null;

		public Reference[] reference = null;

		public Classification(string text)
		{
			if ((text == null) || (text.Length == 0))
				throw new ArgumentException("Classification must have a text node.");
			this.text = text;
		}
		public Classification(Reference[] reference, string ident, string text)
			:this(text)
		{
			this.ident = ((ident == null) || (ident.Length == 0)) ? "0" : ident;

			this.reference = reference;
		}

		public Classification(Reference reference, string ident, string text)
			: this(new Reference[] {reference}, ident, text)
		{
		}

		public XmlElement ToXml(XmlDocument document)
		{
			XmlElement classificationNode = document.CreateElement("idmef:Classification", "http://iana.org/idmef");

			classificationNode.SetAttribute("ident", ident);
			if ((text == null) || (text.Length == 0))
				throw new InvalidOperationException("There must be a test attribute.");
			classificationNode.SetAttribute("text", text);

			if (reference != null)
				foreach (Reference rf in reference)
					if (rf != null)
						classificationNode.AppendChild(rf.ToXml(document));

			return classificationNode;
		}

	}
}