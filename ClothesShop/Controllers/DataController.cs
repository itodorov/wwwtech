using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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


        public ActionResult Categories()
        {
            JsonResult res = Json(ClothesShop.Data.DataHelper.GetCategoriesTree());
            res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return res;
        }
    }
}
