using Shop.Models.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shop.ViewModels
{
    public class ProductFormViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Brand> brands { get; set; }
        public IEnumerable<Tag> tags { get; set; }
        public IEnumerable<SKU> SKUs { get; set; }
        public IEnumerable<Status> Statuses { get; set; }
        public string SelectedTagIds { get; set; }
        public string[] SelectedIdArray { get; set; }

        public int Id { get; set; }
        [Display(Name = "نام محصول")]
        public string Name { get; set; }
        [UIHint("Html")]
        [Display(Name ="توضیحات محصول")]
        public string FullDescription { get; set; }
        [UIHint("Html")]
        [Display(Name ="توضیحات کوتاه")]
        public string ShortDescription { get; set; }
        [Display(Name ="قیمت محصول")]
        public double Price { get; set; }
        [Display(Name ="قیمت شگفت انگیز")]
        public double OffPrice { get; set; }
        [Display(Name ="آیا محصول به عنوان اسلایدر نمایش داده شود؟")]
        public bool IsSlider { get; set; }
        public SKU SKU { get; set; }
        [Display(Name = "فروشگاه ")]
        public int SKUId { get; set; }
        public Status Status { get; set; }
        [Display(Name = "وضعیت ")]
        public int StatusId { get; set; }
        [Display(Name ="تصویر محصول")]
        public string ImageUrl { get; set; }
        [Display(Name ="وزن ")]
        public int Weight { get; set; }
        [Display(Name ="طول ")]
        public int length { get; set; }
        [Display(Name ="عرض ")]
        public int Width { get; set; }
        [Display(Name ="ارتفاع ")]
        public int Height { get; set; }
        public Category Category { get; set; }
        [Display(Name ="دسته بندی محصول")]
        public int CategoryId { get; set; }
        public Tag Tag { get; set; }
        [Display(Name ="برچسب محصول")]
        public int TagId { get; set; }
        public Brand Brand { get; set; }
        [Display(Name ="برند محصول")]
        public int BrandId { get; set; }
        [Display(Name ="تعداد موجودی")]
        [Range(0,10)]
        public int NumberAvailable { get; set; }

        [Display(Name = "شروع تخفیف")]
        public DateTime? OffStart { get; set; }
        [Display(Name = "پایان تخفیف")]
        public DateTime? OffEnd { get; set; }



    }
}