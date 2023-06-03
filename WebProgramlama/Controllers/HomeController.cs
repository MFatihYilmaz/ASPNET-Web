using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebProgramlama.Models;
using System.Text.Json;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using WebProgramlama.Data;
using Microsoft.EntityFrameworkCore;

namespace WebProgramlama.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public List<User> GetAllUsers()
        {
            return _db.Users.ToList();
        }

        public HomeController(ApplicationDbContext db,ILogger<HomeController> logger)
        {
            _db = db;
            _logger = logger;
        }

        public IActionResult Index()
        {
            
            return View();
        }

     
        public async Task<IActionResult> FinanceApi(){
            
             using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://cdn.jsdelivr.net/gh/fawazahmed0/currency-api@1/latest/");
                
                HttpResponseMessage resp=await client.GetAsync("https://cdn.jsdelivr.net/gh/fawazahmed0/currency-api@1/latest/currencies.json");
                if (resp.IsSuccessStatusCode)
                {
                    var jsonString = await resp.Content.ReadAsStringAsync();
                     var currencyData = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonString);
    
                    List<string> symbols = currencyData.Keys.ToList();

                    ViewBag.List=symbols;
                    
                }
        
            }
             
        return View();
        }

       

         public IActionResult Login()
         {
             List<User> users = GetAllUsers();
             
             return View(users);
         }

         [HttpPost]
         public IActionResult Login(User user)
         {
             var controlUser = _db.Users.Where(x => x.Mail == user.Mail && x.Password == user.Password).Count();
             if (controlUser>0)
             {
                return RedirectToAction(nameof(Index));
             }

             return View();
        }

         public IActionResult Register()
         {
             return View();
         }
        [HttpPost]
         public async Task<IActionResult> Register(User user)
         {
             string name = user.Name;
             string surname = user.Surname;
             string email = user.Mail;
             string password=user.Password;
             string confirm=user.Confirm;
             if (confirm == password)
             {
                 await _db.Users.AddAsync(user);
                 await _db.SaveChangesAsync();
                 return RedirectToAction(nameof(Index));
            }
             else
             {
                 Console.WriteLine("Error Ocuured");
             }

             return View();
         }


      
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}