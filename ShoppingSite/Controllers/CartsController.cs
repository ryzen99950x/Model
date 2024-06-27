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
    public class CartsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Carts
        public async Task<IActionResult> Index(int userId)
        {
            
            if (TempData["AddCartProductId"] == null || TempData["AddCartUserId"] == null)
            {
                if (TempData["DeletUserId"] != null)
                {
                    userId = (int)TempData["DeletUserId"];
                }
                var cartItems = await _context.Carts
                    .Where(c => c.UserId == userId)
                    .Include(c => c.Product)
                    .ToListAsync();

                // 商品IDでグループ化し、重複を特定する
                var duplicateGroups = cartItems
                    .GroupBy(c => c.ProductId)
                    .Where(g => g.Count() > 1)
                    .ToList();

                foreach (var group in duplicateGroups)
                {
                    int count = group.Count();
                    for (int i = 0; i < count; i++)
                    {
                        // 最初の商品を除くすべての商品のDupCountを1増やす
                        if (group.ElementAt(i).DupCount == 1)
                        {
                            group.ElementAt(i).DupCount  = count;
                        }
                        else
                        {
                            group.ElementAt(i).DupCount += count-1;
                        }
                    }
                }

                // データベースから重複商品を削除する
                foreach (var group in duplicateGroups)
                {
                    // 各グループの最初の商品以外を削除する
                    foreach (var item in group.Skip(1))
                    {
                        _context.Carts.Remove(item);
                    }
                }
                //変更をデータベースに保存する
                await _context.SaveChangesAsync();
                // 更新されたカート情報をビューに渡す
                return View(cartItems);


            }

            int AddProductId = (int)TempData["AddCartProductId"];
            int AddUserId = (int)TempData["AddCartUserId"];

            // 商品情報をデータベースから取得
            var product = await _context.Products.FindAsync(AddProductId);

            if (product == null)
            {
                return RedirectToAction("Index", "Products");
            }
            var cartItem = new Carts
            {
                UserId = AddUserId,
                ProductId = product.Id,
            };

            _context.Carts.Add(cartItem);
            await _context.SaveChangesAsync();

            // 追加されたユーザーのカート情報を取得
            var cartItemsForUser = await _context.Carts
                .Where(c => c.UserId == AddUserId)
                .Include(c => c.Product)
                .ToListAsync();

            // 商品IDでグループ化し、重複を特定する
            var duplicateGroupsForUser = cartItemsForUser
                .GroupBy(c => c.ProductId)
                .Where(g => g.Count() > 1)
                .ToList();

            foreach (var group in duplicateGroupsForUser)
            {
                int count = group.Count();
                foreach (var item in group)
                {
                    if (item.DupCount == 1)
                    {
                        item.DupCount = count;
                    }
                    else
                    {
                        item.DupCount += count - 1;
                    }
                }
            }
            // データベースから重複商品を削除する
            foreach (var group in duplicateGroupsForUser)
            {
                // 各グループの最初の商品以外を削除する
                foreach (var item in group.Skip(1))
                {
                    _context.Carts.Remove(item);
                }
            }
            // 変更をデータベースに保存する
            await _context.SaveChangesAsync();
            var cartItemsForUserafter = await _context.Carts
                .Where(c => c.UserId == AddUserId)
                .Include(c => c.Product)
                .ToListAsync();
            return View(cartItemsForUserafter);
        }
        // POST: Products/OrderDetail/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OrderDetail(int userId, int id)
        {
            var cartItems = await _context.Carts
                    .Where(c => c.UserId == userId)
                    .Include(c => c.Product)
                    .ToListAsync();
            await _context.SaveChangesAsync();
            if (cartItems != null && cartItems.Any())
            {
                TempData["userIdOfOrder"] = userId;
                return RedirectToAction("OrderFix", "Carts");
            }
            else
            {
                return Redirect("~/home/index");
            }
        }

        public async Task<IActionResult> OrderFix(int Id)
        {

            if (ModelState.IsValid)
            {
                if (TempData["userIdOfOrder"] != null)
                {
                    int userId = (int)TempData["userIdOfOrder"];
                    List<Carts> orderFixed = new List<Carts>();
                    var cartItems = await _context.Carts
                            .Where(c => c.UserId == userId)
                            .Include(c => c.User)
                            .Include(c => c.User.Credits)
                            .Include(c => c.Product)
                            .ToListAsync();
                    return View(cartItems);
                }
  
            }
            return Redirect("~/home/index");
            
        }
        // POST: Products/Complete/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Complete(int userId, int id)
        {
            var cartItems = await _context.Carts
                .Include(c => c.Product) 
                .Where(c => c.UserId == userId)
                .ToListAsync();

            foreach (var cartItem in cartItems)
            {
                cartItem.Product.Stock -= cartItem.DupCount;
                cartItem.Product.Sales += cartItem.DupCount;
            }
            _context.Carts.RemoveRange(cartItems);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
        // GET: Carts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carts = await _context.Carts
                .Include(c => c.Product)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carts == null)
            {
                return NotFound();
            }

            return View(carts);
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carts = await _context.Carts.FindAsync(id);
            if (carts != null)
            {
                _context.Carts.Remove(carts);
            }
            TempData["DeletUserId"] = carts.UserId;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
