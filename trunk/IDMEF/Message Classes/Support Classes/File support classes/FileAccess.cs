using System;
using System.Xml;

namespace idmef
{
	public class FileAccess
	{
		private UserId userId = null;
		private Permission[] permission = null;

		public FileAccess(UserId userId, Permission permission)
		{
			if (userId == null)
				throw new ArgumentException("FileAccess must have a UserId node.");
			this.userId = userId;
			this.permission = new Permission[] { permission };
		}
		public FileAccess(UserId userId, Permission[] permission)
		{
			if (userId == null)
				throw new ArgumentException("FileAccess must have a UserId node.");
			if ((permission == null) || (permission.Length == 0))
				throw new ArgumentException("FileAccess must have at least one Permission node.");
			this.userId = userId;
			this.permission = permission;
		}

		public XmlElement ToXml(XmlDocument document)
		{
			XmlElement fileAccessNode = document.CreateElement("idmef:FileAccess", "http://iana.org/idmef");

			if (!((userId.type == UserIdTypeEnum.userPrivs) ||
				(userId.type == UserIdTypeEnum.groupPrivs) ||
				(userId.type == UserIdTypeEnum.otherPrivs)))
				throw new InvalidOperationException(string
					.Format("FileAcces node can't hold UserId of type {0}!", userId));
			fileAccessNode.AppendChild(userId.ToXml(document));
			foreach (Permission p in permission)
			{
				XmlElement fileAccessSubNode = document.CreateElement("idmef:Permission", "http://iana.org/idmef");
				XmlNode subNode = document.CreateNode(XmlNodeType.Text, "idmef", "Permission", "http://iana.org/idmef");
				subNode.Value = EnumDescription.GetEnumDescription(p);
				fileAccessSubNode.AppendChild(subNode);
				fileAccessNode.AppendChild(fileAccessSubNode);
			}

			return fileAccessNode;
		}
	}
}