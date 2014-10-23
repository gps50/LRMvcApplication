using System;
using System.Data.Entity;
using LRMvcApplication.Domain.Paging;
using LRMvcApplication.Domain.Univer;
using LRMvcApplication.Services.Repository;
using LRMvcApplication.Services.Settings;
using LRMvcApplication.Services.Univer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading;
using LRMvcApplication.Services.UniverExceptions;
using LRUnitTests.Services.Repository;
using LRUnitTests.Services.Settings;

namespace LRUnitTests.Services.Univer
{	
	[TestClass]
	public class UnitTest
	{
		[TestMethod]
		public void CreateUniverService()
		{
			var cource = UniverHepler.CreateCource();

			var cource2 = UniverHepler.CreateService().GetCource(cource.Id);

			UniverHepler.AssertAreEqual(cource, cource2);

		}

		[TestMethod]
		[ExpectedException(typeof(System.Data.Entity.Infrastructure.DbUpdateException))]
		public void CourceCreateNotUniqueTitle()
		{
			var cource = UniverHepler.CreateCource();
			cource = UniverHepler.CreateCource(cource.Title);

		}

		[TestMethod]
		[ExpectedException(typeof(TitleException))]
		public void CourceCreateWrongTitleFormat()
		{
			UniverHepler.ClearCources();
			var cource = UniverHepler.CreateCource("t");

		}

		[TestMethod]
		[ExpectedException(typeof(DescriptionException))]
		public void CourceCreateWrongDescriptionFormat()
		{
			UniverHepler.ClearCources();
			var cource = UniverHepler.CreateCource("title", "description01234567890123456789");

		}
		
		[TestMethod]
		public void CourceGet()
		{
			var cource = UniverHepler.CreateCource();

			var cource2 = UniverHepler.CreateService().GetCource(cource.Id);

			UniverHepler.AssertAreEqual(cource, cource2);

		}

		[TestMethod]
		public void CourceGetList()
		{
			UniverHepler.ClearCources();

			var cources = new Cource[] 
			{ 
				UniverHepler.CreateCource("title", "description"),
				UniverHepler.CreateCource("title2", "description2"),
				UniverHepler.CreateCource("title3", "description3"),
				UniverHepler.CreateCource("title4", "description4")
			};

			var page_size = SettingsHepler.Create().PageSize;
			var pages_count = (int)Math.Ceiling((double)cources.Length / (double)page_size);

			

			for (var i = 0; i < pages_count; i++)
			{
				int totalCount;
				var list = UniverHepler.CreateService().GetCources(
					new Page { Index = i, Size = page_size },
					null, 
					out totalCount);
				Assert.AreEqual(totalCount, cources.Length);
				for (var j = 0; j < list.Count; j++)
				{
					UniverHepler.AssertAreEqual(list[j], cources[i * page_size + j]);
				}				
			}
		}

		[TestMethod]
		public void CourceSearch()
		{
			UniverHepler.ClearCources();

			var cources = new Cource[] 
			{ 
				UniverHepler.CreateCource("title", "description"),
				UniverHepler.CreateCource("title2", "description2"),
				UniverHepler.CreateCource("title3", "description3"),
				UniverHepler.CreateCource("title4", "description4")
			};


			// wait for search indexes creation
			Thread.Sleep(10000);

			var page_size = SettingsHepler.Create().PageSize;
			var pages_count = (int)Math.Ceiling((double)cources.Length / (double)page_size);

			for (var i = 0; i < pages_count; i++)
			{
				int totalCount;
				var list = UniverHepler.CreateService().GetCources(
					new Page { Index = i, Size = page_size },
					"title",
					out totalCount);
				Assert.AreEqual(totalCount, cources.Length);
				for (var j = 0; j < list.Count; j++)
				{
					UniverHepler.AssertAreEqual(list[j], cources[i * page_size + j]);
				}
			}

			for (var i = 0; i < pages_count; i++)
			{
				int totalCount;
				var list = UniverHepler.CreateService().GetCources(
					new Page { Index = i, Size = page_size },
					"description",
					out totalCount);
				Assert.AreEqual(totalCount, cources.Length);
				for (var j = 0; j < list.Count; j++)
				{
					UniverHepler.AssertAreEqual(list[j], cources[i * page_size + j]);
				}
			}

		}
		
		[TestMethod]
		public void CourceUpdate()
		{
			var univerService = UniverHepler.CreateService();
			var cource = UniverHepler.CreateCource(univerService);

			UniverHepler.UpdateCource(univerService, cource);

			var cource2 = UniverHepler.CreateService().GetCource(cource.Id);

			UniverHepler.AssertAreEqual(cource, cource2);

		}

		[TestMethod]
		[ExpectedException(typeof(System.Data.Entity.Infrastructure.DbUpdateException))]
		public void CourceUpdateNotUniqueTitle()
		{
			var univerService = UniverHepler.CreateService();
			var cource = UniverHepler.CreateCource(univerService);
			var cource2 = UniverHepler.CreateCource(univerService);
			UniverHepler.UpdateCource(univerService, cource, cource2.Title);
		}

		[TestMethod]
		[ExpectedException(typeof(TitleException))]
		public void CourceUpdateWrongTitleFormat()
		{
			UniverHepler.ClearCources();
			var univerService = UniverHepler.CreateService();
			var cource = UniverHepler.CreateCource(univerService);
			UniverHepler.UpdateCource(univerService, cource, "t");
		}

		[TestMethod]
		[ExpectedException(typeof(DescriptionException))]
		public void CourceUpdateWrongDescriptionFormat()
		{
			UniverHepler.ClearCources();
			var univerService = UniverHepler.CreateService();
			var cource = UniverHepler.CreateCource(univerService);
			UniverHepler.UpdateCource(univerService, cource, "title", "description01234567890123456789");

		}

		[TestMethod]
		public void CourceDelete()
		{
			var cource = UniverHepler.CreateCource();

			var univerService = UniverHepler.CreateService();
			cource = univerService.GetCource(cource.Id);
			univerService.DeleteCource(cource);

			var cource2 = UniverHepler.CreateService().GetCource(cource.Id);

			Assert.IsNull(cource2);
			
		}
		
	}
}
