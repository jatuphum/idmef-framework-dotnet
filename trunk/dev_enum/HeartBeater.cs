using System;
using idmef;

namespace dev_enum
{
	internal class HeartBeater
	{
		public int heartBeatInterval = 300;
		public Analyzer analyzer;

		public HeartBeater(int heartBeatInterval, Analyzer analyzer)
		{
			this.heartBeatInterval = heartBeatInterval;
			this.analyzer = analyzer;
		}

		public void SendHeartBeat(object state)
		{
			IdmefMessage m = new IdmefMessage(new Heartbeat(
				analyzer,
				heartBeatInterval,
				new AnalyzerTime(),
				null,
				Guid.NewGuid().ToString()
			));
			InfoSender.SendHeartbeat(m.ToXml());
		}
	}
}