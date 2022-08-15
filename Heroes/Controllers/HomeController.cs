using Heroes.Data;
using Heroes.Models;
using Heroes.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Heroes.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var banner = _context.Banners.FirstOrDefault();
            var  responsiv = _context.Responsives.ToList();
            var service = _context.Services.ToList();
            var people = _context.Peoples.ToList();
            var signUp = _context.SignUps.FirstOrDefault();
           

            HomeVM vm = new()
            {
                Banner = banner,
                Responsiv = responsiv,
                Services = service,
                Peoples = people,
                SignUp = signUp,
               

            };


          

            return View(vm);
        }


        [HttpPost]
        public async Task<IActionResult> Contact(Email email)
        {
            _context.Emails.Add(email);
            await _context.SaveChangesAsync();

            return Ok();
        }


    }
}
