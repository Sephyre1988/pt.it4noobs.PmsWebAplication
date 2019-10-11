using SimpleSOAPClient;

namespace pt.it4noobs.local.B2B.API.Broker.Endpoints
{
	public abstract class SoapEndpoint<TBroker> : ISoapEndpoint where TBroker : ISoapClient
	{
		public TBroker Broker { get; }
		protected SoapEndpoint(TBroker broker)
		{
			Broker = broker;
		}
	}
}


