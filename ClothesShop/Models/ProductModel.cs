﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ClothesShop.Models
{
	public enum QuantityType : byte
	{
		Unlimited,
		Limited,
		LimitedBySize
	}

    public class QuantityModel
    {
		public int ID { get; set; }
		public int ProductId { get; set; }
        public int Size { get; set; }
        public int Quantity { get; set; }
    }

    public class ProductModel
    {
		[Editable(false)]
        public int ID { get; set; }

		[Required]
		[Display(Name = "Sub category")]
        public int SubCategoryID { get; set; }

		[Required]
		[Display(Name = "Number")]
        public int Number { get; set; }

		[Required]
		[Display(Name = "Name")]
        public string Name { get; set; }

		[Required]
		[Display(Name = "English name")]
        public string NameEN { get; set; }

		[Display(Name = "Description")]
        public string Description { get; set; }

		[Display(Name = "English description")]
        public string DescriptionEN { get; set; }

		[Required]
		[Display(Name = "Price")]
		[DisplayFormat(DataFormatString="${0:N2}")]
        public decimal Price { get; set; }

		[Required]
		[Display(Name = "Weight")]
        public decimal Weight { get; set; }

		[Required]
		[Display(Name = "Special offer")]
        public bool Special { get; set; }

		[Required]
		[Display(Name = "Quantity type")]
		[EnumDataType(typeof(QuantityType))]
        public int QuantityType { get; set; }

		[Required]
		[Display(Name = "Quantity")]
        public int Quantity { get; set; }

		[Editable(false)]
		public string CategoryName { get; private set; }

		[Editable(false)]
        public List<QuantityModel> Quantities { get; set; }

		[Editable(false)]
		public List<PictureModel> Pictures { get; set; }

        public ProductModel(ClothesShop.Product prod)
        {
			this.Entity = prod;
        }

		public ClothesShop.Product Entity
		{
			set
			{
				ClothesShop.Product prod = value;
				this.ID = prod.ID;
				this.SubCategoryID = prod.SubCategoryID;
				this.Number = prod.No;
				this.Name = prod.Name;
				this.NameEN = prod.NameEN;
				this.Description = prod.Description;
				this.DescriptionEN = prod.DescriptionEN;
				this.Price = prod.Price;
				this.Weight = prod.Weight;
				this.Special = prod.Special;
				this.QuantityType = prod.QuantityType;
				this.Quantity = prod.Quantity;
				this.Quantities = new List<QuantityModel>();

				if (prod.SubCategory != null && prod.SubCategory.Category != null)
				{
					this.CategoryName = prod.SubCategory.Category.CategoryName + " - " + prod.SubCategory.SubCategoryName;
				}

				this.Pictures = prod.Images.Select(x => new PictureModel(){ FileName = x.FileName, ID = x.ID, ProductId = x.ProductID}).ToList();

				foreach (ClothesShop.Quantity quan in prod.Quantities)
				{
					this.Quantities.Add(new QuantityModel() { ID = quan.ID, ProductId = prod.ID , Size = (int)quan.Size, Quantity = quan.Quantity1 });
				}
			}
		}

        public ProductModel()
        {
            this.Quantities = new List<QuantityModel>();
        }
    }
}