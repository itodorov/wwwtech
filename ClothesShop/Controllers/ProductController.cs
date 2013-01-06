using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClothesShop.Controllers
{
    public class ProductController : Controller
    {
		[HttpGet]
        public ActionResult ViewProduct(int id)
        {
            return View(new ClothesShop.Models.ProductModel(Data.DataHelper.GetProduct(id)));
        }
    }
}
