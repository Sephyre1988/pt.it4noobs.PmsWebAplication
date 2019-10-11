using NHibernate;
using SimplePersistence.UoW.NH;

namespace pt.it4noobs.local.Database.UoW.Implementation.Mapping
{
	public class PmsDatabaseSession : DatabaseSession, IPmsDatabaseSession
	{
		public PmsDatabaseSession(ISession session) : base(session)
		{
		}
	}
}