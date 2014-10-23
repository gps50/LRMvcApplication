using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using Autofac;
using Autofac.Integration.Mvc;
using LRMvcApplication.DependencyResolver;
using LRMvcApplication.DependencyResolver.AutofacInfo;
using LRMvcApplication.Services.Repository;
using LRMvcApplication.Services.Settings;
using LRMvcApplication.Services.Univer;

namespace LRMvcApplication
{
	public static class DependencyResolverConfig
	{
		public static IMvcDependencyResolver DI
		{
			get;
			private set;
		}

		public static void RegisterDependencyResolver()
		{
			var builder = new ContainerBuilder();

			#region ioc registration

			// mvc controllers
			builder.RegisterControllers(
				Assembly.GetExecutingAssembly());

			// ISettings
			var settings = new SettingsService(
				connectionString : ConfigurationManager.ConnectionStrings["UniverConnection"].ConnectionString,
				pageSize: Convert.ToInt32(ConfigurationManager.AppSettings["UIPageSize"]),
				cacheDurationS: Convert.ToInt32(ConfigurationManager.AppSettings["CacheDurationS"])
			);
			builder.RegisterInstance(
				settings).As<ISettingsService>().SingleInstance();

			// Ef
			builder.RegisterType<EfDbContext>().InstancePerRequest();
			
			// Univer
			builder.RegisterType<UniverService>().As<IUniverService>().InstancePerRequest();

			#endregion

			var container = builder.Build();
			var di = new MvcDependencyResolver(container);
			System.Web.Mvc.DependencyResolver.SetResolver(di);
			DI = di;


		}
	}
}