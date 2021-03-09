using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Shop.Models;
using System;
using System.Web.Mvc;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Shop.Models.Slider;


namespace Shop.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;


        public HomeController()
        {
            _context = new ApplicationDbContext();
        }


        public ActionResult Test()
        {
            if (User.Identity.GetUserName() == string.Empty)
            {
                return Content("You must to have an account in this websiler");
            }
            string UserName = User.Identity.GetUserName();
            return Content("Hello !!!" + "" + UserName);
        }



        public ActionResult Index()
        {

            ViewBag.Brands = _context.Brands.ToList();

            ViewBag.SpecialOffer = _context.Products.OrderBy(p => p.Price).Take(9).ToList();

            ViewBag.SuggestionSlider = _context.Products.OrderByDescending(p => p.Price).Take(5);

            ViewBag.SlideShow = _context.Sliders.ToList();

            ViewBag.OffStarts = _context.Products.Select(p => new { OffStarts = p.OffStart });

            return View(_context.Products.ToList());
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public FileContentResult UserPhotos()
        {
            if (User.Identity.IsAuthenticated)
            {
                String userId = User.Identity.GetUserId();

                if (userId == null)
                {
                    string fileName = HttpContext.Server.MapPath(@"~/Images/download.png");

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
                string fileName = HttpContext.Server.MapPath(@"~/Images/download.png");

                byte[] imageData = null;
                FileInfo fileInfo = new FileInfo(fileName);
                long imageFileLength = fileInfo.Length;
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                imageData = br.ReadBytes((int)imageFileLength);
                return File(imageData, "image/png");

            }
        }



    }
}
