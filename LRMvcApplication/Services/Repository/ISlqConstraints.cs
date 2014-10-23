using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;

namespace LRMvcApplication.Services.Repository
{
	[ContractClass(typeof(ISlqConstraintsContract))]
	public interface ISlqConstraints
	{
		string TableName { get; }
		IList<IList<string>> Uniques { get; }
		IList<string> FullTextIndex { get; }
	}
}