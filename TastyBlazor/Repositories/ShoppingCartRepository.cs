using Microsoft.EntityFrameworkCore;
using TastyBlazor.Data;
using TastyBlazor.Repositories.IRepositories;

namespace TastyBlazor.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {

        protected readonly ApplicationDbContext _db;

        public ShoppingCartRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> ClearAsync(string userId)
        {
            var CartItems = await _db.ShoppingCart.Where(u => u.UserId == userId).ToListAsync();
            _db.ShoppingCart.RemoveRange(CartItems);
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<ShoppingCart>> GetAllAsync(string userId)
        {
            var CartItems = await _db.ShoppingCart.Where(u => u.UserId == userId).Include(u => u.Product).ToListAsync();
            return CartItems;
        }

        public async Task<bool> UpdateCartAsync(string userId, int productId, int UpdateBy)
        {
            var cart = await _db.ShoppingCart.FirstOrDefaultAsync(u => u.UserId == userId && u.ProductId == productId);
            if(cart == null)
            {
                cart = new ShoppingCart
                {
                    UserId = userId,
                    ProductId = productId,
                    Count = UpdateBy
                };
               await  _db.ShoppingCart.AddAsync(cart);

            }
            else
            {
                cart.Count += UpdateBy;
                if(cart.Count <= 0)
                {
                    _db.ShoppingCart.Remove(cart);
                }
            }
            return await _db.SaveChangesAsync() > 0;

        }
    }
}
