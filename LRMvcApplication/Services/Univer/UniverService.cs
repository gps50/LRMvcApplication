using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;
using LRMvcApplication.Domain.Paging;
using LRMvcApplication.Domain.Univer;
using LRMvcApplication.Libraries.LambdaExpressions;
using LRMvcApplication.Services.Repository;
using LRMvcApplication.Services.UniverExceptions;

namespace LRMvcApplication.Services.Univer
{
	public class UniverService : IUniverService
	{
		#region fields

		private readonly EfDbContext _dbContext;

		#endregion


		#region ctor

		public UniverService(
			EfDbContext dbContext)
		{
			Contract.Requires(dbContext != null);

			this._dbContext = dbContext;
		}

		#endregion


		#region IUniverService


		#region cources

		public Cource CreateCource(
			string title,
			string description)
		{
			var cource = new Cource
			{
				Title = title,
				Description = description
			};

			if (_dbContext.Entry(cource).Property(x => x.Title).GetValidationErrors().Count() > 0)
			{
				throw new TitleException();
			}

			if (_dbContext.Entry(cource).Property(x => x.Description).GetValidationErrors().Count() > 0)
			{
				throw new DescriptionException();
			}

			_dbContext.Cources.Add(
				cource);

			_dbContext.SaveChanges();

			return cource;
		}

		public Cource GetCource(
			int id)
		{
			var result = _dbContext.Cources
				.Where(x => (x.Id == id))
				.FirstOrDefault();

			return result;
		}

		public IList<Cource> GetCources(
			Page page,
			string searchQuery,
			out int totalCount)
		{
			int skip = page.Index * page.Size;

			if (string.IsNullOrEmpty(searchQuery))
			{
				IList<Cource> list;
				using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.Serializable))
				{
					totalCount = _dbContext.Cources.Count();
					list = _dbContext.Cources
						.OrderBy(x => x.Id)
						.Skip(skip)
						.Take(page.Size)
						.ToList();
				}
				return list;
			}
			else
			{
				IList<Cource> list;
				Func<SqlParameter> search = () => new SqlParameter("@search", SqlDbType.VarChar)
				{
					SqlValue = string.Format(
						@"""{0}*""",
						searchQuery)
				};
				using (var transaction = _dbContext.Database.BeginTransaction(IsolationLevel.Serializable))
				{
					var query = string.Format(
@"SELECT COUNT(*) FROM {0}
WHERE (
CONTAINS({2}, {1}) OR 
CONTAINS({3}, {1}))",
						(new CourceMap()).TableName,
						search().ParameterName,
						LambdaExpressionsHelper.GetPropertyName<Cource, string>(x => x.Title),
						LambdaExpressionsHelper.GetPropertyName<Cource, string>(x => x.Description)
					);
					totalCount = _dbContext.Database.SqlQuery<int>(
						 query,
						 search()
					 ).FirstOrDefault();

					query = string.Format(
@"SELECT TOP ({0}) * FROM
(
SELECT *, ROW_NUMBER() OVER (ORDER BY {1} ASC) AS RowNumber
FROM {2} AS Extent
WHERE (
CONTAINS({4}, {3}) OR 
CONTAINS({5}, {3}))
) 
AS Extent
WHERE RowNumber > {6}
ORDER BY {1} ASC",
						page.Size,
						LambdaExpressionsHelper.GetPropertyName<Cource, int>(x => x.Id),
						(new CourceMap()).TableName,
						search().ParameterName,
						LambdaExpressionsHelper.GetPropertyName<Cource, string>(x => x.Title),
						LambdaExpressionsHelper.GetPropertyName<Cource, string>(x => x.Description),
						skip
					);

					list = _dbContext.Cources.SqlQuery(
						query,
						search()
					).ToList();
				}
				return list;
			}
		}

		public void UpdateCource(
			Cource cource)
		{
			if (_dbContext.Entry(cource).Property(x => x.Title).GetValidationErrors().Count() > 0)
			{
				throw new TitleException();
			}

			if (_dbContext.Entry(cource).Property(x => x.Description).GetValidationErrors().Count() > 0)
			{
				throw new DescriptionException();
			}

			_dbContext.SaveChanges();
		}

		public void DeleteCource(
			Cource cource)
		{
			_dbContext.Cources.Remove(cource);
			_dbContext.SaveChanges();
		}

		#endregion


		#endregion


	}
}