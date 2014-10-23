using System;
using LRMvcApplication.Models;
using LRUnitTests.Services.Univer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LRUnitTests.Models
{
	[TestClass]
	public class UnitTest
	{
		[TestMethod]
		public void ModelMap()
		{
			var entity = UniverHepler.CreateCource();

			var model = CourceModelMap.CreateModel(entity);

			CourceModelMap.UpdateEntity(model, entity);

			var model2 = CourceModelMap.CreateModel(entity);

			ModelHelper.AssertAreEqual(model, model2);
			
		}
	}
}
