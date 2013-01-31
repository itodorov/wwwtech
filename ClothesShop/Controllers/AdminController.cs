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
			return RedirectToAction("Categories");
		}

		public ActionResult Categories()
		{
			return View(this.GetAllCategories());
		}

		public ActionResult SubCategories()
		{
			ViewBag.Categories = this.GetAllCategories();
			return View(this.GetAllSubcategories());
		}

		public ActionResult AddCategory()
		{
			return View();
		}

		[HttpPost]
		public ActionResult AddCategory(CategoryModel categoryModel)
		{
			if (ModelState.IsValid)
			{
				using (ClothesShopEntities entity = new ClothesShopEntities())
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

			return RedirectToAction("Categories");
		}

		public ActionResult AddSubCategory()
		{
			ViewBag.Categories = this.GetAllCategories();
			return View();
		}

		[HttpPost]
		public ActionResult AddSubCategory(SubcategoryModel model)
		{
			if (ModelState.IsValid)
			{
				using (ClothesShopEntities entity = new ClothesShopEntities())
				{
					if (entity.Categories.Where(category => category.ID == model.CategoryID).Count() != 0)
					{
						if (entity.SubCategories.Where(subcategory => subcategory.SubCategoryName == model.SubcategoryName && subcategory.CategoryID == model.CategoryID).Count() == 0)
						{
							SubCategory newSubcategory = new SubCategory() { SubCategoryName = model.SubcategoryName, CategoryID = model.CategoryID };
							entity.AddToSubCategories(newSubcategory);
							entity.SaveChanges();
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

			return RedirectToAction("SubCategories");
		}

		[HttpGet]
		public ActionResult RemoveCategory(int id)
		{
			if (ModelState.IsValid)
			{
				using (ClothesShopEntities entity = new ClothesShopEntities())
				{
					Category category = entity.Categories.FirstOrDefault(c => c.ID == id);
					if (category != null)
					{
						entity.Categories.DeleteObject(category);
						entity.SaveChanges();
					}
					else
					{
						ModelState.AddModelError("", "A category with the given ID does not exist.");
					}
				}
			}

			return RedirectToAction("Categories");
		}

		[HttpGet]
		public ActionResult RemoveSubCategory(int id)
		{
			if (ModelState.IsValid)
			{
				using (ClothesShopEntities entity = new ClothesShopEntities())
				{
					SubCategory subcategory = entity.SubCategories.FirstOrDefault(c => c.ID == id);
					if (subcategory != null)
					{
						entity.SubCategories.DeleteObject(subcategory);
						entity.SaveChanges();
					}
					else
					{
						ModelState.AddModelError("", "A subcategory with the given ID does not exist.");
					}
				}
			}
			return RedirectToAction("SubCategories");
		}

		private List<CategoryModel> GetAllCategories()
		{
			using (ClothesShopEntities entity = new ClothesShopEntities())
			{
				return new List<CategoryModel>(entity.Categories.Select(category => new CategoryModel() { Name = category.CategoryName, ID = category.ID }));
			}
		}

		private List<SubcategoryModel> GetAllSubcategories()
		{
			using (ClothesShopEntities entity = new ClothesShopEntities())
			{
				return new List<SubcategoryModel>(entity.SubCategories.Select(subcategory => new SubcategoryModel() { SubcategoryName = subcategory.Category.CategoryName + " | " + subcategory.SubCategoryName, ID = subcategory.ID }));
			}
		}
	}
}
