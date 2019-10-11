namespace pt.it4noobs.local.Database.UoW.Contracts
{
	public interface IPmsUnitOfWorkFactory
	{
		IPmsUnitOfWorkFactoryScope GetScope();
	}
}