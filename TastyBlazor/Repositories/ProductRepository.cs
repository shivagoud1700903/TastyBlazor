using Microsoft.EntityFrameworkCore;
using TastyBlazor.Data;
using TastyBlazor.Repositories.IRepositories;

namespace TastyBlazor.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProductRepository(ApplicationDbContext db, IWebHostEnvironment _webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<Product> CreateAsync(Product obj)
        {
            _db.Product.Add(obj);
          await  _db.SaveChangesAsync();
            return obj;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var obj =  await _db.Product.FirstOrDefaultAsync(u => u.Id == id);
            var imagepath = Path.Combine(webHostEnvironment.WebRootPath, obj.ImageUrl.Trim('/'));
            if (File.Exists(imagepath))
            {
                File.Delete(imagepath);
            }
                if(obj is not null)
            {
                _db.Product.Remove(obj);
                return await(_db.SaveChangesAsync()) > 0;
            }
            return false;
        }

        public async Task<Product> GetAsync(int id)
        {
            var obj = await _db.Product.FirstOrDefaultAsync(u => u.Id == id);
            if(obj is not null)
            {
                return obj;
            }
            return new Product();
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
           return await _db.Product.Include(p => p.Category).ToListAsync();

        }

        public async Task<Product> UpdateAsync(Product obj)
        {
            var objfromdatabase = await _db.Product.FirstOrDefaultAsync(u => u.Id == obj.Id);
            if(objfromdatabase is not null)
            {
                objfromdatabase.Name = obj.Name;
                objfromdatabase.Price = obj.Price;
                objfromdatabase.Description = obj.Description;
                objfromdatabase.SpecialTag = obj.SpecialTag;
                objfromdatabase.CategoryId = obj.CategoryId;
                objfromdatabase.Category = obj.Category;
                objfromdatabase.ImageUrl = obj.ImageUrl;
                _db.Product.Update(objfromdatabase);
               await _db.SaveChangesAsync();
                return objfromdatabase;
            }
            return obj;
        }
    }
}
