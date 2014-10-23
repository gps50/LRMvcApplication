using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace LRMvcApplication.Models
{
	public class CourceModel
	{
		public int Id { get; set; }

		public string Title { get; set; }

		public string Description { get; set; }
	}

	public class CourcesModel
	{
		public int PageNumber { get; set; }
		public int PagesCount { get; set; }
		public IList<CourceModel> Cources { get; set; }
	}

}