using PagedList;
using Shop.Models;
using Shop.Models.Page;
using Shop.Models.Product;
using Shop.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Shop.Models.Order;

namespace Shop.Controllers
{
 
    public class PagesController : Controller
    {
        private ApplicationDbContext _context;
        public PagesController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Posts
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var viewModel = new ProductFormViewModel
            {

                Categories = _context.Categories.ToList()

            };

            return View("PageForm", viewModel);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Page page, IEnumerable<HttpPostedFileBase> files)
        {


            if (!ModelState.IsValid)
            {
                var viewModel = new ProductFormViewModel
                {
                    Name = page.Name,
                    FullDescription = page.FullDescription,
                    ShortDescription = page.ShortDescription,

                };
                return View("PageForm", viewModel);
            }
            if (page.Id == 0)
            {

                foreach (var file in files)
                {
                    if (file.ContentLength == 0) continue;
                    var fileName = Guid.NewGuid().ToString();
                    var extension = Path.GetExtension(file.FileName).ToLower();

                    using (var img = Image.FromStream(file.InputStream))
                    {
                        page.ImageUrl = string.Format("/GalleryImages/{0}{1}", fileName, extension);


                        // Save thumbnail size image, 100 x 100
                        SaveToFolder(img, fileName, extension, new Size(100, 100), page.ImageUrl);

                        // Save large size image, 800 x 800
                        SaveToFolder(img, fileName, extension, new Size(600, 600), page.ImageUrl);
                    }

                }
                _context.Pages.Add(page);
            }
            else
            {
                var productInDb = _context.Products.Single(p => p.Id == page.Id);

                productInDb.Name = page.Name;
                productInDb.FullDescription = page.FullDescription;
                productInDb.ShortDescription = page.ShortDescription;
                productInDb.CategoryId = page.CategoryId;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Pages");

        }
        [Authorize(Roles = "Admin")]
        public Size NewImageSize(Size imageSize, Size newSize)
        {
            Size finalSize;
            double tempval;
            if (imageSize.Height > newSize.Height || imageSize.Width > newSize.Width)
            {
                if (imageSize.Height > imageSize.Width)
                    tempval = newSize.Height / (imageSize.Height * 1.0);
                else
                    tempval = newSize.Width / (imageSize.Width * 1.0);

                finalSize = new Size((int)(tempval * imageSize.Width), (int)(tempval * imageSize.Height));
            }
            else
                finalSize = imageSize; // image is already small size

            return finalSize;
        }

        [Authorize(Roles = "Admin")]
        private void SaveToFolder(Image img, string fileName, string extension, Size newSize, string pathToSave)
        {
            // Get new resolution
            Size imgSize = NewImageSize(img.Size, newSize);
            using (System.Drawing.Image newImg = new Bitmap(img, imgSize.Width, imgSize.Height))
            {
                newImg.Save(Server.MapPath(pathToSave), img.RawFormat);
            }
        }
        [Authorize(Roles = "Admin")]
        public ActionResult All_Pages()
        {

            return View("IndexPage");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Page page = _context.Pages.Find(id);
            if (page == null)
            {
                return HttpNotFound();
            }

            ViewBag.CategoryId = new SelectList(_context.Categories, "Id", "Name", page.CategoryId);


            return View(page);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,FullDescription,ShortDescription,ImageUrl,CategoryId")] Page page)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(page).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("All_Pages");
            }

            ViewBag.CategoryId = new SelectList(_context.Categories, "Id", "Name", page.CategoryId);


            return View(page);
        }
        [AllowAnonymous]
        public ActionResult Categories() 
        {

            ViewBag.Category = _context.Categories.ToList();
            ViewBag.Brand = _context.Brands.ToList();

            return View(_context.Categories.ToList());
        }
       
