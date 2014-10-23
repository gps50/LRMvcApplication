using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LRMvcApplication.Services.Settings;

namespace LRUnitTests.Services.Settings
{
	public static class SettingsHepler
	{
		public static string ConnectionString = "UniverConnection";
		public static int PageSize = 2;
		public static int CacheDurationS = 60;

		public static ISettingsService Create()
		{
			var settingsService = new SettingsService(
				ConnectionString,
				PageSize,
				CacheDurationS);

			return settingsService;
		}
	}
}
