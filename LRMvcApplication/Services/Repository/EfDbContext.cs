using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using LRMvcApplication.Domain.Univer;
using LRMvcApplication.Services.Settings;

namespace LRMvcApplication.Services.Repository
{
	//[DbConfigurationType(typeof(EfDbContextConfiguration))] 
	public class EfDbContext : DbContext
	{
		public DbSet<Cource> Cources { get; set; }
		public DbSet<Student> Students { get; set; }
		
		public EfDbContext(
			ISettingsService settingsService) :
			base(
				settingsService.ConnectionString)
		{
			Database.SetInitializer(
				new EfDbContextInitializer());
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
			base.OnModelCreating(modelBuilder);
		}

	}
}