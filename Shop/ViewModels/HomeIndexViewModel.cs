using Shop.Models.Product;
using Shop.Models.Slider;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shop.ViewModels
{
    public class HomeIndexViewModel
    {
        public IEnumerable<Slider> Sliders { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Brand> brands { get; set; }
        public IEnumerable<Tag> tags { get; set; }
        public IEnumerable<SKU> SKUs { get; set; }
        public IEnumerable<Status> Statuses { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public int Id { get; set; }
        [Display(Name = "نام محصول")]
        public string Name { get; set; }
        [UIHint("Html")]
        [Display(Name = "توضیحات ")]
        public string FullDescription { get; set; }
        [UIHint("Html")]
        [Display(Name = "توضیحات کوتاه ")]
        public string ShortDescription { get; set; }
        [Display(Name = "قیمت ")]
        public double Price { get; set; }
        [Display(Name = "قیمت پیشنمادی ")]
        public double OffPrice { get; set; }
        [Display(Name = "اسلایدر ")]
        public bool IsSlider { get; set; }
        public Status Status { get; set; }
        [Display(Name = "وضعیت")]
        public int StatusId { get; set; }
        [Display(Name = "وزن ")]
        public int Weight { get; set; }
        [Display(Name = "طول ")]
        public int length { get; set; }
        [Display(Name = "عرض ")]
        public int Width { get; set; }
        [Display(Name = "ارتفاع ")]
        public int Height { get; set; }
        [MaxLength]
        [Display(Name = "تصویر ")]
        public string ImageUrl { get; set; }
        public SKU SKU { get; set; }
        [Display(Name = "نام انبار")]
        public int SKUId { get; set; }
        public Category Category { get; set; }
        [Display(Name = "دسته بندی ")]
        public int CategoryId { get; set; }
        public Tag Tag { get; set; }
        [Display(Name = "برچسب ")]
        public int TagId { get; set; }
        [Display(Name = "دیگر برچسب ها ")]
        public string SelectedTagIds { get; set; }
        public string[] SelectedIdArray { get; set; }
        public Brand Brand { get; set; }
        [Display(Name = "برند ")]
        public int BrandId { get; set; }
        [Range(0, 10)]
        [Display(Name = "موجودی ")]
        public int NumberAvailable { get; set; }
        [Display(Name = "شروع تخفیف")]

        public DateTime? OffStart { get; set; }
        [Display(Name = "پایان تخفیف")]

        public DateTime? OffEnd { get; set; }
        [Display(Name = "اسلایدر")]
        public string SliderImageUrl { get; set; }
    }
}