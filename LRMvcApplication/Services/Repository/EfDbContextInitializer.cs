using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;

namespace LRMvcApplication.Services.Repository
{
	public class EfDbContextInitializer : IDatabaseInitializer<EfDbContext>
	{
		public void InitializeDatabase(EfDbContext context)
		{
			if (context.Database.Exists())
			{
				if (context.Database.CompatibleWithModel(true))
				{
					return;
				}
				else
				{
					throw new Exception(
						Resources.Repository.DatabaseExistsWithDifferentModel);
				}
			}
			else
			{
				context.Database.Create();

				var types = Assembly.GetExecutingAssembly().GetTypes()
					.Where(x => (typeof(ISlqConstraints).IsAssignableFrom(x) && x.IsClass))
					.ToList();

				foreach (var iter in types)
				{
					var constraint = (ISlqConstraints)Activator.CreateInstance(iter);
					if (constraint.Uniques != null)
					{
						if (constraint.Uniques.Count > 0)
						{
							int uniqueConstraintNumber = 1;
							foreach (var unique in constraint.Uniques)
							{
								if (unique.Count > 0)
								{
									var uniqueFields = new StringBuilder();
									foreach (var uniqueField in unique)
										uniqueFields.AppendFormat(@"{0},", uniqueField);

									uniqueFields.Length--;

									string query = String.Format(
@"ALTER TABLE {0} ADD CONSTRAINT EF_UNIQUE_CONSTRAINT_FOR_{0}_{1}_OF_{2} UNIQUE ({3})",
										constraint.TableName,
										uniqueConstraintNumber,
										constraint.Uniques.Count,
										uniqueFields.ToString());

									context.Database.ExecuteSqlCommand(
										query);
								}
								uniqueConstraintNumber++;
							}
						}
					}
					if (constraint.FullTextIndex != null)
					{
						if (constraint.FullTextIndex.Count > 0)
						{
							var fields = new StringBuilder();
							foreach (var uniqueField in constraint.FullTextIndex)
								fields.AppendFormat(@"{0},", uniqueField);

							fields.Length--;

							string query = String.Format(
@"CREATE FULLTEXT CATALOG FULLTEXT_CATALOG_FOR_{0}; 
CREATE FULLTEXT INDEX ON {0} ({1}) KEY INDEX [PK_dbo.{0}] ON FULLTEXT_CATALOG_FOR_{0};",
									constraint.TableName,
									fields.ToString());

							context.Database.ExecuteSqlCommand(
								TransactionalBehavior.DoNotEnsureTransaction,
								query);
						}
					}
				}
			}
		}
	}
}