using System;
using System.Collections.Generic;
using System.Web;
using Autofac;
using LRMvcApplication.DependencyResolver;
using LRMvcApplication.DependencyResolver.AutofacInfo;
using LRMvcApplication.Services.Repository;
using LRMvcApplication.Services.Settings;
using LRMvcApplication.Services.Univer;
using LRUnitTests.Services.Settings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LRUnitTests.DependencyResolver.Autofac
{
	[TestClass]
	public class UnitTest
	{
		HttpContextBase _httpContext;
		HttpContextBase _httpContext2;
		IMvcDependencyResolver _di;

		[TestInitialize]
		public void Initialize()
		{
			var builder = new ContainerBuilder();

			builder.RegisterInstance(
				SettingsHepler.Create()).As<ISettingsService>().SingleInstance();

			builder.RegisterType<EfDbContext>().InstancePerRequest();

			builder.RegisterType<UniverService>().As<IUniverService>().InstancePerRequest();

			var httpContext = new Mock<HttpContextBase>();
			var items = new Dictionary<object, object>();
			httpContext.Setup(x => x.Items).Returns(items);
			_httpContext = httpContext.Object;

			var httpContext2 = new Mock<HttpContextBase>();
			var items2 = new Dictionary<object, object>();
			httpContext2.Setup(x => x.Items).Returns(items2);
			_httpContext2 = httpContext2.Object;

			var container = builder.Build();
			_di = new MvcDependencyResolver(container);
			
		}


		[TestMethod]
		public void AutofacISettingsServiceTest()
		{
			var settingsService = _di.Resolve<ISettingsService>(_httpContext);
			var settingsService2 = _di.Resolve<ISettingsService>(_httpContext);
			Assert.IsNotNull(settingsService);
			Assert.IsNotNull(settingsService2);
			Assert.AreEqual(settingsService, settingsService2);
			var settingsService3 = _di.Resolve<ISettingsService>(_httpContext2);
			Assert.IsNotNull(settingsService3);
			Assert.AreEqual(settingsService, settingsService3); // single instance
			
		}

		[TestMethod]
		public void AutofacEfDbContextTest()
		{
			var dbContext = _di.Resolve<EfDbContext>(_httpContext);
			var dbContext2 = _di.Resolve<EfDbContext>(_httpContext);
			Assert.IsNotNull(dbContext);
			Assert.IsNotNull(dbContext2);
			Assert.AreEqual(dbContext, dbContext2);
			var dbContext3 = _di.Resolve<EfDbContext>(_httpContext2);
			Assert.AreNotEqual(dbContext, dbContext3); // instance per context
			
		}

		[TestMethod]
		public void AutofacIUniverServiceTest()
		{			
			var univerService = _di.Resolve<IUniverService>(_httpContext);
			var univerService2 = _di.Resolve<IUniverService>(_httpContext);
			Assert.IsNotNull(univerService);
			Assert.IsNotNull(univerService2);
			Assert.AreEqual(univerService, univerService2);
			var univerService3 = _di.Resolve<IUniverService>(_httpContext2);
			Assert.AreNotEqual(univerService, univerService3); // instance per context

		}
	}
}
