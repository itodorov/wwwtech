using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClothesShop.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult ViewProduct(int productId)
        {
            return View(new ClothesShop.Models.ProductModel(Data.DataHelper.GetProduct(productId)));
        }
    }
}
