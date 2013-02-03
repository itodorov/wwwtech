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

		protected override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			if (!this.IsAuthenticated())
			{
				filterContext.Result = new RedirectResult("/Account/Logon?returnUrl=" + Request.Url.LocalPath, false);
			}

			base.OnActionExecuting(filterContext);
		}

        public ActionResult Index()
        { 
            if ((Session["Basket"] is BasketModel))
            {
                return View((BasketModel)Session["Basket"]);    
            }

            return View();
        }

		[HttpGet]
		public ActionResult AddToCart(int id)
		{
			if (!(Session["Basket"] is BasketModel))
			{
				Session["Basket"] = new BasketModel();
			}

			ProductModel product = new ProductModel(DataHelper.GetProduct(id));
			((BasketModel)Session["Basket"]).Add(new BasketItem(product, 1, 0));

			return RedirectToAction("Index");
		}

		[HttpGet]
		public ActionResult RemoveFromCart(int id, int quantity, int size)
		{
			if (!(Session["Basket"] is BasketModel))
			{
				Session["Basket"] = new BasketModel();
			}

			ProductModel product = new ProductModel(DataHelper.GetProduct(id));
			BasketModel model = ((BasketModel)Session["Basket"]);
			BasketItem item = model.Where(x => x.Product.ID == id && x.Quantity == quantity && x.Size == size).FirstOrDefault();
			if (item != null)
			{
				model.Remove(item);
			}
			
			return RedirectToAction("Index");
		}



        private bool IsAuthenticated()
        {
            return Object.Equals(Session["IsAuthenticated"], true);
        }
    }
}
