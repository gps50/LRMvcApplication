using System;
using LRMvcApplication.Domain.Univer;
using LRMvcApplication.Services.Repository;
using LRMvcApplication.Services.Settings;
using LRUnitTests.Services.Settings;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LRUnitTests.Services.Repository
{
	[TestClass]
	public class UnitTest
	{
		[TestMethod]
		public void RepositoryTest()
		{
			var dbContext = RepositoryHepler.Create();
			var cources = dbContext.Set<Cource>();
			var students = dbContext.Set<Student>();
		}
	}
}
