using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClothesShop.Data;

namespace ClothesShop.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {  
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

		public ActionResult Lang(string id, string returnUrl)
		{
			if (id == "BG")
			{
				Session["Language"] = Language.BG;
			}
			else if (id == "EN")
			{
				Session["Language"] = Language.EN;
			}

			return new RedirectResult(returnUrl);
		}
    }
}
