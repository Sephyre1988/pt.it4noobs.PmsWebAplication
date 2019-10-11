using System.Net.Http;
using pt.it4noobs.local.B2B.API.Broker.Endpoints;
using SimpleSOAPClient;

namespace pt.it4noobs.local.B2B.API.Broker
{
	public class B2BApiBroker : SoapClient, IB2BApiBroker
	{
		private IB2BEndpoint B2BEndpoint { get; }

		public string Url { get; set; }

		public B2BApiBroker()
		{
			B2BEndpoint = new B2BEndpoint(this);
		}
		public B2BApiBroker(HttpClient httpClient, bool disposeHttpClient = true) : base(httpClient, disposeHttpClient)
		{
			B2BEndpoint = new B2BEndpoint(this);
		}
	}
}