         [AllowAnonymous]
        public ActionResult SingleProduct(int id)
        {
            //string userId = User.Identity.GetUserId();
            //var order = _context.Orders.SingleOrDefault(o => o.UserId == userId);
            //if (order != null)
            //{
            //    ViewBag.OrderId = order.Id;
            //    var calculatePrices = _context.OrderDetails.Where(o => o.OrderId == order.Id).Select(o => o.SubTotalPrice);
            //    double Sum = 0;
            //    foreach (var item in calculatePrices)
            //    {
            //        Sum = Sum + item;
            //    }
            //    var seletedOrder = _context.Orders.Find(order.Id);
            //    seletedOrder.TotalPrice = Sum.ToString();

            //    _context.SaveChanges();
            //}
            //else
            //{
            //    if (userId != null)
            //    {
            //        var status = _context.OrderStatuses.Single(s => s.Name == "ثبت اولیه").Id;
            //        var CoponId = _context.OrderCopons.Single(s => s.Name == "ندارد").Id;
            //        Order order1 = new Order();
            //        order1.UserId = userId;
            //        order1.OrderStatusId = status;
            //        order1.OrderCoponId = CoponId;
            //        order1.FactorNumber = DateTime.Now.ToString("yy/mm/dd") + User.Identity.GetUserName();
            //        _context.Orders.Add(order1);
            //        _context.SaveChanges();
            //        return RedirectToAction("Index");
            //    }

            //}

            var product = _context.Products.Include(p=>p.Brand).Include(p=>p.Category).Include(p=>p.Tag).Include(p=>p.SKU).Include(p=>p.Status).Single(p=>p.Id== id);

            return View(product);
        }

        public ActionResult Error() 
        {
            
            var pageInDb = _context.Pages.Single(p => p.Id == 1);
            return View("CustomPage",pageInDb);
        }
        [AllowAnonymous]
        public ActionResult Blog(int id)
        {
            var pageInDb = _context.Pages.Single(p => p.Id == id);
            return View("CustomPage", pageInDb);
        }
        [AllowAnonymous]
        public ActionResult Blogs()
        {
            var blogs = _context.Pages.Include(p=>p.Category).ToList();
            return View("Blogs", blogs);
        }
        [AllowAnonymous]
        public ActionResult ByCategory(string sortOrder, int id, int? page)
        {
            ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Price";
            ViewBag.DateSortParm = sortOrder == "Available" ? "available_desc" : "Available";
            ViewBag.AllProduct = sortOrder == "All_Product" ? "All_Product_desc" : "All_Product";

            ViewBag.Category = _context.Categories.ToList();
            ViewBag.Brand = _context.Brands.ToList();



            page = 1;
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var students = _context.Products.Include(p => p.Brand).Include(p => p.Category).Where(p => p.CategoryId == id).ToList();


            return View("Sort", students.ToPagedList(pageNumber, pageSize));


        }
        [AllowAnonymous]
        public ActionResult ByBrand(string sortOrder, int id,int? page)
        {
            ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Price";
            ViewBag.DateSortParm = sortOrder == "Available" ? "available_desc" : "Available";
            ViewBag.AllProduct = sortOrder == "All_Product" ? "All_Product_desc" : "All_Product";

            ViewBag.Category = _context.Categories.ToList();
            ViewBag.Brand = _context.Brands.ToList();
            
            int pageSize = _context.Products.Where(p=>p.BrandId==id).Count();
            if (pageSize == 0)
            {
                pageSize = 72;
            }
            int pageNumber = (page ?? 1);
            var students = _context.Products.Include(p => p.Brand).Include(p => p.Category).Where(p => p.BrandId == id).OrderBy(p=>p.NumberAvailable);


            return View("Sort", students.ToPagedList(pageNumber, pageSize));
        }



        [AllowAnonymous]
        public ActionResult Sort(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "price" : "";
            //ViewBag.PriceSortParm = String.IsNullOrEmpty(sortOrder) ? "price" : "";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Price";
            ViewBag.DateSortParm = sortOrder == "Available" ? "available_desc" : "Available";
            ViewBag.AllProduct = sortOrder == "All_Product" ? "All_Product_desc" : "All_Product";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }


            ViewBag.CurrentFilter = searchString;

            var students = _context.Products.Include(p => p.Brand).Include(p => p.Category);

