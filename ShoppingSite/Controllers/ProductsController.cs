using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShoppingSite.Data;
using ShoppingSite.Models;
using ShoppingSite.ViewModels;

namespace ShoppingSite.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index(ProductSearchViewModel searchModel)
        {
            IQueryable<Products> products = _context.Products.Include(p => p.Category);
            IQueryable<Categories> categories = _context.Categories;
            var searchResult = TempData["SearchModel"] as string;

            if (searchResult == "Mens" || searchResult == "Womens")
            {
                if (searchResult == "Mens")
                {
                    searchModel.GenderId = 1;
                }
                else
                {
                    searchModel.GenderId = 2;
                }
                    products = products.Where(p => p.GenderId == searchModel.GenderId || p.GenderId == 3);
            }
            else if (categories.Any(c => c.CategoryName.Contains(searchResult)))
            {
                categories = categories.Where(c => c.CategoryName.Contains(searchResult));
                var categoryIds = await categories.Select(c => c.Id).ToListAsync();
                products = products.Where(p => categoryIds.Contains(p.CategoryId));
            }
            else if (!string.IsNullOrEmpty(searchResult))
            {
                products = products.Where(p => p.Name.Contains(searchResult));
            }

            var searchResults = await products.ToListAsync();

            foreach (var product in searchResults)
            {
                if (product.Category == null)
                {
                    product.Category = await _context.Categories.FindAsync(product.CategoryId);
                }
            }

            var viewModelList = searchResults.Select(p => new ProductSearchViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Image = p.Image,
                CategoryId = p.CategoryId,
                Category = p.Category,
            }).ToList();

            return View(viewModelList);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Gender)
                .Include(p => p.Review)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        private bool ProductsExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }

        // POST: Products/AddCart/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCart(int id, [Bind("Id,Description,Size,Image,Name,Price,Weight,Material,GenderId,Stock,Limited,Package,CategoryId,ReviewId,Sales")] Products products,int userId)
        {
            if (id != products.Id)
            {

                return NotFound();
            }

            if (ModelState.IsValid)
            {
                TempData["AddCartProductId"] = products.Id;
                TempData["AddCartUserId"] = userId;
                return RedirectToAction("Index", "Carts");
            }
            else
            {
                return Redirect("~/Identity/Account/Login");
            }
        }
    }
}
