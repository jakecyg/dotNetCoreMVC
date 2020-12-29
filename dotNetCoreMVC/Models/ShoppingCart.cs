using dotNetCoreMVC.Common.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotNetCoreMVC.Models
{
    public class ShoppingCart
    {
        private readonly dbContext _db;
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
        public ShoppingCart(dbContext db) => _db = db;
        
        // Inject services provided by IServiceProvider
        public static ShoppingCart GetCart(IServiceProvider services)
        {
            // Bring access to sessions(by default you dont have access to session in a regular class(you do in controllers))
            ISession session = services.GetRequiredService<HttpContextAccessor>()?.HttpContext.Session;
            // Bring access to dbContext
            var context = services.GetService<dbContext>();
            // Look for session key called CartId, if null then create new guid and assign
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            // Create session
            session.SetString("CartId", cartId);
            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        public void AddToCart(Pie pie, int amount)
        {
            // Check if pie to add already exists in db & session
            var shoppingCartItem = _db.ShoppingCartItems.SingleOrDefault(x => x.Pie.Id == pie.Id && x.ShoppingCartId == ShoppingCartId);
            // If not = create new
            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Pie = pie,
                    Amount = amount
                };
                _db.ShoppingCartItems.Add(shoppingCartItem);
                _db.SaveChanges();
            }
            // If already exists = add
            else
            {
                shoppingCartItem.Amount++;
                _db.SaveChanges();
            }

        }
        public int RemoveFromCart(Pie pie)
        {
            var shoppingCartItem = _db.ShoppingCartItems
                                      .SingleOrDefault( x => x.Pie.Id == pie.Id && x.ShoppingCartId == ShoppingCartId);

            var localAmount = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                    _db.SaveChanges();
                }
                else
                {
                    _db.ShoppingCartItems.Remove(shoppingCartItem);
                    _db.SaveChanges();
                }
            }
            return localAmount;
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ?? (ShoppingCartItems = _db.ShoppingCartItems
                                                                .Where(c => c.ShoppingCartId == ShoppingCartId)
                                                                .Include(s => s.Pie)
                                                                .ToList());
        }
        public void ClearCart()
        {
            var cartItems = _db.ShoppingCartItems.Where(x => x.ShoppingCartId == ShoppingCartId);
            _db.ShoppingCartItems.RemoveRange(cartItems);
            _db.SaveChanges();
        }

        public decimal GetShoppingCartTotal()
        {
            var total = _db.ShoppingCartItems
                           .Where(x => x.ShoppingCartId == ShoppingCartId)
                           .Select(x => x.Pie.Price * x.Amount)
                           .Sum();
            return total;
        }
    }
}
