using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClothesShop.Data;
using ClothesShop.Models;

namespace ClothesShop.Controllers
{
    public class BasketController : Controller
    {
        //
        // GET: /Basket/

        public ActionResult Index()
        {
            if (!IsAuthenticated())
            {
                return RedirectToAction("LogOn", "Account");
            }

            if ((Session["Basket"] is BasketModel))
            {
                return View((BasketModel)Session["Basket"]);    
            }

            return View();
        }

        [HttpGet]
        public ActionResult AddToCart(int id)
        {
            if (!IsAuthenticated())
            {
                return RedirectToAction("LogOn", "Account");
            }

            if(!(Session["Basket"] is BasketModel))
            {
                Session["Basket"] = new BasketModel();
            }

            ProductModel product = new ProductModel(DataHelper.GetProduct(id));
            ((BasketModel)Session["Basket"]).Add(new BasketItem(product, 1, 0));

            return RedirectToAction("Index");
        }

        private bool IsAuthenticated()
        {
            return Object.Equals(Session["IsAuthenticated"], true);
        }
    }
}
