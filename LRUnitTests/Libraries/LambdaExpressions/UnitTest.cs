using System;
using LRMvcApplication.Libraries.LambdaExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LRUnitTests.Libraries.LambdaExpressions
{
	[TestClass]
	public class UnitTest
	{
		class TestClass
		{
			public string Property1 { get; set;}
			public int Property2 { get; set; }
		}

		[TestMethod]
		public void LambdaExpressions()
		{
			Assert.AreEqual(LambdaExpressionsHelper.GetPropertyName<TestClass, string>(x => x.Property1), "Property1");
			Assert.AreEqual(LambdaExpressionsHelper.GetPropertyName<TestClass, int>(x => x.Property2), "Property2");
		}
	}
}
