using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRMvcApplication.DependencyResolver
{
	[ContractClass(typeof(IMvcDependencyResolverContract))]
	public interface IMvcDependencyResolver
	{
		T Resolve<T>(System.Web.HttpContext context) where T : class;

		T[] ResolveAll<T>(System.Web.HttpContext context);

		T Resolve<T>(System.Web.HttpContextBase context) where T : class;

		T[] ResolveAll<T>(System.Web.HttpContextBase context);

	}
}
