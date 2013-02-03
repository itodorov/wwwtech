using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClothesShop.Models;
using System.IO;

namespace ClothesShop.Controllers
{
	public class AdminController : Controller
	{
		//
		// GET: /Admin/
		[HttpGet]
		public ActionResult Index()
		{
			return RedirectToAction("Categories");
		}

		[HttpGet]
		public ActionResult Categories()
		{
			return View(this.GetAllCategories());
		}

		[HttpGet]
		public ActionResult Products(int? id, int? subid)
		{
			ViewBag.Categories = this.GetAllCategories();
			ViewBag.SubCategories = this.GetAllSubcategories();
			return View(this.GetAllProducts(id, subid));
		}

		[HttpGet]
		public ActionResult ProductImages(int id)
		{
			ViewBag.ProductId = id;
			return View(this.GetProductImages(id));
		}

		[HttpGet]
		public ActionResult ProductQuantity(int id)
		{
			ViewBag.ProductId = id;
			return View(this.GetProductQuantities(id));
		}

		public ActionResult SubCategories()
		{
			ViewBag.Categories = this.GetAllCategories();
			return View(this.GetAllSubcategories());
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

		public ActionResult AddProduct()
		{
			ViewBag.SubCategories = this.GetAllSubcategories();
			return View();
		}

		public ActionResult EditProduct(int id)
		{
			ViewBag.SubCategories = this.GetAllSubcategories();
			using (ClothesShopEntities entity = new ClothesShopEntities())
			{
				return View(new ProductModel(entity.Products.Where(x => x.ID == id).FirstOrDefault()));
			}
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

		[HttpPost]
		public ActionResult EditProduct(ProductModel product)
		{
			ViewBag.SubCategories = this.GetAllSubcategories();

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

		private List<ProductModel> GetAllProducts(int? categoryId = null, int? subCategoryId = null)
		{
			using (ClothesShopEntities entity = new ClothesShopEntities())
			{
				if (subCategoryId != null && subCategoryId.HasValue)
				{
					return new List<ProductModel>(entity.Products.Where(x => x.SubCategoryID == subCategoryId.Value).Select(x => new ProductModel() { Entity = x }));
				}
				else if (categoryId != null && categoryId.HasValue)
				{
					return new List<ProductModel>(entity.Products.Where(x => x.SubCategory.CategoryID == categoryId.Value).Select(x => new ProductModel() { Entity = x }));
				}
				else
				{
					return new List<ProductModel>(entity.Products.Select(x => new ProductModel() { Entity = x }));
				}
			}
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
				return new List<SubcategoryModel>(entity.SubCategories.Select(subcategory => new SubcategoryModel() { SubcategoryName = subcategory.SubCategoryName, ID = subcategory.ID }));
			}
		}

		private List<PictureModel> GetProductImages(int id)
		{
			using (ClothesShopEntities entity = new ClothesShopEntities())
			{
				return new List<PictureModel>(entity.Products.Where(x => x.ID == id).First().Images.Select(x => new PictureModel() { ProductId = x.ProductID, ID = x.ID, FileName = x.FileName }));
			}
		}

		private List<QuantityModel> GetProductQuantities(int id)
		{
			using (ClothesShopEntities entity = new ClothesShopEntities())
			{
				return new List<QuantityModel>(entity.Products.Where(x => x.ID == id).First().Quantities.Select(x => new QuantityModel() { ProductId = x.ProductID, ID = x.ID, Quantity = x.Quantity1, Size = (int)x.Size }));
			}
		}

	}
}