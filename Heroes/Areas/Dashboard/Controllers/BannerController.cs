using Heroes.Data;
using Heroes.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Heroes.Areas.Dashboard.Controllers
{
    [Area("dashboard")]
    [Authorize]
    public class BannerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BannerController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var banner = _context.Banners.FirstOrDefault();
            return View(banner);
        }

        public IActionResult Create()
        {
            var banner = _context.Banners.FirstOrDefault();

            if (banner != null)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Create(Banner banner, IFormFile NewPhoto)
        {
            var fileExtation = Path.GetExtension(NewPhoto.FileName);
            if (fileExtation != ".jpg")
            {
                ViewBag.PhotoError = "Only photos";
                return View();
            }
            var MyPhoto = Guid.NewGuid().ToString() + Path.GetExtension(NewPhoto.FileName);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/admin/img", MyPhoto);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                NewPhoto.CopyTo(stream);
            }
            banner.PhotoURL = "/admin/img/" + MyPhoto;
            _context.Banners.Add(banner);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {

            var banner = _context.Banners.FirstOrDefault(x => x.Id == id);
            if (banner == null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]

        public IActionResult Delete(Banner Banner)
        {
            if (Banner == null)
            {
                return RedirectToAction("Index");
            }
            _context.Banners.Remove(Banner);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {

            if (id == null)
            {
                return NotFound();
            }
            var banner = _context.Banners.FirstOrDefault(x => x.Id == id);
            if (banner == null)
            {
                return NotFound();
            }
            return View(banner);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var banner = _context.Banners.FirstOrDefault(x => x.Id == id);
            if (banner == null)
            {
                return NotFound();
            }
            return View(banner);
        }



        [HttpPost]
        public IActionResult Edit(Banner banner, IFormFile NewPhoto, string? oldPhoto)
        {
            if (NewPhoto != null)
            {
                string imageName = Guid.NewGuid().ToString() + Path.GetExtension(NewPhoto.FileName);
                //get url
                string savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/admin/img", imageName);
                using (var fs = new FileStream(savePath, FileMode.Create))
                {
                    NewPhoto.CopyTo(fs);
                }
                banner.PhotoURL = "/admin/img/" + imageName;
            }
            else
            {
                banner.PhotoURL = oldPhoto;
            }
            _context.Banners.Update(banner);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
