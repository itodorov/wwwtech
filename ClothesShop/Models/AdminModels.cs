﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace ClothesShop.Models
{
	public class CategoryModel
	{
		[Required]
		[Display(Name = "Category name")]
		public string Name { get; set; }

		public int ID { get; set; }
	}

	public class SubcategoryModel
	{
		[Required]
		[Display(Name = "Subcategory name")]
		public string SubcategoryName { get; set; }

		public int ID { get; set; }

		[Required]
		[Display(Name = "Category")]
		public int CategoryID { get; set; }
	}

	public class PictureModel
	{
		public int ID { get; set; }
		public int ProductId { get; set; }
		public string FileName { get; set; }
	}
}
