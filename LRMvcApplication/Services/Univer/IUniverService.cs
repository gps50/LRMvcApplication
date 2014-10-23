using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;
using LRMvcApplication.Domain.Paging;
using LRMvcApplication.Domain.Univer;

namespace LRMvcApplication.Services.Univer
{
	[ContractClass(typeof(IUniverServiceCotract))]
	public interface IUniverService
	{
		#region cources

		/// <exception cref="LRMvcApplication.Services.UniverExceptions.TitleException">Incorrect title</exception>
		/// <exception cref="LRMvcApplication.Services.UniverExceptions.DescriptionException">Incorrect description</exception>
		Cource CreateCource(
			string title,
			string description);

		Cource GetCource(
			int id);

		IList<Cource> GetCources(
			Page page,
			string searchQuery,
			out int totalCount);

		/// <exception cref="LRMvcApplication.Services.UniverExceptions.TitleException">Incorrect title</exception>
		/// <exception cref="LRMvcApplication.Services.UniverExceptions.DescriptionException">Incorrect description</exception>
		void UpdateCource(
			Cource cource);

		void DeleteCource(
			Cource cource);

		#endregion


	}
}