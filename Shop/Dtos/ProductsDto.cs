using Shop.Models.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shop.Dtos
{
    public class ProductsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [UIHint("Html")]
        public string FullDescription { get; set; }
        public string ShortDescription { get; set; }
        public double Price { get; set; }
        public double OffPrice { get; set; }
        public bool IsSlider { get; set; }
        public int SKU { get; set; }
        public Status StatusItem { get; set; }
        public int Weight { get; set; }
        public int length { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        [MaxLength]
        public string ImageUrl { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public Tag Tag { get; set; }
        public int TagId { get; set; }
        public string SelectedTagIds { get; set; }
        public string[] SelectedIdArray { get; set; }
        public Brand Brand { get; set; }
        public int BrandId { get; set; }
        [Range(0, 10)]
        public int NumberAvailable { get; set; }
        public enum Status
        {
            موجوددرانبار = 0,
            موجودنیست = 1,
            کاتالوگ = 2,
            سفارشی = 3
        }

    }
}