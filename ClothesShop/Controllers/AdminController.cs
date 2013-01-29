using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClothesShop.Models;

namespace ClothesShop.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View();
        }

		public ActionResult AddCategory()
		{
			return View();
		}

		[HttpPost]
		public ActionResult AddCategory(CategoryModel categoryModel)
		{
			if(ModelState.IsValid)
			{
				using(ClothesShopEntities entity = new ClothesShopEntities())
				{
					if (entity.Categories.Where(category => category.CategoryName == categoryModel.Name).Count() == 0)
					{
						Category newCategory = new Category() { CategoryName = categoryModel.Name };
						entity.AddToCategories(newCategory);
						entity.SaveChanges();
					}
					else
					{
						ModelState.AddModelError("", "A category with the same name already exists.");
					}
				}
			}
			return View(categoryModel);
		}
    }
}
