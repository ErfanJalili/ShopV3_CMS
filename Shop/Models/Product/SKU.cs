using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shop.Models.Product
{
    public class SKU
    {
        public int Id { get; set; }
        [Display(Name="نام فروشگاه")]
        public string Name { get; set; }
        [Display(Name="نام مسئول")]
        public string ManagerName { get; set; }
        [Display(Name="توضیحات ")]
        public string Description { get; set; }
        [Display(Name="آدرس")]
        public string Address { get; set; }
        [Display(Name="تلفن ثابت")]
        public string Phone { get; set; }
        [Display(Name="موبایل")]
        public string Mobile { get; set; }
        [Display(Name="وب سایت")]
        public string WebSite { get; set; }
        [Display(Name="اینستاگرام")]
        public string Instagram { get; set; }
        [Display(Name="فیس بوک")]
        public string Facebook { get; set; }
        [Display(Name="تگرام")]
        public string Telegram { get; set; }
        [Display(Name="واتس اپ")]
        public string Whatsapp { get; set; }
        [Display(Name="ساعت کار روزانه")]
        public string StartTime { get; set; }
        [Display(Name="ساعت کار شبانه")]
        public string EndTime{ get; set; }
        [Display(Name="گوگل مپ")]
        public string GoogleLocation { get; set; }
        [Display(Name="نشان")]
        public string NeshanLocation { get; set; }


    }
}