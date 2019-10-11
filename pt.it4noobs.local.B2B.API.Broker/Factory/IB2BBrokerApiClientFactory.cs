using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace pt.it4noobs.local.B2B.API.Broker.Factory
{
	public interface IB2BBrokerApiClientFactory
	{
		Task ExecuteAsync(Func<IB2BApiBroker, CancellationToken, Task> handler, string address, CancellationToken ct);
		Task<T> ExecuteAsync<T>(Func<IB2BApiBroker, CancellationToken, Task<T>> handler, string address, CancellationToken ct);

	}
}
