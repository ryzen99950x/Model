﻿using System;
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
            IQueryable<Products> products = _context.Products.Include(p => p.Category); // Include を使用して関連するカテゴリも読み込む
            var resultName = TempData["SearchModel"] as string;
            if (!string.IsNullOrEmpty(resultName))
            {
                products = products.Where(p => p.Name.Contains(resultName));
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

            var products = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Gender)
                .Include(p => p.Review)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id");
            ViewData["GenderId"] = new SelectList(_context.Genders, "Id", "Id");
            ViewData["ReviewId"] = new SelectList(_context.Reviews, "Id", "Id");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,Size,Image,Name,Price,Weight,Material,GenderId,Stock,Limited,Package,CategoryId,ReviewId,Sales")] Products products)
        {
            if (ModelState.IsValid)
            {
                _context.Add(products);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", products.CategoryId);
            ViewData["GenderId"] = new SelectList(_context.Genders, "Id", "Id", products.GenderId);
            ViewData["ReviewId"] = new SelectList(_context.Reviews, "Id", "Id", products.ReviewId);
            return View(products);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products.FindAsync(id);
            if (products == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", products.CategoryId);
            ViewData["GenderId"] = new SelectList(_context.Genders, "Id", "Id", products.GenderId);
            ViewData["ReviewId"] = new SelectList(_context.Reviews, "Id", "Id", products.ReviewId);
            return View(products);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,Size,Image,Name,Price,Weight,Material,GenderId,Stock,Limited,Package,CategoryId,ReviewId,Sales")] Products products)
        {
            if (id != products.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(products);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductsExists(products.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", products.CategoryId);
            ViewData["GenderId"] = new SelectList(_context.Genders, "Id", "Id", products.GenderId);
            ViewData["ReviewId"] = new SelectList(_context.Reviews, "Id", "Id", products.ReviewId);
            return View(products);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Gender)
                .Include(p => p.Review)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var products = await _context.Products.FindAsync(id);
            if (products != null)
            {
                _context.Products.Remove(products);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductsExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}