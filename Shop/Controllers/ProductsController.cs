using Shop.Models;
using Shop.Models.Gallary;
using Shop.Models.Product;
using Shop.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Shop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        private ApplicationDbContext _context;
        public ProductsController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Products
        public ActionResult Index()
        {
            
            var viewModel = new ProductFormViewModel
            {
              
                Categories = _context.Categories.ToList(),
                tags = _context.Tags.ToList(),
                brands = _context.Brands.ToList(),
                SKUs=_context.SKUs.ToList(),
                Statuses=_context.Statuses.ToList()

            };

            return View("ProductForm", viewModel);
        }

       

        // POST: Brands/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost ,ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Product product, IEnumerable<HttpPostedFileBase> files)
        {
          

            if (!ModelState.IsValid)
            {
                var viewModel = new ProductFormViewModel
                {
                    Name = product.Name,
                    FullDescription = product.FullDescription,
                    Price = product.Price,
                    OffPrice = product.OffPrice,
                    ShortDescription = product.ShortDescription,
                    Weight = product.Weight,
                    Width = product.Width,
                    length = product.length,
                    Height = product.Height,
                    TagId = product.TagId,
                    BrandId = product.BrandId,
                    CategoryId = product.CategoryId,
                    StatusId = product.StatusId,
                    IsSlider = product.IsSlider
                };
                return View("ProductForm", viewModel);
            }
            if (product.Id == 0)
            {
                //        Gallary tbl = new Gallary();

                //        var allowedExtensions = new[] {
                //    ".Jpg", ".png", ".jpg", "jpeg"
                //};

                foreach (var file in files)
                {
                    if (file.ContentLength == 0) continue;
                    var fileName = Guid.NewGuid().ToString();
                    var extension = Path.GetExtension(file.FileName).ToLower();

                    using (var img = Image.FromStream(file.InputStream))
                    {
                        product.ImageUrl = string.Format("/GalleryImages/{0}{1}", fileName, extension);


                        // Save thumbnail size image, 100 x 100
                        SaveToFolder(img, fileName, extension, new Size(100, 100), product.ImageUrl);

                        // Save large size image, 800 x 800
                        SaveToFolder(img, fileName, extension, new Size(600, 600), product.ImageUrl);
                    }

                }



                product.TagId = 5;

                product.SelectedTagIds = string.Join(",", product.SelectedIdArray);

              
                

                //product.ImageUrl = file.ToString();
                //tbl.ImageUrl = file.ToString();

                //var fileName = Path.GetFileName(file.FileName); //getting only file name(ex-ganesh.jpg)  
                //var ext = Path.GetExtension(file.FileName); //getting the extension(ex-.jpg)  
                //if (allowedExtensions.Contains(ext)) //check what type of extension  
                //{
                //    string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                //    string myfile = name + "_" + product.Id + ext; //appending the name with id  
                       
                //                                               // store the file inside ~/project folder(Img)  
                //    var path = Path.Combine(Server.MapPath("~/Content/Gallary/"), myfile);
                //    product.ImageUrl = path;
                //    tbl.ImageUrl = path;
                //    _context.Gallaries.Add(tbl);
                //    _context.SaveChanges();
                //    file.SaveAs(path);
                //}
                //else
                //{
                //    ViewBag.message = "Please choose only Image file";
                //}
                
                _context.Products.Add(product);

            }
            else
            {
                var productInDb = _context.Products.Single(p => p.Id == product.Id);

                //way 1 have Optinal providers in Magic string
                //TryUpdateModel(customerInDb,"",new string[] {"Name","Birthday" });

                //way2 have Optinal providers in lamda expretion
                productInDb.Name = product.Name;
                productInDb.FullDescription = product.FullDescription;
                productInDb.Price = product.Price;
                productInDb.OffPrice = product.OffPrice;
                productInDb.ShortDescription = product.ShortDescription;
                productInDb.Weight = product.Weight;
                productInDb.Width = product.Width;
                productInDb.length = product.length;
                productInDb.Height = product.Height;
                productInDb.TagId = product.TagId;
                productInDb.BrandId = product.BrandId;
                productInDb.CategoryId = product.CategoryId;
                productInDb.SKUId = product.SKUId;
                productInDb.IsSlider = product.IsSlider;
                productInDb.StatusId = product.StatusId;


                //way3 without providers
                //Mapper.Map(customer, customerInDb);
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Products");
            //    if (ModelState.IsValid)
            //    {
            //        _context.Products.Add(product);
            //        _context.SaveChanges();
            //        return RedirectToAction("Index");
            //    }

            //    return View(product);
            //}
        }


        public ActionResult All_Products()
        {

          return View("IndexProducts");
        }



       
        // GET: Products1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = _context.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrandId = new SelectList(_context.Brands, "Id", "Name", product.BrandId);
            ViewBag.CategoryId = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            ViewBag.TagId = new SelectList(_context.Tags, "Id", "Name", product.TagId);
            ViewBag.SkuId = new SelectList(_context.SKUs, "Id", "Name", product.SKUId);
            ViewBag.StatusId = new SelectList(_context.Statuses, "Id", "Name", product.StatusId);

            return View(product);
        }

        // POST: Products1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,FullDescription,ShortDescription,Price,OffPrice,IsSlider,StatusId,Weight,length,Width,Height,ImageUrl,CategoryId,TagId,SelectedTagIds,BrandId,NumberAvailable,SKUId")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(product).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("All_Products");
            }
            ViewBag.BrandId = new SelectList(_context.Brands, "Id", "Name", product.BrandId);
            ViewBag.CategoryId = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            ViewBag.TagId = new SelectList(_context.Tags, "Id", "Name", product.TagId);
            ViewBag.SkuId = new SelectList(_context.SKUs, "Id", "Name", product.SKUId);
            ViewBag.StatusId = new SelectList(_context.Statuses, "Id", "Name", product.StatusId);
          
            
            return View(product);
        }




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


        private void SaveToFolder(Image img, string fileName, string extension, Size newSize, string pathToSave)
        {
            // Get new resolution
            Size imgSize = NewImageSize(img.Size, newSize);
            using (System.Drawing.Image newImg = new Bitmap(img, imgSize.Width, imgSize.Height))
            {
                newImg.Save(Server.MapPath(pathToSave), img.RawFormat);
            }
        }


        public ActionResult AddProduct()
        {

            return View(_context.Products.ToList());
        }

      

    }

}

