using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClothesShop.Data;

namespace ClothesShop.Controllers
{
    public class DataController : Controller
    {
        //
        // GET: /Data/

        public ActionResult Index()
        {
            return View();
        }

		public ActionResult Products()
		{
			return JsonResultilizer(DataHelper.GetSubCategoryItems(4));
		}

		private ActionResult JsonResultilizer(object result)
		{
			JsonResult res = Json(result);
			res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
			return res;
		}
    }
}
