using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using pt.it4noobs.local.B2B.API.Broker;
using pt.it4noobs.local.B2B.API.Broker.Factory;

namespace pt.it4noobs.local.Business.IoC.Factories
{
	public class B2BBrokerApiClientFactory : IB2BBrokerApiClientFactory
	{
		private readonly IServiceScopeFactory _serviceScopeFactory;

		public B2BBrokerApiClientFactory(IServiceScopeFactory serviceScopeFactory)
		{
			if (serviceScopeFactory == null) throw new ArgumentNullException(nameof(serviceScopeFactory));

			_serviceScopeFactory = serviceScopeFactory;
		}

		/// <summary>
		/// Executes a given function provided with a scoped <see cref="IB2BApiBroker"/>.
		/// </summary>
		/// <param name="handler">Function to execute.</param>
		/// <param name="address">The address that the <see cref="IB2BApiBroker"/> should target.</param>
		/// <param name="ct">The cancellation token.</param>
		/// <returns>A task to be awaited.</returns>
		public async Task ExecuteAsync(Func<IB2BApiBroker, CancellationToken, Task> handler, string address, CancellationToken ct)
		{
			ThrowOnInvalidArgument(handler, address);

			using (var scope = _serviceScopeFactory.CreateScope())
			{
				var broker = scope.ServiceProvider.GetRequiredService<B2BApiBroker>();
				broker.Url = address;
				await handler(broker, ct);
			}
		}

		/// <summary>
		/// Executes a given function provided with a scoped <see cref="IB2BApiBroker"/> and returns the function result.
		/// </summary>
		/// <typeparam name="T">The type of the function result.</typeparam>
		/// <param name="handler">Function to execute.</param>
		/// <param name="address">The address that the <see cref="IB2BApiBroker"/> should target.</param>
		/// <param name="ct">The cancellation token.</param>
		/// <returns>A task to be awaited.</returns>
		public async Task<T> ExecuteAsync<T>(Func<IB2BApiBroker, CancellationToken, Task<T>> handler, string address, CancellationToken ct)
		{
			ThrowOnInvalidArgument(handler, address);

			using (var scope = _serviceScopeFactory.CreateScope())
			{
				var broker = scope.ServiceProvider.GetRequiredService<B2BApiBroker>();
				broker.Url = address;
				return await handler(broker, ct);
			}
		}

		private static void ThrowOnInvalidArgument(Func<IB2BApiBroker, CancellationToken, Task> handler, string address)
		{
			if (handler == null)
			{
				throw new ArgumentNullException(nameof(handler));
			}
			if (address == null)
			{
				throw new ArgumentNullException(nameof(address));
			}
			if (string.IsNullOrWhiteSpace(address))
			{
				throw new ArgumentException("Invalid address provided.", nameof(address));
			}
		}
	}
}