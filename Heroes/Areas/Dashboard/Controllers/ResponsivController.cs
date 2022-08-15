using Heroes.Data;
using Heroes.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Heroes.Areas.Dashboard.Controllers
{
    [Area("dashboard")]
    [Authorize]
    public class ResponsivController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ResponsivController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var responsiv = _context.Responsives.ToList();
            return View(responsiv);
        }

        public IActionResult Create()
        {
            var responsiv = _context.Responsives.ToList();
            if (responsiv == null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]

        public IActionResult Create(Responsiv responsiv)
        {
            if (responsiv == null)
            {
                return RedirectToAction("Index");
            }

            _context.Responsives.Add(responsiv);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var responsiv = _context.Responsives.FirstOrDefault(x => x.Id == id);
            if (responsiv == null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Delete(Responsiv responsiv)
        {
            if (responsiv == null)
            {
                return RedirectToAction("Index");
            }
            _context.Responsives.Remove(responsiv);
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
            var responsiv = _context.Responsives.FirstOrDefault(x => x.Id == id);
            if (responsiv == null)
            {
                return NotFound();
            }
            return View(responsiv);
        }

        public IActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var responsiv = _context.Responsives.FirstOrDefault(x => x.Id == id);
            if (responsiv == null)
            {
                return NotFound();
            }
            return View(responsiv);
        }

        [HttpPost]

        public IActionResult Edit(Responsiv responsiv)
        {
            if (responsiv == null)
            {
                return RedirectToAction("Index");
            }
            _context.Responsives.Update(responsiv);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
