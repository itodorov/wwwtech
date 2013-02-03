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
            ProductModel product = new ProductModel(DataHelper.GetProduct(id));
            ((BasketModel)Session["Basket"]).Add(new BasketItem(product, 1, 0));

            return RedirectToAction("Index");
        }

		public ActionResult RemoveItem(int index)
		{
			BasketModel basket = Session["Basket"] as BasketModel;
			if (basket != null && index >= 0 && index < basket.Count)
			{
				basket.RemoveAt(index);
			}

			return RedirectToAction("Index");
		}

		public ActionResult CheckOut()
		{
			bool success = false;
			BasketModel basket = Session["Basket"] as BasketModel;
			if (basket != null)
			{
				using (ClothesShopEntities entity = new ClothesShopEntities())
				{
					Order order = new Order() { OrderDate = DateTime.Now, UserID = (int)Session["UserID"] };
					foreach (BasketItem item in basket)
					{
						OrderedProduct orderedProduct = new OrderedProduct() { ProductID = item.Product.ID, Quantity = item.Quantity };
						order.OrderedProducts.Add(orderedProduct);
					}

					entity.Orders.AddObject(order);
					try
					{
						entity.SaveChanges();
						success = true;
					}
					catch
					{
						success = false;
					}
					basket.Clear();
				}
			}

			return View(success);
		}

        private bool IsAuthenticated()
        {
            return Object.Equals(Session["IsAuthenticated"], true);
        }
    }
}
