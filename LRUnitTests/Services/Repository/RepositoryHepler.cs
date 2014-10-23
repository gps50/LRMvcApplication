using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LRMvcApplication.Services.Repository;
using LRUnitTests.Services.Settings;

namespace LRUnitTests.Services.Repository
{
	public static class RepositoryHepler
	{
		public static EfDbContext Create()
		{
			var dbContext = new EfDbContext(
				SettingsHepler.Create());

			return dbContext;
		}
	}
}
