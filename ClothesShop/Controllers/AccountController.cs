using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using ClothesShop.Models;

namespace ClothesShop.Controllers
{
    public class AccountController : Controller
    {

        //
        // GET: /Account/LogOn

		public ActionResult LogOn(string returnUrl)
        {
			return View(new LogOnModel() { ReturnUrl = returnUrl });
        }

        //
        // POST: /Account/LogOn

        [HttpPost]
        public ActionResult LogOn(LogOnModel model)
        {
			string returnUrl = model.ReturnUrl;
            if (ModelState.IsValid)
            {
				using (ClothesShopEntities entities = new ClothesShopEntities())
				{
					ClothesShop.User user = entities.Users.Where(x => x.Username == model.UserName && x.Password == model.Password).FirstOrDefault();
					if (user != null)
					{
						Session["Username"] = model.UserName;
						Session["IsAuthenticated"] = true;
						Session["IsAdmin"] = user.IsAdmin;
						Session["Basket"] = new BasketModel();

						if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
							&& !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
						{
							return Redirect(returnUrl);
						}
						else
						{
							return RedirectToAction("Index", "Home");
						}
					}
					else
					{
						ModelState.AddModelError("", "The user name or password provided is incorrect.");
					}
				}
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            Session.RemoveAll();
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/Register

        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
				using (ClothesShopEntities entities = new ClothesShopEntities())
				{
					if (entities.Users.Where(x => x.Username == model.UserName).Count() > 0)
					{
						ModelState.AddModelError("", "Username already exists");
						return View(model);
					}

					ClothesShop.User user = new ClothesShop.User();
					user.Username = model.UserName;
					user.Password = model.Password;
					user.IsAdmin = false;
					entities.Users.AddObject(user);
					entities.SaveChanges();

					Session["Username"] = user.Username;
					Session["IsAuthenticated"] = true;
					return RedirectToAction("Index", "Home");
				}
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

    }
}
