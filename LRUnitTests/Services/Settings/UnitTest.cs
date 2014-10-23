using System;
using LRMvcApplication.Services.Settings;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LRUnitTests.Services.Settings
{	
	[TestClass]
	public class SettingsUnitTest
	{
		[TestMethod]
		public void SettingsTest()
		{
			var settings = SettingsHepler.Create();

			Assert.AreEqual(settings.ConnectionString, SettingsHepler.ConnectionString);
			Assert.AreEqual(settings.PageSize, SettingsHepler.PageSize);
			Assert.AreEqual(settings.CacheDurationS, SettingsHepler.CacheDurationS);
		

		}
	}
}
