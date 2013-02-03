using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Script.Serialization;
using ClothesShop.Models;

namespace ClothesShop.Data
{
    public class DataHelper
    {
        public static string GetCategoriesTree()
        {
			List<CategoryNode> result;
			using (ClothesShopEntities entities = new ClothesShopEntities())
			{
				result = new List<CategoryNode>();
				foreach (ClothesShop.Category cat in entities.Categories)
				{
					CategoryNode node = new CategoryNode();
					node.CategoryName = cat.CategoryName;
					node.Level = 0;
					node.HasChildren = true;

					foreach (ClothesShop.SubCategory subCat in cat.SubCategories)
					{
						CategoryNode subNode = new CategoryNode();

						subNode.CategoryName = subCat.SubCategoryName + "(" + subCat.Products.Count + ")";
						subNode.CategoryId = subCat.ID;
						subNode.Level = 1;
						subNode.HasChildren = false;

						node.ChildNodes.Add(subNode);
					}

					result.Add(node);
				}
			}

			JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            string json = jsonSerializer.Serialize(result);
            return json;
        }

		public static object GetSubCategoryItems(int subCategoryID)
		{
			using (ClothesShopEntities entities = new ClothesShopEntities())
			{
				var result = from product in entities.Products
							 where product.SubCategoryID == subCategoryID
							 select new { ProductName = product.Name, ProductID = product.ID, UnitPrice = product.Price, ImageFileName = product.Images.FirstOrDefault() != null ? product.Images.FirstOrDefault().FileName : "noimage.png" };

				return result.ToList();
			}
		}

        public static Product GetProduct(int productId)
        {
            ClothesShopEntities entities = new ClothesShopEntities();
            return entities.Products.Where(x => x.ID == productId).FirstOrDefault();
        }

		public static List<ProductModel> GetAllProducts(int? categoryId = null, int? subCategoryId = null)
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

		public static List<CategoryModel> GetAllCategories()
		{
			using (ClothesShopEntities entity = new ClothesShopEntities())
			{
				return new List<CategoryModel>(entity.Categories.Select(category => new CategoryModel() { Name = category.CategoryName, ID = category.ID }));
			}
		}

		public static List<SubcategoryModel> GetAllSubcategories()
		{
			using (ClothesShopEntities entity = new ClothesShopEntities())
			{
				return new List<SubcategoryModel>(entity.SubCategories.Select(subcategory => new SubcategoryModel() { SubcategoryName = subcategory.SubCategoryName, ID = subcategory.ID , CategoryName = subcategory.Category.CategoryName, CategoryID = subcategory.CategoryID}));
			}
		}

		public static List<PictureModel> GetProductImages(int id)
		{
			using (ClothesShopEntities entity = new ClothesShopEntities())
			{
				return new List<PictureModel>(entity.Products.Where(x => x.ID == id).First().Images.Select(x => new PictureModel() { ProductId = x.ProductID, ID = x.ID, FileName = x.FileName }));
			}
		}

		public static List<QuantityModel> GetProductQuantities(int id)
		{
			using (ClothesShopEntities entity = new ClothesShopEntities())
			{
				return new List<QuantityModel>(entity.Products.Where(x => x.ID == id).First().Quantities.Select(x => new QuantityModel() { ProductId = x.ProductID, ID = x.ID, Quantity = x.Quantity1, Size = (int)x.Size }));
			}
		}

    }
}