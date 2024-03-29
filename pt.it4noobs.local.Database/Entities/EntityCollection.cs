﻿using System.Collections.Generic;
using SimplePersistence.Model;

namespace pt.it4noobs.local.Database.Entities
{
	public class EntityCollection<T> where T : IEntity
	{
		public IReadOnlyCollection<T> Entities { get; set; }

		public int TotalCount { get; set; }
	}
}