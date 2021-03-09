using Shop.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PhotosController : Controller
    {
        private ApplicationDbContext db;
        public PhotosController()
        {
            db = new ApplicationDbContext();
        }
        public ActionResult Index(string filter = null, int page = 1, int pageSize = 10)
        {
            var records = new PagedList<Photo>();
            ViewBag.filter = filter;

            records.Content = db.Photos
                        .Where(x => filter == null || (x.Decription.Contains(filter)))
                        .OrderByDescending(x => x.PhotoId)
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();

            // Count
            records.TotalRecords = db.Photos
                            .Where(x => filter == null || (x.Decription.Contains(filter))).Count();

            records.CurrentPage = page;
            records.PageSize = pageSize;

            return View(records);
        }


        [HttpGet]
        public ActionResult Create()
        {
            var photo = new Photo();
            return View(photo);
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


        [HttpPost]
        public ActionResult Create(Photo photo, IEnumerable<HttpPostedFileBase> files)
        {
            if (!ModelState.IsValid)
                return View(photo);
            if (files.Count() == 0 || files.FirstOrDefault() == null)
            {
                ViewBag.error = "Please choose a file";
                return View(photo);
            }

            var model = new Photo();
            foreach (var file in files)
            {
                if (file.ContentLength == 0) continue;

                model.Decription = photo.Decription;
                var fileName = Guid.NewGuid().ToString();
                var extension = System.IO.Path.GetExtension(file.FileName).ToLower();

                using (var img = System.Drawing.Image.FromStream(file.InputStream))
                {
                    model.ThumbPath = String.Format("~/GalleryImages/thumbs/{0}{1}", fileName, extension);
                    model.ImagePath = String.Format("~/GalleryImages/{0}{1}", fileName, extension);

                    // Save thumbnail size image, 100 x 100
                    SaveToFolder(img, fileName, extension, new Size(100, 100), model.ThumbPath);

                    // Save large size image, 800 x 800
                    SaveToFolder(img, fileName, extension, new Size(600, 600), model.ImagePath);
                }

                // Save record to database
                model.CreatedOn = DateTime.Now;
                db.Photos.Add(model);
                db.SaveChanges();
            }

            return RedirectPermanent("/photos");
        }

       
        public ActionResult Edit(int id)
        {
            var photo= db.Photos.Single(p => p.PhotoId == id);

            return Content("Path is ="+ photo.ImagePath);
        }



    }
}