using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace ClothesShop.Models
{
	public class AddCategoryModel
	{
		[Required]
		[Display(Name = "Category name")]
		public string Name { get; set; }

		public int ID { get; set; }
	}

	public class AddSubcategoryModel
	{
		[Required]
		[Display(Name = "Subcategory name")]
		public string SubcategoryName { get; set; }

		public List<AddCategoryModel> Categories { get; set; }

		[Required]
		[Display(Name = "Category")]
		public int ParentCategoryID { get; set; }
	}

	public class RemoveCategoryModel
	{
		public List<AddCategoryModel> Categories { get; set; }

		[Required]
		[Display(Name = "Category")]
		public int ID { get; set; }
	}
}
