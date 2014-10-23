using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KellermanSoftware.CompareNetObjects;
using LRMvcApplication.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LRUnitTests.Models
{
	public static class ModelHelper
	{
		public static void AssertAreEqual(CourceModel cource, CourceModel cource2)
		{
			if (!(new CompareLogic()).Compare(cource, cource2).AreEqual)
			{
				throw new AssertFailedException();
			}
		}

		public static void AssertAreEqual(CourcesModel cource, CourcesModel cource2)
		{
			var config = new ComparisonConfig()
			{
				IgnoreCollectionOrder = true,
				IgnoreObjectTypes = true,
				ComparePrivateFields = false,
				ComparePrivateProperties = false
			};

			if (!(new CompareLogic(config)).Compare(cource, cource2).AreEqual)
			{
				throw new AssertFailedException();
			}
		}
	}
}
