using Microsoft.EntityFrameworkCore;
using TastyBlazor.Data;
using TastyBlazor.Repositories.IRepositories;

namespace TastyBlazor.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<Category> CreateAsync(Category obj)
        {
            _db.Category.Add(obj);
          await  _db.SaveChangesAsync();
            return obj;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var obj =  await _db.Category.FirstOrDefaultAsync(u => u.Id == id);
                if(obj is not null)
            {
                _db.Category.Remove(obj);
                return await(_db.SaveChangesAsync()) > 0;
            }
            return false;
        }

        public async Task<Category> GetAsync(int id)
        {
            var obj = await _db.Category.FirstOrDefaultAsync(u => u.Id == id);
            if(obj is not null)
            {
                return obj;
            }
            return new Category();
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
           return await _db.Category.ToListAsync();

        }

        public async Task<Category> UpdateAsync(Category obj)
        {
            var objfromdatabase = await _db.Category.FirstOrDefaultAsync(u => u.Id == obj.Id);
            if(objfromdatabase is not null)
            {
                objfromdatabase.Name = obj.Name;
                _db.Category.Update(objfromdatabase);
               await _db.SaveChangesAsync();
                return objfromdatabase;
            }
            return obj;
        }
    }
}
