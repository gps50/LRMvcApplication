using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Web;
using System.Web.Mvc;

namespace LRMvcApplication.Controllers
{    
	[AllowAnonymous]
	[SessionState(System.Web.SessionState.SessionStateBehavior.Disabled)]
	public partial class ErrorController : Controller
	{
		public virtual ActionResult Index(
			bool http404 = false)
		{
			if (Request.IsAjaxRequest())
				return new HttpStatusCodeResult(500);
			else
			{
				ViewBag.Http404 = http404;
				return View();
			}
		}
	}    
}
