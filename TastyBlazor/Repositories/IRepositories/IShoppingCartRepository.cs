using TastyBlazor.Data;

namespace TastyBlazor.Repositories.IRepositories
{
    public interface IShoppingCartRepository
    {

        public Task<bool> UpdateCartAsync(string userId, int productid, int UpdateBy);
        public Task<IEnumerable<ShoppingCart>> GetAllAsync(string userId);

        public Task<bool> ClearAsync(string userId);
    }
}