            ViewBag.Category = _context.Categories.ToList();
            ViewBag.Brand = _context.Brands.ToList();

            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Include(p => p.Brand).Include(p => p.Category).Where(s => s.Name.Contains(searchString));
            }



            switch (sortOrder)
            {
                case "Price":
                    students = students.Where(s=>s.NumberAvailable!=0).OrderBy(s => s.Price);
                    break;
                case "price_desc":
                    students = students.Where(s => s.NumberAvailable != 0).OrderByDescending(s => s.Price);
                    break;
                case "Available":
                    students = students.Where(s => s.NumberAvailable != 0).OrderBy(s=>s.Price);
                    break;
                case "available_desc":
                    students = students.Where(s => s.NumberAvailable != 0).OrderByDescending(s => s.NumberAvailable);
                    break;
                case "All_Product":
                    students = students.OrderBy(s => s.NumberAvailable);
                    break;
                case "All_Product_desc":
                    students = students.OrderByDescending(s => s.NumberAvailable);
                    break;
                default:
                    students = students.Where(s => s.NumberAvailable != 0).OrderBy(s => s.Price);
                    break;
            }
            int pageSize = 12;
            int pageNumber = (page ?? 1);
            return View(students.ToPagedList(pageNumber, pageSize));
        }
        [Route("Orders/AddToCard/SelectedProduct={productId}/YourOrderId={orderId}")]
        public ActionResult AddToCard(int productId, int? orderId)
        {
            var checkLogin = User.Identity.GetUserId();
            if (checkLogin == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (orderId == null)
            {
                return Content("لاگینی اما تا حالا هیچی نخریدی");
            }
            var orderInProcces = _context.Orders.Include(o=>o.OrderStatus).Single(o=>o.Id==orderId);
            if (orderInProcces.OrderStatus.Name != "ثبت اولیه")
            {
                return Content("شما دارای سفارش در راه می باشید در صورتی که تمایل به ویرایش آن دارید با شماره 33803692 تماس بگیرید");
            }
            var ordersDetailsInDb = _context.OrderDetails.SingleOrDefault(o => (o.OrderId == orderId && o.ProductId == productId));

            var productInDb = _context.Products.Find(productId);
            if (orderId != null)
            {
                if (ordersDetailsInDb == null)
                {

                    OrderDetail orderDetails = new OrderDetail();
                    orderDetails.OrderId = Convert.ToInt32(orderId);
                    orderDetails.ProductId = productId;
                    orderDetails.Quantity = 1;
                    orderDetails.Created_at = DateTime.Now;
                    orderDetails.SinglePrice = Convert.ToInt32(productInDb.Price);
                    orderDetails.SubTotalPrice = (orderDetails.SinglePrice) * (orderDetails.Quantity);
                    _context.OrderDetails.Add(orderDetails);
                    _context.SaveChanges();
                    return Content("<script language='javascript' type='text/javascript'> location.href='/pages/singleproduct/" + productId + "'; alert('محصول با موفقیت به سبد خرید شما اضافه گردید.');</script>");

                }
                else
                {
                    var Edit = _context.OrderDetails.Find(ordersDetailsInDb.Id);
                    if (Edit.Quantity >= 3)
                    {
                        return Content("<script language='javascript' type='text/javascript'> location.href='javascript:history.back()'; alert('شما تا کنون سه عدد از این محصول را دارید،خرید دوباره این محصول برای شما مجاز نیست.'); </script>");

                    }
                    Edit.Updated_at = DateTime.Now;
                    Edit.Quantity++;
                    Edit.SubTotalPrice = Edit.Quantity * Edit.SinglePrice;
                    _context.SaveChanges();
                    //return Content("<script language='javascript' type='text/javascript'>alert('محصول موجود است و یک واحد اضافه شد!');location.href='/orders/Index';</script>");
                    return Content("<script language='javascript' type='text/javascript'> location.href='/pages/singleproduct/"+productId+"'; alert('محصول به تعداد یک عدد دیگر به سبد خرید شما اضافه گردید.');</script>");


                }


            }

            return RedirectToAction("Index");
        }


    }

    //[Route("Pages/Filter/{query}/{catId?}/{brandId?}")]
    //public ActionResult Filter(string query,int? catId,int? brandId) 
    //{
    //    ViewBag.Category = _context.Categories.ToList();
    //    ViewBag.Brand = _context.Brands.ToList();

    //    if (query == "ByPrice")
    //    {
    //        if (catId != null && brandId != null)
    //        {
    //            var ByPrice = _context.Products.Include(p => p.Brand).Include(p => p.Category).Include(p => p.Tag).Include(p => p.SKU).Include(p => p.Status).Where(p => p.CategoryId == catId && p.BrandId == brandId).OrderBy(p => p.Price);
    //            return View("Category", ByPrice);
    //        }
    //        return View("Category", _context.Products.Include(p => p.Brand).Include(p => p.Category).Include(p => p.Tag).Include(p => p.SKU).Include(p => p.Status).OrderBy(p => p.Price));
    //    }
    //    else if (query == "ByOffPrice")
    //    {
    //        if (catId != null && brandId != null)
    //        {
    //            var ByOFFPrice = _context.Products.Include(p => p.Brand).Include(p => p.Category).Include(p => p.Tag).Include(p => p.SKU).Include(p => p.Status).Where(p => p.CategoryId == catId && p.BrandId == brandId).OrderBy(p => p.OffPrice);
    //            return View("Category", ByOFFPrice);
    //        }
    //        return View("Category", _context.Products.Include(p => p.Brand).Include(p => p.Category).Include(p => p.Tag).Include(p => p.SKU).Include(p => p.Status).OrderBy(p => p.OffPrice));
    //    }
    //    else if (query == "ByAvailable")
    //    {
    //        if (catId != null && brandId != null)
    //        {
    //            var ByPrice = _context.Products.Include(p => p.Brand).Include(p => p.Category).Include(p => p.Tag).Include(p => p.SKU).Include(p => p.Status).Where(p => p.CategoryId == catId && p.BrandId == brandId).OrderBy(p => p.NumberAvailable);
    //            return View("Category", ByPrice);
    //        }
    //        return View("Category", _context.Products.Include(p => p.Brand).Include(p => p.Category).Include(p => p.Tag).Include(p => p.SKU).Include(p => p.Status).OrderBy(p => p.NumberAvailable));
    //    }

    //    return Content("Filter");

    //}


    //[Route("Pages/Filter/{catId?}/{brandId?}")]
    //public ActionResult FilterCategoryAndBrand( int? catId, int? brandId)
    //{
    //    return Content("Na baba");
    //}
    //[Route("Pages/Filter/{catId?}")]
    //public ActionResult FilterByCategory(int? catId)
    //{
    //    return Content("Na baba");
    //}

    //[Route("Pages/Filter/{brandId?}")]
    //public ActionResult FilterByBrand(int? brandId)
    //{
    //    return Content("Na baba");
    //}

    //public ActionResult SortBy(int? id,string categoryId, int? brandId)
    //{

    //    //0-are


    //    //var a = categoryId.Split["-"];


    //    ViewBag.Category = _context.Categories.ToList();
    //    ViewBag.Brand = _context.Brands.ToList();

    //    if (id == 1 )
    //    {
    //        var productsByPrice = _context.Products.Include(p => p.Brand).Include(p => p.Category).Include(p => p.Tag).Include(p => p.SKU).Include(p => p.Status).OrderBy(p=>p.Price);
    //        return View("Category", productsByPrice);

    //    }
    //    else if (id == 2)
    //    {
    //        var productsByOffPrice = _context.Products.Include(p => p.Brand).Include(p => p.Category).Include(p => p.Tag).Include(p => p.SKU).Include(p => p.Status).OrderByDescending(p => p.OffPrice);
    //        return View("Category",productsByOffPrice);

    //    }
    //    else if (id == 3)
    //    {
    //        var productsByAvaialble = _context.Products.Include(p => p.Brand).Include(p => p.Category).Include(p => p.Tag).Include(p => p.SKU).Include(p => p.Status).OrderByDescending(p => p.NumberAvailable);
    //        return View("Category",productsByAvaialble);

    //    }
    //    var products = _context.Products.Include(p => p.Brand).Include(p => p.Category).Include(p => p.Tag).Include(p => p.SKU).Include(p => p.Status);

    //    return View("Category",products);
}
