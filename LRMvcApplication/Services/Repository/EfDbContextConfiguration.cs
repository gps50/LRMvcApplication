//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Data.Entity.Infrastructure;
//using System.Data.Entity.SqlServer;
//using System.Linq;
//using System.Runtime.Remoting.Messaging;
//using System.Web;

//namespace LRMvcApplication.Services.Repository
//{
//	public class EfDbContextConfiguration : DbConfiguration
//	{
//		public EfDbContextConfiguration()
//		{
//			this.SetExecutionStrategy("System.Data.SqlClient", () => SuspendExecutionStrategy
//			  ? (IDbExecutionStrategy)new DefaultExecutionStrategy()
//			  : new SqlAzureExecutionStrategy());
//		}

//		public static bool SuspendExecutionStrategy
//		{
//			get
//			{
//				return (bool?)CallContext.LogicalGetData("SuspendExecutionStrategy") ?? false;
//			}
//			set
//			{
//				CallContext.LogicalSetData("SuspendExecutionStrategy", value);
//			}
//		}
//	}
//}