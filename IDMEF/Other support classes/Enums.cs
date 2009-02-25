using System;
using System.ComponentModel;
using System.Reflection;

namespace idmef
{
	public enum UynEnum
	{
		unknown = 0,
		yes = 1,
		no = 2
	}

	public enum SeverityEnum
	{
		undefined = -1,
		info = 0,
		low = 1,
		medium = 2,
		high = 3
	}

	public enum CompletionEnum
	{
		undefined = -1,
		failed = 0,
		succeeded = 1
	}

	public enum AssessmentTypeEnum
	{
		admin = 0,
		dos = 1,
		file = 2,
		recon = 3,
		user = 4,
		other = 5
	}

	public enum ActionCategoryEnum
	{
		[Description("block-installed")]
		blockInstalled = 0,
		[Description("notification-sent")]
		notificationSent = 1,
		[Description("taken-offline")]
		takenOffline = 2,
		other = 3
	}

	public enum ConfidenceRatingEnum
	{
		low = 0,
		medium = 1,
		high = 2,
		numeric = 3
	}

	public enum OriginEnum
	{
		unknown = 0,
		[Description("vendor-specific")]
		vendorSpecific = 1,
		[Description("user-specific")]
		userSpecific = 2,
		bagtraqid = 3,
		cve = 4,
		osvdb = 5
	}

	public enum NodeCategoryEnum
	{
		unknown = 0,
		ads = 1,
		afs = 2,
		coda = 3,
		dfs = 4,
		dns = 5,
		hosts = 6,
		kerberos = 7,
		nds = 8,
		nis = 9,
		nisplus = 10,
		nt = 11,
		wfw = 12
	}

	public enum AddressCategoryEnum
	{
		unknown = 0,
		atm = 1,
		[Description("e-mail")]
		email = 2,
		[Description("lotus-notes")]
		lotusNotes = 3,
		mac = 4,
		sna = 5,
		vm = 6,
		[Description("ipv4-addr")]
		ipv4Addr = 7,
		[Description("ipv4-addr-hex")]
		ipv4AddrHex = 8,
		[Description("ipv4-net")]
		ipv4Net = 9,
		[Description("ipv4-net-mask")]
		ipv4NetMask = 10,
		[Description("ipv6-addr")]
		ipv6Addr = 11,
		[Description("ipv6-addr-hex")]
		ipv6AddrHex = 12,
		[Description("ipv6-net")]
		ipv6Net = 13,
		[Description("ipv6-net-mask")]
		ipv6NetMask = 14
	}

	public enum UserCategoryEnum
	{
		unknown = 0,
		application = 1,
		[Description("os-device")]
		osDevice = 2
	}

	public enum UserIdTypeEnum
	{
		[Description("current-user")]
		currentUser = 0,
		[Description("original-user")]
		originalUser = 1,
		[Description("target-user")]
		targetUser = 2,
		[Description("user-privs")]
		userPrivs = 3,
		[Description("current-group")]
		currentGroup = 4,
		[Description("group-privs")]
		groupPrivs = 5,
		[Description("other-privs")]
		otherPrivs = 6,
	}

	public enum FileCategoryEnum
	{
		unknown = -1,
		current = 0,
		original = 1
	}

	public enum FileSystemTypeEnum
	{
		unknown = -1,
		ufs = 0,
		efs = 1,
		nfs = 2,
		afs = 3,
		ntfs = 4,
		fat16 = 5,
		fat32 = 6,
		pcfs = 7,
		joliet = 8,
		iso9660 = 9
	}

	public enum Permission
	{
		noAccess = 0,
		read = 1,
		write = 2,
		execute = 3,
		search = 4,
		delete = 5,
		executeAs = 6,
		changePermissions = 7,
		takeOwnership = 8
	}

	public enum LinkageCategoryEnum
	{
		unknown = -1,
		[Description("hard-link")]
		hardLink = 0,
		[Description("mount-point")]
		mountpoint = 0,
		[Description("reparse-point")]
		reparsePoint = 0,
		shortcut = 0,
		stream = 0,
		[Description("symbolic-link")]
		symbolicLink = 0,
	}

	public enum ChecksumAlgorithmEnum
	{
		MD4 = 0,
		MD5 = 1,
		SHA1 = 2,
		[Description("SHA2-256")]
		SHA2_256 = 3,
		[Description("SHA2-384")]
		SHA2_384 = 4,
		[Description("SHA2-512")]
		SHA2_512 = 5,
		[Description("CRC-32")]
		CRC32 = 6,
		Haval = 7,
		Tiger = 8,
		Gost = 9
	}

	internal enum ADEnum
	{
		boolean,
		[Description("byte")]
		adByte,
		character,
		[Description("date-time")]
		datetime,
		integer,
		ntpstamp,
		portlist,
		real,
		[Description("string")]
		adString,
		[Description("byte-string")]
		byteString,
		xml
	}

	public class EnumDescription
	{
		public static string GetEnumDescription(Enum value)
		{
			FieldInfo fi = value.GetType().GetField(value.ToString());
			var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof (DescriptionAttribute), false);
			return (attributes.Length > 0) ? attributes[0].Description : value.ToString();
		}
	}
}