using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClothesShop.Models;
using System.IO;
using ClothesShop.Data;

namespace ClothesShop.Controllers
{
	public class AdminController : Controller
	{
		protected override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			if (!this.IsAuthenticated())
			{
				filterContext.Result = new RedirectResult("/Account/Logon?returnUrl=" + Request.Url.LocalPath, false);
				return;
			}
			else if (!this.IsAdmin())
			{
				filterContext.Result = new HttpStatusCodeResult(403, "Forbidden");
				filterContext.HttpContext.Response.Write("Forbidden");
			}

			base.OnActionExecuting(filterContext);
		}

		#region Categories

		[HttpGet]
		public ActionResult Index()
		{
			return RedirectToAction("Categories");
		}

		[HttpGet]
		public ActionResult Categories()
		{
			return View(DataHelper.GetAllCategories());
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

		#endregion

		#region SubCategories

		public ActionResult SubCategories()
		{
			ViewBag.Categories = DataHelper.GetAllCategories();
			return View(DataHelper.GetAllSubcategories());
		}

		public ActionResult AddSubCategory()
		{
			ViewBag.Categories = DataHelper.GetAllCategories();
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

		#endregion

		#region Products

		[HttpGet]
		public ActionResult Products(int? id, int? subid)
		{
			ViewBag.Categories = DataHelper.GetAllCategories();
			ViewBag.SubCategories = DataHelper.GetAllSubcategories();
			return View(DataHelper.GetAllProducts(id, subid));
		}

		public ActionResult AddProduct()
		{
			ViewBag.SubCategories = DataHelper.GetAllSubcategories();
			return View();
		}

		public ActionResult EditProduct(int id)
		{
			ViewBag.SubCategories = DataHelper.GetAllSubcategories();
			using (ClothesShopEntities entity = new ClothesShopEntities())
			{
				return View(new ProductModel(entity.Products.Where(x => x.ID == id).FirstOrDefault()));
			}
		}

		[HttpPost]
		public ActionResult EditProduct(ProductModel product)
		{
			ViewBag.SubCategories = DataHelper.GetAllSubcategories();

			using (ClothesShopEntities entity = new ClothesShopEntities())
			{
				Product prod = entity.Products.Where(x => x.ID == product.ID).FirstOrDefault();

				prod.SubCategoryID = product.SubCategoryID;
				prod.No = product.Number;
				prod.Name = product.Name;
				prod.NameEN = product.NameEN;
				prod.Description = product.Description;
				prod.DescriptionEN = product.DescriptionEN;
				prod.Price = product.Price;
				prod.Weight = product.Weight;
				prod.Special = product.Special;
				prod.QuantityType = (byte)product.QuantityType;
				prod.Quantity = product.Quantity;

				entity.SaveChanges();
			}

			return RedirectToAction("Products");
		}

		[HttpPost]
		public ActionResult AddProduct(ProductModel product)
		{
			using (ClothesShopEntities entity = new ClothesShopEntities())
			{
				Product prod = new Product()
				{
					SubCategoryID = product.SubCategoryID,
					No = product.Number,
					Name = product.Name,
					NameEN = product.NameEN,
					Description = product.Description,
					DescriptionEN = product.DescriptionEN,
					Price = product.Price,
					Weight = product.Weight,
					Special = product.Special,
					QuantityType = (byte)product.QuantityType,
					Quantity = product.Quantity,
				};
				entity.Products.AddObject(prod);
				entity.SaveChanges();
			}

			return RedirectToAction("Products");
		}

		public ActionResult RemoveProduct(int id)
		{
			using (ClothesShopEntities entity = new ClothesShopEntities())
			{
				entity.Products.DeleteObject(entity.Products.Where(x => x.ID == id).FirstOrDefault());
				entity.SaveChanges();
			}

			return RedirectToAction("Products");
		}

		#endregion

		#region Images

		[HttpGet]
		public ActionResult ProductImages(int id)
		{
			ViewBag.ProductId = id;
			return View(DataHelper.GetProductImages(id));
		}

		[HttpPost]
		public ActionResult AddImage(int id)
		{
			HttpPostedFileBase file = Request.Files[0];
			//TODO: check file extensions

			string fileName = Guid.NewGuid().ToString() + ".png";
			string dir = Server.MapPath("/ProductPictures");
			if (!Directory.Exists(dir))
			{
				Directory.CreateDirectory(dir);
			}

			file.SaveAs(Server.MapPath("/ProductPictures/" + fileName));

			using (ClothesShopEntities entity = new ClothesShopEntities())
			{
				Image image = new Image()
				{
					ProductID = id,
					FileName = fileName
				};
				entity.Images.AddObject(image);
				entity.SaveChanges();
			}

			return RedirectToAction("ProductImages", new { id = id });
		}

		[HttpGet]
		public ActionResult RemoveImage(int id)
		{
			using (ClothesShopEntities entity = new ClothesShopEntities())
			{
				Image image = entity.Images.Where(x => x.ID == id).FirstOrDefault();
				string fileName = Server.MapPath("/ProductPictures/" + image.FileName);

				if (System.IO.File.Exists(fileName))
				{
					System.IO.File.Delete(fileName);
				}

				entity.Images.DeleteObject(image);
				entity.SaveChanges();
			}

			return RedirectToAction("ProductImages", new { id = id });
		}

		#endregion

		#region Sizes

		[HttpGet]
		public ActionResult ProductQuantity(int id)
		{
			ViewBag.ProductId = id;
			return View(DataHelper.GetProductQuantities(id));
		}

		[HttpPost]
		public ActionResult AddQuantity(int id, int size, int quantity)
		{
			using (ClothesShopEntities entity = new ClothesShopEntities())
			{
				Product product = entity.Products.Where(x => x.ID == id).FirstOrDefault();
				Quantity q = new Quantity();
				q.Size = size;
				q.Quantity1 = quantity;
				product.Quantities.Add(q);
				entity.SaveChanges();
			}

			return RedirectToAction("ProductQuantity", new { id = id });
		}

		[HttpGet]
		public ActionResult DeleteQuantity(int id)
		{
			using (ClothesShopEntities entity = new ClothesShopEntities())
			{
				Quantity q = entity.Quantities.Where(x => x.ID == id).FirstOrDefault();
				entity.DeleteObject(q);
				entity.SaveChanges();
			}

			return RedirectToAction("ProductQuantity", new { id = id });
		}

		#endregion

		private bool IsAdmin()
		{
			return Object.Equals(Session["IsAdmin"], true);
		}

		private bool IsAuthenticated()
		{
			return Object.Equals(Session["IsAuthenticated"], true);
		}
	}
}