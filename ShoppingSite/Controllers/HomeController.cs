// ProductsController.cs

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ShoppingSite.Data;
using ShoppingSite.Models;
using ShoppingSite.ViewModels;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.ToListAsync();
            var viewModel = new ProductSearchViewModel
            {
                Results = products
            };
            foreach (var product in viewModel.Results)
            {
                product.Category = await _context.Categories.FindAsync(product.CategoryId);
            }
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(ProductSearchViewModel searchModel)
        {
            IQueryable<Products> products = _context.Products;
            var num = searchModel.GenderId;
            if (num!= 0) {
                TempData["SearchModel"] = searchModel.GenderId + 1000000000;
                    products = products.Where(p => p.GenderId == searchModel.GenderId || p.GenderId == 3);

                searchModel.Results = await products.ToListAsync();

                foreach (var product in searchModel.Results)
                {
                    product.Category = await _context.Categories.FindAsync(product.CategoryId);
                }
                return RedirectToAction("Index", "Products");
            }
            TempData["SearchModel"] = searchModel.Name;
            if (!string.IsNullOrEmpty(searchModel.Name))
            {
                products = products.Where(p => p.Name.Contains(searchModel.Name));
            }

            searchModel.Results = await products.ToListAsync();

            foreach (var product in searchModel.Results)
            {
                product.Category = await _context.Categories.FindAsync(product.CategoryId);
            }
            return RedirectToAction("Index", "Products");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
