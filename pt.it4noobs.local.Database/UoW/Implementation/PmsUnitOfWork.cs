using pt.it4noobs.local.Database.UoW.Contracts;
using pt.it4noobs.local.Database.UoW.Contracts.Area.Pms;
using SimplePersistence.UoW.NH;

namespace pt.it4noobs.local.Database.UoW.Implementation
{
	public class PmsUnitOfWork : NHUnitOfWork, IPmsUnitOfWork
	{
		public PmsUnitOfWork(IPmsDatabaseSession session, IPmsWorkArea pms)
			: base(session)
		{
			PmsWorkArea = pms;
		}

		#region Implementation of IPmsUnitOfWork

		public IPmsWorkArea PmsWorkArea { get; }

		#endregion

	}
}