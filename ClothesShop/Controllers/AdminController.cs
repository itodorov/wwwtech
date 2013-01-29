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
		public ActionResult AddCategory(AddCategoryModel categoryModel)
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
						return RedirectToAction("AddCategory", "Admin");
					}
					else
					{
						ModelState.AddModelError("", "A category with the same name already exists.");
					}
				}
			}
			return View(categoryModel);
		}

		public ActionResult AddSubcategory()
		{
			return View(new AddSubcategoryModel() { Categories = GetAllCategories() });
		}

		[HttpPost]
		public ActionResult AddSubcategory(AddSubcategoryModel model)
		{
			if (ModelState.IsValid)
			{
				using (ClothesShopEntities entity = new ClothesShopEntities())
				{
					if (entity.Categories.Where(category => category.ID == model.ParentCategoryID).Count() != 0)
					{
						if (entity.SubCategories.Where(subcategory => subcategory.SubCategoryName == model.SubcategoryName && subcategory.CategoryID == model.ParentCategoryID).Count() == 0)
						{
							SubCategory newSubcategory = new SubCategory() { SubCategoryName = model.SubcategoryName, CategoryID = model.ParentCategoryID };
							entity.AddToSubCategories(newSubcategory);
							entity.SaveChanges();
							return RedirectToAction("AddSubcategory", "Admin");
						}
						else
						{
							ModelState.AddModelError("", "A subcategory with the same name already exists.");
						}
					}
					else
					{
						ModelState.AddModelError("", "A category with the given ID does not exist.");
					}
				}
			}
			return View(model);
		}

		public ActionResult RemoveCategory()
		{
			return View(new RemoveCategoryModel() { Categories = GetAllCategories() });
		}

		[HttpPost]
		public ActionResult RemoveCategory(RemoveCategoryModel model)
		{
			if (ModelState.IsValid)
			{
				using (ClothesShopEntities entity = new ClothesShopEntities())
				{
					Category category = entity.Categories.FirstOrDefault(c => c.ID == model.ID);
					if (category != null)
					{
						entity.Categories.DeleteObject(category);
						entity.SaveChanges();
						return RedirectToAction("RemoveCategory", "Admin");
					}
					else
					{
						ModelState.AddModelError("", "A category with the given ID does not exist.");
					}
				}
			}
			return View(model);
		}

		public ActionResult RemoveSubcategory()
		{
			return View(new RemoveSubcategoryModel() { Subcategories = GetAllSubcategories() });
		}

		[HttpPost]
		public ActionResult RemoveSubcategory(RemoveSubcategoryModel model)
		{
			if (ModelState.IsValid)
			{
				using (ClothesShopEntities entity = new ClothesShopEntities())
				{
					SubCategory subcategory = entity.SubCategories.FirstOrDefault(c => c.ID == model.ID);
					if (subcategory != null)
					{
						entity.SubCategories.DeleteObject(subcategory);
						entity.SaveChanges();
						return RedirectToAction("RemoveSubcategory", "Admin");
					}
					else
					{
						ModelState.AddModelError("", "A subcategory with the given ID does not exist.");
					}
				}
			}
			return View(model);
		}

		private List<AddCategoryModel> GetAllCategories()
		{
			using (ClothesShopEntities entity = new ClothesShopEntities())
			{
				return new List<AddCategoryModel>(entity.Categories.Select(category => new AddCategoryModel() { Name = category.CategoryName, ID = category.ID }));
			}
		}

		private List<AddSubcategoryModel> GetAllSubcategories()
		{
			using (ClothesShopEntities entity = new ClothesShopEntities())
			{
				return new List<AddSubcategoryModel>(entity.SubCategories.Select(subcategory => new AddSubcategoryModel()
					{ SubcategoryName = subcategory.Category.CategoryName + " | " + subcategory.SubCategoryName, ID = subcategory.ID }));
			}
		}
    }
}
