using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using System.ComponentModel;

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

		[Editable(false)]
		public string CategoryName { get; set; }

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

	public class OrderItem
	{
		public int ID { get; set; }
		public string Username { get; set; }
		public DateTime Date { get; set; }
	}

	public class OrdersModel : List<OrderItem>
	{
		[Required]
		[DisplayName("Start date")]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d.M.yyyy}")]
		public DateTime StartDate { get; set; }

		[Required]
		[DisplayName("End date")]
		[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d.M.yyyy}")]
		public DateTime EndDate { get; set; }

		public OrdersModel(DateTime startDate, DateTime endDate)
		{
			StartDate = startDate;
			EndDate = endDate;
		}
	}
}
