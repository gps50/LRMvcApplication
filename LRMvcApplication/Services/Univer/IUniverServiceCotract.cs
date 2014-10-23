using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;
using LRMvcApplication.Domain.Paging;
using LRMvcApplication.Domain.Univer;

namespace LRMvcApplication.Services.Univer
{
	[ContractClassFor(typeof(IUniverService))]
	public abstract class IUniverServiceCotract : IUniverService
	{
		#region cources

		public Cource CreateCource(
			string title,
			string description)
		{
			Contract.Requires(!string.IsNullOrEmpty(title));

			Contract.Ensures(Contract.Result<Cource>() != null);

			return default(Cource);
		}

		public Cource GetCource(
			int id)
		{
			Contract.Requires(id >= 0);

			return default(Cource);
		}

		public IList<Cource> GetCources(
			Page page)
		{
			Contract.Requires(page != null);
			Contract.Requires(page.Index >= 0);
			Contract.Requires(page.Size > 0);

			Contract.Ensures(Contract.Result<IList<Cource>>() != null);

			return default(IList<Cource>);
		}

		public IList<Cource> GetCources(
			Page page,
			string searchQuery,
			out int totalCount)
		{
			Contract.Requires(page != null);
			Contract.Requires(page.Index >= 0);
			Contract.Requires(page.Size > 0);

			Contract.Ensures(Contract.Result<IList<Cource>>() != null);

			totalCount = default(int);
			return default(IList<Cource>);
		}

		public void UpdateCource(
			Cource cource)
		{
			Contract.Requires(cource != null);
		}

		public void DeleteCource(
			Cource cource)
		{
			Contract.Requires(cource != null);
		}

		#endregion


	}
}