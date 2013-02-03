using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothesShop.Models
{
    public class BasketItem
    {
        public ProductModel Product {get;set;}
        public int Quantity {get;set;}
        public int? Size {get;set;}
        
        public BasketItem(ProductModel product, int quantity, int? size)
        {
            this.Product = product;
            this.Quantity = quantity;
            this.Size = size;
        }

        public BasketItem(ProductModel product)
        {
            this.Product = product;
            this.Quantity = 1;
            this.Size = 0;
        }
    }

    public class BasketModel : List<BasketItem>
    {
        
    }
}