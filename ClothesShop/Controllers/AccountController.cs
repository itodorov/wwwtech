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

        public ActionResult LogOn()
        {
            return View();
        }

        //
        // POST: /Account/LogOn

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                ClothesShopEntities entities = new ClothesShopEntities();
                if (entities.Users.Where(x => x.Username == model.UserName && x.Password == model.Password).Count() == 1)
                {
                    Session["Username"] = model.UserName;
                    Session["IsAuthenticated"] = true;

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

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            Session.Remove("Username");
            Session.Remove("IsAuthenticated");

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
                ClothesShopEntities entities = new ClothesShopEntities();
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
                entities.Dispose();
                Session["Username"] = user.Username;
                Session["IsAuthenticated"] = true;
                return RedirectToAction("Index", "Home");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

    }
}
