using SimplePersistence.UoW;

namespace pt.it4noobs.local.Database.UoW.Contracts
{
	public interface IPmsUnitOfWork : IUnitOfWork
	{
		IPmsWorkArea PmsWorkArea { get; }
	}
}