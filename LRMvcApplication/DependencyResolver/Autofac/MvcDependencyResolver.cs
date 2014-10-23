using Autofac;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LRMvcApplication.DependencyResolver.AutofacInfo
{
	public class MvcDependencyResolver : IDependencyResolver, IMvcDependencyResolver
	{
		#region fileds

		private readonly ILifetimeScope _container;

		#endregion


		#region ctor

		public MvcDependencyResolver(ILifetimeScope container)
		{
			Contract.Requires(container != null);

			this._container = container;
		}

		#endregion


		#region IDependencyResolver

		public object GetService(Type serviceType)
		{
			Contract.Assert(serviceType != null);
			return Scope(HttpContext.Current).ResolveOptional(serviceType);
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			Contract.Ensures(Contract.Result<IEnumerable<object>>() != null);
			Contract.Assert(serviceType != null);

			var type = typeof(IEnumerable<>).MakeGenericType(serviceType);
			return (IEnumerable<object>)Scope(HttpContext.Current).Resolve(type);
		}

		#endregion


		#region dependency resolver

		public T Resolve<T>(HttpContext context) where T : class
		{
			return Scope(context).Resolve<T>();
		}

		public T[] ResolveAll<T>(HttpContext context)
		{
			return Scope(context).Resolve<IEnumerable<T>>().ToArray();
		}

		public T Resolve<T>(HttpContextBase context) where T : class
		{
			return Scope(context).Resolve<T>();
		}

		public T[] ResolveAll<T>(HttpContextBase context)
		{
			return Scope(context).Resolve<IEnumerable<T>>().ToArray();
		}

		#endregion


		#region private

		private ILifetimeScope Scope(HttpContext context)
		{
			return Scope(context.Request.RequestContext.HttpContext);
		}

		private ILifetimeScope Scope(HttpContextBase context)
		{
			if (context.Items[typeof(ILifetimeScope)] == null)
			{
				context.Items[typeof(ILifetimeScope)] = new MvcDependencyResolver(
					_container.BeginLifetimeScope(
						AutofacInfo.InstancePerRequestTagName));
			}
			var scope = context.Items[typeof(ILifetimeScope)] as MvcDependencyResolver;

			return scope._container;
		}

		#endregion


	}
}
