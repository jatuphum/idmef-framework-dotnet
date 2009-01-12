using System;
using System.Xml;

namespace idmef
{
	public class User
	{
		public string ident = "0";
		public UserCategoryEnum category = UserCategoryEnum.unknown;

		public UserId[] userId;

		public User(UserId userId)
		{
			if (userId == null)
				throw new ArgumentException("User must have at least one UserId node.");
			this.userId = new UserId[]{userId};
		}
		public User(UserId[] userId)
		{
			if ((userId == null)||(userId.Length == 0))
				throw new ArgumentException("User must have at least one UserId node.");
			this.userId = userId;
		}
		public User(UserId[] userId, string ident, UserCategoryEnum category)
			: this(userId)
		{
			this.ident = ((ident == null) || (ident.Length == 0)) ? "0" : ident;
			this.category = category;
		}
		public User(UserId userId, string ident, UserCategoryEnum category)
			: this(new UserId[]{ userId }, ident, category)
		{
		}


		public XmlElement ToXml(XmlDocument document)
		{
			XmlElement userNode = document.CreateElement("idmef:User", "http://iana.org/idmef");

			userNode.SetAttribute("ident", ident);
			userNode.SetAttribute("category", EnumDescription.GetEnumDescription(category));

			if ((userId == null) || (userId.Length == 0))
				throw new InvalidOperationException("User must have at least one UserId node.");
			foreach (UserId ui in userId)
				if (ui != null) userNode.AppendChild(ui.ToXml(document));

			return userNode;
		}
	}
}