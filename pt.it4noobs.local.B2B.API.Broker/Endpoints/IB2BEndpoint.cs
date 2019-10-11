using System.Threading;
using System.Threading.Tasks;

namespace pt.it4noobs.local.B2B.API.Broker.Endpoints
{
	//this implements all of API tasks
	public interface IB2BEndpoint : ISoapEndpoint
	{
		//implements like this:
		//Task<TheResponseModel> GetSomethingAsync(TheRequestModel request, CancellationToken ct);
	}
}