using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Temple.Models;

namespace Temple.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        static List<Product> products = new List<Product>();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        
        public IActionResult ShowAll()
        {
            return View("ShowAll", products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind("Id", "Name", "Price")] Product product)
        {
            //thêm vào danh sách
            products.Add(product);
            //gọi hiển thị danh sách
            return RedirectToAction("ShowAll");
        }

        public IActionResult Edit(int id)
        {
            Product p = products.SingleOrDefault(p => p.Id == id);
            if (p != null) //tìm thấy
            {
                return View(p);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Edit(int id, [Bind("Id", "Name", "Price")] Product product)
        {
            //Sửa vào danh sách
            Product p = products.SingleOrDefault(p => p.Id == id);
            if (p != null) //tìm thấy
            {
                p.Name = product.Name;
                p.Price = product.Price;
            }
            //gọi hiển thị danh sách
            return RedirectToAction("ShowAll");
        }

        public IActionResult Delete(int id)
        {
            //tìm Product cần xóa (dùng LINQ)
            Product p = products.SingleOrDefault(p => p.Id == id);
            if (p != null) //tìm thấy
            {
                products.Remove(p);
            }
            //gọi hiển thị danh sách
            return RedirectToAction("ShowAll");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}