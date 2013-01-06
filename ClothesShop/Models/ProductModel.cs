using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothesShop.Models
{
    public class QuantityModel
    {
        public int Size { get; set; }
        public int Quantity { get; set; }
    }

    public class ProductModel
    {
        public int ID { get; set; }
        public int SubCategoryID { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public string NameEN { get; set; }
        public string Description { get; set; }
        public string DescriptionEN { get; set; }
        public decimal Price { get; set; }
        public decimal Weight { get; set; }
        public bool Special { get; set; }
        public int QuantityType { get; set; }
        public int Quantity { get; set; }
        public List<QuantityModel> Quantities { get; set; }

        public ProductModel(ClothesShop.Product prod)
        {
            this.ID = prod.ID;
            this.SubCategoryID = prod.SubCategoryID;
            this.Number = prod.No;
            this.Name = prod.Name;
            this.Description = prod.Description;
            this.DescriptionEN = prod.DescriptionEN;
            this.Price = prod.Price;
            this.Weight = prod.Weight;
            this.Special = prod.Special;
            this.QuantityType = prod.QuantityType;
            this.Quantity = prod.Quantity;
            this.Quantities = new List<QuantityModel>();

            foreach (ClothesShop.Quantity quan in prod.Quantities)
            {
                this.Quantities.Add(new QuantityModel() { Size = (int)quan.Size, Quantity = quan.Quantity1 });
            }
        }

        public ProductModel()
        {
            this.Quantities = new List<QuantityModel>();
        }
    }
}