using System;
using System.Collections.Generic;

namespace ClothesShop.Data
{
    public class CategoryNode
    {
        public string CategoryName { get; set; }
        public int Level { get; set; }
        public int CategoryId { get; set; }
		public bool HasChildren { get; set; }
        public List<CategoryNode> ChildNodes { get; set; }

        public CategoryNode()
        {
            ChildNodes = new List<CategoryNode>();
        }
    }
}