using System.Net.Http;

namespace pt.it4noobs.local.B2B.API.Broker
{
	public class B2BApiBrokerBuilder
	{
		/// <summary>
		/// Builds the specified route.
		/// </summary>
		/// <returns></returns>
		public static B2BApiBroker Build()
		{
			return new B2BApiBroker();
		}

		/// <summary>
		/// Builds the specified route.
		/// </summary>
		/// <param name="httpClient"></param>
		/// <param name="disposeClient">if set to <c>true</c> [dispose client].</param>
		/// <returns></returns>
		public static B2BApiBroker Build(HttpClient httpClient, bool disposeClient = true)
		{
			return new B2BApiBroker(httpClient, disposeClient);
		}
	}
}