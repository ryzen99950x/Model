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
            IQueryable<Categories> categories = _context.Categories;
            if (searchModel.GenderName == "Mens" || searchModel.GenderName == "Womens") {
                searchModel.Results = await products.ToListAsync();
                TempData["SearchModel"] = searchModel.GenderName;
                return RedirectToAction("Index", "Products");
            }
            else if (searchModel.CategoryName != null)
            {
                categories = categories.Where(c => c.CategoryName.Contains(searchModel.CategoryName));

                searchModel.Results = await products.ToListAsync();
                TempData["SearchModel"] = searchModel.CategoryName;
                return RedirectToAction("Index", "Products");

            }
            else
            {
                TempData["SearchModel"] = searchModel.Name;
                searchModel.Results = await products.ToListAsync();
                return RedirectToAction("Index", "Products");
            }
            
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
