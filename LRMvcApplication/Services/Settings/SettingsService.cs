using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;

namespace LRMvcApplication.Services.Settings
{
	public class SettingsService : ISettingsService
	{
		#region ctor

		public SettingsService(
			string connectionString,
			int pageSize,
			int cacheDurationS)
		{
			Contract.Requires(!string.IsNullOrEmpty(connectionString));
			Contract.Requires(pageSize > 0);
			Contract.Requires(cacheDurationS > 0);

			this._connectionString = connectionString;
			this._pageSize = pageSize;
			this._cacheDurationS = cacheDurationS;
		}

		#endregion


		#region data base

		private string _connectionString;
		public string ConnectionString 
		{
			get
			{
				return _connectionString;
			}
		}

		#endregion


		#region UI

		private int _pageSize;
		public int PageSize
		{
			get
			{
				return _pageSize;
			}
		}

		#endregion


		#region UI

		private int _cacheDurationS;
		public int CacheDurationS
		{
			get
			{
				return _cacheDurationS;
			}
		}

		#endregion


	}
}