using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using LRMvcApplication.Services.Settings;

namespace LRMvcApplication.Filters
{
	public class NoCacheAttribute : OutputCacheAttribute
	{
		public NoCacheAttribute()
		{
			Duration = 0;
			NoStore = true;
			Location = System.Web.UI.OutputCacheLocation.None;
			VaryByParam = "None";
		}
	}


#if DEBUG

	public class CacheAttribute : NoCacheAttribute
	{
	}

#else

	public class CacheAttribute : OutputCacheAttribute
	{
		public CacheAttribute()
		{
			Duration = DependencyResolverConfig.DI.Resolve<ISettingsService>(HttpContext.Current).CacheDurationS;
			VaryByParam = "*";
			Location = System.Web.UI.OutputCacheLocation.Any;
		}
	}

#endif
    
    
}
