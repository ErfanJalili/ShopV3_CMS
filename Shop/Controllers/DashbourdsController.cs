
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Shop.Models;
using Shop.Models.Gallary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DashbourdsController : Controller
    {

        private ApplicationDbContext _context;

        public DashbourdsController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Dashbourds
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public FileContentResult UserPhotos()
        {
            if (User.Identity.IsAuthenticated)
            {
                String userId = User.Identity.GetUserId();

                if (userId == null)
                {
                    string fileName = HttpContext.Server.MapPath(@"~/Images/noImg.png");

                    byte[] imageData = null;
                    FileInfo fileInfo = new FileInfo(fileName);
                    long imageFileLength = fileInfo.Length;
                    FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    imageData = br.ReadBytes((int)imageFileLength);

                    return File(imageData, "image/png");

                }
                // to get the user details to load user Image    
                var bdUsers = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
                var userImage = bdUsers.Users.Where(x => x.Id == userId).FirstOrDefault();

                return new FileContentResult(userImage.UserPhoto, "image/jpeg");
            }
            else
            {
                string fileName = HttpContext.Server.MapPath(@"~/Images/noImg.jpg");

                byte[] imageData = null;
                FileInfo fileInfo = new FileInfo(fileName);
                long imageFileLength = fileInfo.Length;
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                imageData = br.ReadBytes((int)imageFileLength);
                return File(imageData, "image/png");

            }
        }

        public ActionResult Upload()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Upload(FormCollection fc, HttpPostedFileBase file)
        {
            Gallary tbl = new Gallary();
            var allowedExtensions = new[] {
            ".Jpg", ".png", ".jpg", "jpeg"
        };
            tbl.ImageUrl = file.ToString(); //getting complete url  
            tbl.Name = fc["Name"].ToString();
            tbl.Description = fc["Description"].ToString();
            tbl.Alt = fc["Alt"].ToString();
            var fileName = Path.GetFileName(file.FileName); //getting only file name(ex-ganesh.jpg)  
            var ext = Path.GetExtension(file.FileName); //getting the extension(ex-.jpg)  
            if (allowedExtensions.Contains(ext)) //check what type of extension  
            {
                string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                string myfile = name + "_" + tbl.Id + ext; //appending the name with id  
                                                           // store the file inside ~/project folder(Img)  
                var path = Path.Combine(Server.MapPath("~/Content/Gallary/"), myfile);
                tbl.ImageUrl = path;
                _context.Gallaries.Add(tbl);
                _context.SaveChanges();
                file.SaveAs(path);
            }
            else
            {
                ViewBag.message = "Please choose only Image file";
            }
            return View();
        }

    }
    
}