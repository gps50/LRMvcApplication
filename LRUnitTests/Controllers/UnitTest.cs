using System;
using LRMvcApplication.Controllers;
using LRUnitTests.Services.Univer;
using LRUnitTests.Services.Settings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LRMvcApplication.Domain.Univer;
using System.Web.Mvc;
using LRMvcApplication.Models;
using LRMvcApplication.Services.Univer;
using LRMvcApplication.Services.Settings;
using LRMvcApplication.Domain.Paging;
using System.Net;
using System.Threading;
using LRUnitTests.Models;
using System.Collections.Generic;

namespace LRUnitTests.Controllers
{
	[TestClass]
	public class UnitTest
	{
		IUniverService _univerService;
		ISettingsService _settingsService;
		UniverController _univerController;
		Cource[] _cources;

		[TestInitialize]
		public void Initialize()
		{
			_univerService = UniverHepler.CreateService();
			_settingsService = SettingsHepler.Create();

			_univerController = new UniverController(
				_univerService,
				_settingsService);
		}

		void CreateTestData()
		{
			UniverHepler.ClearCources();

			_cources = new Cource[] 
			{ 
				UniverHepler.CreateCource("title", "description"),
				UniverHepler.CreateCource("title2", "description2"),
				UniverHepler.CreateCource("title3", "description3"),
				UniverHepler.CreateCource("title4", "description4")
			};
		}

		[TestMethod]
		public void UniverControllerCourcesListTest()
		{
			CreateTestData();

			var result = (ViewResult) _univerController.CourcesList();
			var model = (CourcesModel)result.Model;

			var model2 = new CourcesModel
			{
				PageNumber = 1,
				PagesCount = 2
			};
			model2.Cources = new List<CourceModel>();
			for (var i = 0; i < _settingsService.PageSize; i++)
			{
				model2.Cources.Add(CourceModelMap.CreateModel(_cources[i]));
			}

			ModelHelper.AssertAreEqual(model, model2);
			
		}

		[TestMethod]
		public void UniverControllerCourcesListSearchTest()
		{
			CreateTestData();

			// wait for search indexes creation
			Thread.Sleep(10000);

			var result = (ViewResult)_univerController.CourcesList(search: "title");
			var model = (CourcesModel)result.Model;
			
			var model2 = new CourcesModel
			{
				PageNumber = 1,
				PagesCount = 2
			};
			model2.Cources = new List<CourceModel>();
			for (var i = 0; i < _settingsService.PageSize; i++)
			{
				model2.Cources.Add(CourceModelMap.CreateModel(_cources[i]));
			}

			ModelHelper.AssertAreEqual(model, model2);
			
		}

		[TestMethod]
		public void UniverControllerCreateTest()
		{
			UniverHepler.ClearCources();

			var entity = UniverHepler.CreateCource(_univerService);
			var model = CourceModelMap.CreateModel(entity);
			_univerService.DeleteCource(entity);
			
			var result = (HttpStatusCodeResult)_univerController.CreateCource(model);
			Assert.AreEqual(result.StatusCode, 201);

			// wait for search indexes creation
			Thread.Sleep(10000);

			int count;
			var list = _univerService.GetCources(new Page { Index = 0, Size = 1 }, model.Title, out count);

			Assert.AreEqual(count, 1);
			Assert.AreEqual(list.Count, 1);

			var model2 = CourceModelMap.CreateModel(list[0]);
			model2.Id = model.Id;

			ModelHelper.AssertAreEqual(model, model2);
		}

		[TestMethod]
		public void UniverControllerUpdateTest()
		{
			UniverHepler.ClearCources();

			var entity = UniverHepler.CreateCource(_univerService);
			var model = CourceModelMap.CreateModel(entity);

			model.Title = "New title";

			var result = (HttpStatusCodeResult)_univerController.UpdateCource(model);
			Assert.AreEqual(result.StatusCode, 200);

			// wait for search indexes creation
			Thread.Sleep(10000);

			int count;
			var list = _univerService.GetCources(new Page { Index = 0, Size = 1 }, model.Title, out count);

			Assert.AreEqual(count, 1);
			Assert.AreEqual(list.Count, 1);

			var model2 = CourceModelMap.CreateModel(list[0]);
			
			ModelHelper.AssertAreEqual(model, model2);

		}

		[TestMethod]
		public void UniverControllerDeleteTest()
		{
			UniverHepler.ClearCources();

			var entity = UniverHepler.CreateCource(_univerService);
			var model = CourceModelMap.CreateModel(entity);

			var result = (HttpStatusCodeResult)_univerController.DeleteCource(model);
			Assert.AreEqual(result.StatusCode, 200);

			// wait for search indexes creation
			Thread.Sleep(10000);

			int count;
			var list = _univerService.GetCources(new Page { Index = 0, Size = 1 }, model.Title, out count);

			Assert.AreEqual(count, 0);
			Assert.AreEqual(list.Count, 0);
			
		}

	}
}
