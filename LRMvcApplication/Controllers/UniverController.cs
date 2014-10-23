using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LRMvcApplication.Domain.Paging;
using LRMvcApplication.Filters;
using LRMvcApplication.Models;
using LRMvcApplication.Services.Settings;
using LRMvcApplication.Services.Univer;
using LRMvcApplication.Services.UniverExceptions;

namespace LRMvcApplication.Controllers
{
	[SessionState(System.Web.SessionState.SessionStateBehavior.Disabled)]
	public partial class UniverController : Controller
	{
		#region fields

		private readonly string _titleError = "Длина названия от 2 до 10 символов";
		private readonly string _descriptionError = "Длина описания не более 20 символов";

		private readonly IUniverService _univerService;
		private readonly ISettingsService _settingsService;

		#endregion


		#region ctor

		public UniverController(
			IUniverService univerService,
			ISettingsService settingsService)
		{
			this._univerService = univerService;
			this._settingsService = settingsService;
		}

		#endregion


		#region index

		[HttpGet]
		[Cache]
		public virtual ActionResult Index()
		{
			return View();
		}

		#endregion


		#region cources

		[HttpGet]
		[NoCache]
		public virtual ActionResult CourcesList(
			int? pageNumber, // int pageNumber = 1, // ?t4mvc
			int? pageSize = null,
			string search = null)
		{
			pageNumber = pageNumber ?? 1; // ?t4mvc

			var page = new Page
			{
				Index = pageNumber.Value - 1,
				Size = pageSize ?? _settingsService.PageSize
			};

			int totalCount;
			var list = _univerService.GetCources(
				page,
				search,
				out totalCount);

			var model = new CourcesModel
			{
				PageNumber = pageNumber.Value,
				PagesCount = (int)Math.Ceiling((double)totalCount / (double)page.Size),
				Cources = list.Select(x => CourceModelMap.CreateModel(x)).ToList()
			};

			return View(model);
		}

		[HttpPost]
		[NoCache]
		public virtual ActionResult CreateCource(
			CourceModel model)
		{
			try
			{
				var entity = _univerService.CreateCource(
					model.Title,
					model.Description);
				return new HttpStatusCodeResult(
					HttpStatusCode.Created);
			}
			catch (TitleException)
			{
				return new HttpStatusCodeResult(
					HttpStatusCode.BadRequest,
					_titleError);
			}
			catch (DescriptionException)
			{
				return new HttpStatusCodeResult(
					HttpStatusCode.BadRequest,
					_descriptionError);
			}
		}

		[HttpPut]
		[NoCache]
		public virtual ActionResult UpdateCource(
			CourceModel model)
		{
			try
			{
				var entity = _univerService.GetCource(model.Id);
				if (entity != null)
				{
					CourceModelMap.UpdateEntity(model, entity);
					_univerService.UpdateCource(
						entity);
					return new HttpStatusCodeResult(HttpStatusCode.OK);
				}
				else
				{
					return new HttpStatusCodeResult(HttpStatusCode.NotFound);
				}
			}
			catch (TitleException)
			{
				return new HttpStatusCodeResult(
					HttpStatusCode.BadRequest,
					_titleError);
			}
			catch (DescriptionException)
			{
				return new HttpStatusCodeResult(
					HttpStatusCode.BadRequest,
					_descriptionError);
			}
		}

		[HttpDelete]
		[NoCache]
		public virtual ActionResult DeleteCource(
			CourceModel model)
		{
			var entity = _univerService.GetCource(
				model.Id);
			if (entity != null)
			{
				_univerService.DeleteCource(entity);
				return new HttpStatusCodeResult(HttpStatusCode.OK);
			}
			else
			{
				return new HttpStatusCodeResult(HttpStatusCode.NotFound);
			}
		}

		#endregion

	}
}
