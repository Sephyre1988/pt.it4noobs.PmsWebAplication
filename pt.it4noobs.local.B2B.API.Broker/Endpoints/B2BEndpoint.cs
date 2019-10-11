namespace pt.it4noobs.local.B2B.API.Broker.Endpoints
{
	public class B2BEndpoint : SoapEndpoint<B2BApiBroker>, IB2BEndpoint
	{
		public B2BEndpoint(B2BApiBroker broker) : base(broker)
		{
		}

	}
}