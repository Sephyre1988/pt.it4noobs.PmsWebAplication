using System;

namespace pt.it4noobs.local.Database.UoW.Contracts
{
	public interface IPmsUnitOfWorkFactoryScope : IDisposable
	{
		IPmsUnitOfWork UnitOfWork { get; }
	}
}