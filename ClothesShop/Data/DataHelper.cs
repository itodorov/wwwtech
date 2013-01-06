using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Script.Serialization;

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

					foreach (ClothesShop.SubCategory subCat in cat.SubCategories)
					{
						CategoryNode subNode = new CategoryNode();

						subNode.CategoryName = subCat.SubCategoryName + "(" + subCat.Products.Count + ")";
						subNode.CategoryId = subCat.ID;
						subNode.Level = 1;

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
							 select new { ProductName = product.Name, ProductID = product.ID, UnitPrice = product.Price };

				return result.ToList();
			}
		}

        public static ClothesShop.Product GetProduct(int productId)
        {
            ClothesShopEntities entities = new ClothesShopEntities();
            return entities.Products.Where(x => x.ID == productId).FirstOrDefault();
        }
    }
}