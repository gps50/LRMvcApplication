using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;

namespace LRMvcApplication.Services.Repository
{
	[ContractClassFor(typeof(ISlqConstraints))]
	public abstract class ISlqConstraintsContract : ISlqConstraints
	{
		public string TableName
		{
			get
			{
				Contract.Ensures(!string.IsNullOrEmpty(Contract.Result<string>()));

				return default(string);
			}
		}

		public IList<IList<string>> Uniques
		{
			get
			{
				return default(IList<IList<string>>);
			}
		}

		public IList<string> FullTextIndex
		{
			get
			{
				return default(IList<string>);
			}
		}

	}
}