using Heroes.Data;
using Heroes.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Heroes.Areas.Dashboard.Controllers
{
    [Area("dashboard")]
    [Authorize]
    public class PeopleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PeopleController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
             var people = _context.Peoples.ToList();
            return View(people);
        }

        public IActionResult Create()
        {
            var people = _context.Peoples.ToList();
            if (people == null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }



        [HttpPost]
        public IActionResult Create(People people, IFormFile NewPhoto)
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
            people.PhotoURL = "/admin/img/" + MyPhoto;
            _context.Peoples.Add(people);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {

            var people = _context.Banners.FirstOrDefault(x => x.Id == id);
            if (people == null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]

        public IActionResult Delete(People people)
        {
            if (people == null)
            {
                return RedirectToAction("Index");
            }
            _context.Peoples.Remove(people);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var people = _context.Peoples.FirstOrDefault(x => x.Id == id);
            if (people == null)
            {
                return NotFound();
            }
            return View(people);
        }



        [HttpPost]
        public IActionResult Edit(People people, IFormFile NewPhoto, string? oldPhoto)
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
                people.PhotoURL = "/admin/img/" + imageName;
            }
            else
            {
                people.PhotoURL = oldPhoto;
            }
            _context.Peoples.Update(people);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
