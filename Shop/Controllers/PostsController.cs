using Shop.Models;
using Shop.Models.Post;
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
    public class PostsController : Controller
    {
        private ApplicationDbContext _context;
        public PostsController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Posts
        public ActionResult Index()
        {
            var viewModel = new ProductFormViewModel
            {

                Categories = _context.Categories.ToList()
                
            };

            return View("PostsForm", viewModel);
        }

        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Post post, IEnumerable<HttpPostedFileBase> files)
        {


            if (!ModelState.IsValid)
            {
                var viewModel = new ProductFormViewModel
                {
                    Name = post.Name,
                    FullDescription = post.FullDescription,
                    ShortDescription = post.ShortDescription,
                   
                };
                return View("PostsForm", viewModel);
            }
            if (post.Id == 0)
            {
              
                foreach (var file in files)
                {
                    if (file.ContentLength == 0) continue;
                    var fileName = Guid.NewGuid().ToString();
                    var extension = Path.GetExtension(file.FileName).ToLower();

                    using (var img = Image.FromStream(file.InputStream))
                    {
                        post.ImageUrl = string.Format("/GalleryImages/{0}{1}", fileName, extension);


                        // Save thumbnail size image, 100 x 100
                        SaveToFolder(img, fileName, extension, new Size(100, 100), post.ImageUrl);

                        // Save large size image, 800 x 800
                        SaveToFolder(img, fileName, extension, new Size(600, 600), post.ImageUrl);
                    }

                }
                _context.Posts.Add(post);
            }
            else
            {
                var productInDb = _context.Products.Single(p => p.Id == post.Id);

                productInDb.Name = post.Name;
                productInDb.FullDescription = post.FullDescription;
                productInDb.ShortDescription = post.ShortDescription;
                productInDb.CategoryId = post.CategoryId;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Posts");
           
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

        public ActionResult All_Posts()
        {

            return View("IndexPost");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = _context.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
          
            ViewBag.CategoryId = new SelectList(_context.Categories, "Id", "Name", post.CategoryId);
           

            return View(post);
        }

        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,FullDescription,ShortDescription,ImageUrl,CategoryId")] Post post)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(post).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("All_Products");
            }
           
            ViewBag.CategoryId = new SelectList(_context.Categories, "Id", "Name", post.CategoryId);
           

            return View(post);
        }
    }
}