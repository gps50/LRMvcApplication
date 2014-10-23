using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;

namespace LRMvcApplication.DependencyResolver
{
	[ContractClassFor(typeof(IMvcDependencyResolver))]
	public abstract class IMvcDependencyResolverContract : IMvcDependencyResolver
	{
		public T Resolve<T>(System.Web.HttpContext context) where T : class
		{
			Contract.Requires(context != null);
			Contract.Ensures(Contract.Result<T>() != null);

			return default(T);
		}

		public T[] ResolveAll<T>(System.Web.HttpContext context)
		{
			Contract.Requires(context != null);
			Contract.Ensures(Contract.Result<T[]>() != null);

			return default(T[]);
		}

		public T Resolve<T>(System.Web.HttpContextBase context) where T : class
		{
			Contract.Requires(context != null);
			Contract.Ensures(Contract.Result<T>() != null);

			return default(T);
		}

		public T[] ResolveAll<T>(System.Web.HttpContextBase context)
		{
			Contract.Requires(context != null);
			Contract.Ensures(Contract.Result<T[]>() != null);

			return default(T[]);
		}
	}
}