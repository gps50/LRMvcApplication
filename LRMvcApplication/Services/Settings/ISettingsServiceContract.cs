using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;

namespace LRMvcApplication.Services.Settings
{
	[ContractClassFor(typeof(ISettingsService))]
	public abstract class ISettingsServiceContract : ISettingsService
	{
		#region data base

		public string ConnectionString 
		{
			get 
			{
				Contract.Ensures(!string.IsNullOrEmpty(Contract.Result<string>()));
				return default(string);
			}
		}

		#endregion


		#region UI

		public int PageSize
		{
			get
			{
				Contract.Ensures(Contract.Result<int>() > 0);
				return default(int);
			}
		}

		#endregion


		#region UI

		public int CacheDurationS 
		{
			get
			{
				Contract.Ensures(Contract.Result<int>() > 0);
				return default(int);
			}
		}

		#endregion

	}
}