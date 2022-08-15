using Heroes.Data;
using Heroes.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Heroes.Areas.Dashboard.Controllers
{
    [Area("dashboard")]
    [Authorize]
    public class ServiceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ServiceController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var service = _context.Services.ToList();
            return View(service);
        }

        public IActionResult Create()
        {
            var service = _context.Services.ToList();
            if (service == null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }


        [HttpPost]
        public IActionResult Create(Service service, IFormFile NewPhoto)
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
            service.PhotoURL = "/admin/img/" + MyPhoto;
            _context.Services.Add(service);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var service = _context.Services.FirstOrDefault(x => x.Id == id);
            if (service == null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Delete(Service service)
        {
            if (service == null)
            {
                return RedirectToAction("Index");
            }
            _context.Services.Remove(service);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }

        public IActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = _context.Services.FirstOrDefault(x => x.Id == id);
            if (service == null)
            {
                return NotFound();
            }
            return View(service);
        }

        [HttpPost]
        public IActionResult Edit(Service service, IFormFile NewPhoto, string? oldPhoto)
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
                service.PhotoURL = "/admin/img/" + imageName;
            }
            else
            {
                service.PhotoURL = oldPhoto;
            }
            _context.Services.Update(service);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
