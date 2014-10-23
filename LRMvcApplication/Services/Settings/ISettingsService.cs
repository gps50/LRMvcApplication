using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;

namespace LRMvcApplication.Services.Settings
{
	[ContractClass(typeof(ISettingsServiceContract))]
	public interface ISettingsService
	{
		#region data base

		string ConnectionString { get; }

		#endregion


		#region UI

		int PageSize { get; }

		#endregion


		#region UI

		int CacheDurationS { get; }

		#endregion

	}
}