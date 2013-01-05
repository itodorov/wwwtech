using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace ClothesShop.Data
{
    public class DataHelper
    {
        public static string GetCategoriesTree()
        {
            ClothesShopEntities entities = new ClothesShopEntities();
            List<CategoryNode> result = new List<CategoryNode>();
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

            var jsonSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            string json = jsonSerializer.Serialize(result);
            return json;
        } 
    }
}