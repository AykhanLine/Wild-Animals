using Heroes.Data;
using Heroes.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Heroes.Areas.Dashboard.Controllers
{
    [Area("dashboard")]
    [Authorize]
    public class SignUpController : Controller
    {
        public readonly ApplicationDbContext _context;

        public SignUpController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var signup = _context.SignUps.FirstOrDefault();
            return View(signup);
        }

        public IActionResult Create()
        {
            var signup = _context.SignUps.FirstOrDefault();

            if (signup != null)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost]

        public IActionResult Create(SignUp signup)
        {
            if (signup == null)
            {
                return RedirectToAction("Index");
            }

            _context.SignUps.Add(signup);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {


            var signup = _context.SignUps.FirstOrDefault(x => x.Id == id);
            if (signup == null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]

        public IActionResult Delete(SignUp signup)
        {
            if (signup == null)
            {
                return RedirectToAction("Index");
            }
            _context.SignUps.Remove(signup);
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
            var signup = _context.SignUps.FirstOrDefault(x => x.Id == id);
            if (signup == null)
            {
                return NotFound();
            }
            return View(signup);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var signup = _context.SignUps.FirstOrDefault(x => x.Id == id);
            if (signup == null)
            {
                return NotFound();
            }
            return View(signup);
        }

        [HttpPost]

        public IActionResult Edit(SignUp signup)
        {
            if (signup == null)
            {
                return RedirectToAction("Index");
            }
            _context.SignUps.Update(signup);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
