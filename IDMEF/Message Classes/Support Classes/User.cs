using System;
using System.Xml;

namespace idmef
{
	public class User
	{
		public UserCategoryEnum category = UserCategoryEnum.unknown;
		public string ident = "0";

		public UserId[] userId;

		public User(UserId userId)
		{
			if (userId == null) throw new ArgumentException("User must have at least one UserId node.");
			this.userId = new[] {userId};
		}

		public User(UserId[] userId)
		{
			if ((userId == null) || (userId.Length == 0)) throw new ArgumentException("User must have at least one UserId node.");
			this.userId = userId;
		}

		public User(UserId[] userId, string ident, UserCategoryEnum category): this(userId)
		{
			this.ident = string.IsNullOrEmpty(ident) ? "0" : ident;
			this.category = category;
		}

		public User(UserId userId, string ident, UserCategoryEnum category): this(new[] {userId}, ident, category)
		{
		}


		public XmlElement ToXml(XmlDocument document)
		{
			XmlElement userNode = document.CreateElement("idmef:User", "http://iana.org/idmef");

			userNode.SetAttribute("ident", ident);
			userNode.SetAttribute("category", EnumDescription.GetEnumDescription(category));

			if ((userId == null) || (userId.Length == 0))
				throw new InvalidOperationException("User must have at least one UserId node.");
			foreach (var ui in userId)
				if (ui != null) userNode.AppendChild(ui.ToXml(document));

			return userNode;
		}
	}
}