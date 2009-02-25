using System;
using idmef;

namespace dev_enum
{
	internal class HeartBeater
	{
		public Analyzer analyzer;
		public int heartBeatInterval = 300;

		public HeartBeater(int heartBeatInterval, Analyzer analyzer)
		{
			this.heartBeatInterval = heartBeatInterval;
			this.analyzer = analyzer;
		}

		public void SendHeartBeat(object state)
		{
			var m = new IdmefMessage(new Heartbeat(analyzer, heartBeatInterval, new AnalyzerTime(), null, Guid.NewGuid().ToString()));
			InfoSender.SendHeartbeat(m.ToXml());
		}
	}
}