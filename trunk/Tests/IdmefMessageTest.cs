using System;
using idmef;
using NUnit.Framework;
using Action=idmef.Action;

namespace Tests
{
	[TestFixture]
	public class XmlSerializationTest
	{
		[Test]
		public void AlertToXmlFullTest()
		{
			var message = new IdmefMessage(
				new Alert(
					new Analyzer(
						new Node(
							"node_location",
							"node_name",
							new[]
								{
									new Address(
										"192.168.0.1",
										"255.255.255.0",
										Guid.NewGuid().ToString(),
										AddressCategoryEnum.ipv4Addr,
										"home",
										0
										)
								},
							Guid.NewGuid().ToString(),
							NodeCategoryEnum.nt
							),
						new Process(
							"process_name",
							1234,
							"s:\\ome\\path",
							new[] {"-h"},
							new[] {"ver=\"7.0\""},
							Guid.NewGuid().ToString()
							),
						new Analyzer(Guid.NewGuid().ToString()),
						Guid.NewGuid().ToString()
						),
					new Classification(
						new Reference(
							OriginEnum.userSpecific,
							"test_reference",
							"reference_name",
							"http://127.0.0.1"
							),
						Guid.NewGuid().ToString(),
						"Te&t <la&&ifi<ati()n."
						),
					new DetectTime(),
					new AnalyzerTime(),
					new[]
						{
							new Source(
								new Node("node_name_2"),
								new User(
									new UserId(
										"userid_name",
										13,
										Guid.NewGuid().ToString(),
										UserIdTypeEnum.currentUser,
										"userid_tty"
										),
									Guid.NewGuid().ToString(),
									UserCategoryEnum.application
									),
								new Process("process_name_2"),
								new Service(
									"http",
									80,
									null,
									"TCP",
									new SnmpService(
										"1.2.3.4.5",
										3,
										0,
										"security_name",
										5,
										"context_name",
										"engine_id",
										"command"
										),
									null,
									Guid.NewGuid().ToString()
									),
								Guid.NewGuid().ToString(),
								UynEnum.no,
								"some interface"
								)
						},
					new[]
						{
							new Target(
								new Node("node_name_3"),
								new User(new UserId("userid_name_2", 3144)),
								new Process("process_name_2"),
								new Service("https", 443),
								new File(
									"filename",
									"/file/path/",
									DateTime.Now,
									DateTime.Now,
									DateTime.Now,
									12353675,
									025973598612378656,
									new[]
										{
											new FileAccess(
												new UserId(
													"userid_name_3",
													11144,
													Guid.NewGuid().ToString(),
													UserIdTypeEnum.userPrivs,
													null
													),
												Permission.read
												)
										},
									new[]
										{
											new Linkage(
												"link_name",
												"..\\path",
												new File("fname", "fpath", FileCategoryEnum.original),
												LinkageCategoryEnum.symbolicLink
												)
										},
									new Inode(
										DateTime.Now,
										254546,
										0,
										1,
										0,
										1
										),
									new[]
										{
											new Checksum(
												"0x12345678",
												"0x00000000",
												ChecksumAlgorithmEnum.CRC32
												)
										},
									Guid.NewGuid().ToString(),
									FileCategoryEnum.current,
									FileSystemTypeEnum.ntfs,
									"text/html"
									),
								Guid.NewGuid().ToString(),
								UynEnum.yes,
								"yet_another_interface"
								)
						},
					new Assessment(
						new Impact(
							SeverityEnum.info,
							CompletionEnum.succeeded,
							AssessmentTypeEnum.admin,
							"assessment_description"
							),
						new Action(
							ActionCategoryEnum.notificationSent,
							"action_description"
							),
						new Confidence(ConfidenceRatingEnum.low)
						),
					new ToolAlert(
						"toolalert_name",
						"tool.exe",
						new[] {new AlertIdent("value", "another_analyzer_id")}
						),
					new[]
						{
							new AdditionalData(
								"additional_data_test",
								new byte[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 0}
								)
						},
					Guid.NewGuid().ToString()
					)
				);
			Console.WriteLine(message.ToXml().OuterXml);
		}

		[Test]
		public void AlertToXmlTest()
		{
			var message = new IdmefMessage(
				new Alert(
					new Analyzer(Guid.NewGuid().ToString()),
					new Classification("Te&t <la&&ifi<ati()n."),
					null,
					null,
					null,
					null,
					null,
					null,
					null
					)
				);
			Console.WriteLine(message.ToXml().OuterXml);
		}

		[Test]
		public void EnumTest()
		{
			Console.WriteLine(EnumDescription.GetEnumDescription(ActionCategoryEnum.notificationSent));
		}

		[Test]
		public void IdmefMessageToXmlTest()
		{
			var message = new IdmefMessage((Alert)null);
			Console.WriteLine(message.ToXml().OuterXml);
		}

		[Test]
		public void NtpStampTest()
		{
			string stamp = NtpStamp.Convert(DateTime.Parse("2000-03-09T10:01:25.93464Z")).ToString();
			Assert.AreEqual("0xbc71f4f5.0xef449129", stamp);
		}
	}
}