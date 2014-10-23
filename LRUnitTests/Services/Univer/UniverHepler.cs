using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KellermanSoftware.CompareNetObjects;
using LRMvcApplication.Domain.Univer;
using LRMvcApplication.Libraries.LambdaExpressions;
using LRMvcApplication.Services.Repository;
using LRMvcApplication.Services.Univer;
using LRUnitTests.Services.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LRUnitTests.Services.Univer
{
	public static class UniverHepler
	{
		private static Random _random = new Random();

		public static IUniverService CreateService(EfDbContext dbContext = null)
		{
			return new UniverService(
				dbContext ?? RepositoryHepler.Create());
		}

		public static Cource CreateCource(
			string title = null,
			string desctiption = null)
		{
			return CreateCource(
				CreateService(),
				title,
				desctiption);
		}

		public static Cource CreateCource(
			IUniverService univerService,
			string title = null,
			string desctiption = null)
		{
			return univerService.CreateCource(
				title ?? string.Format("{0, 9}", _random.Next()),
				desctiption ?? string.Format("{0, 9}", _random.Next()));
		}

		public static void UpdateCource(
			IUniverService univerService,
			Cource cource,
			string title = null,
			string description = null)
		{
			cource.Title = title ?? string.Format("{0, 9}", _random.Next());
			cource.Description = description ?? string.Format("{0, 9}", _random.Next());
			univerService.UpdateCource(cource);
		}

		public static void AssertAreEqual(Cource cource, Cource cource2)
		{
			var config = new ComparisonConfig()
			{
				ComparePrivateFields = false,
				ComparePrivateProperties = false,
				IgnoreObjectTypes = true,
				CompareFields = false,
				CompareReadOnly = false,
				CompareStaticProperties = false,
				CompareStaticFields = false
			};
			config.ClassTypesToIgnore.Add(typeof(IList<>));
			config.MembersToIgnore.Add(
				LambdaExpressionsHelper.GetPropertyName<Cource, IList<Student>>(x => x.Students));

			if (!(new CompareLogic(config)).Compare(cource, cource2).AreEqual)
			{
				throw new AssertFailedException();
			}			
		}

		public static void ClearCources(DbContext dbContext = null)
		{
			(dbContext ?? RepositoryHepler.Create()).Database.ExecuteSqlCommand(
				string.Format(
					"delete from {0}",
					(new CourceMap()).TableName));
		}
	}
}
