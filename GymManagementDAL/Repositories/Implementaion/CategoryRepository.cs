using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Implementaion
{
    class CategoryRepository : ICategoryRepository
    {
        private readonly GymDbContext _dbContext = new GymDbContext();
        public int Add(Category category)
        {
            _dbContext.Categories.Add(category);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<Category> GetAllCategories() => _dbContext.Categories.ToList();
       
        public Category? GetCategoryById(int id) => _dbContext.Categories.Find(id);
       

        public int Remove(int id)
        {
            var category = _dbContext.Categories.Find(id);
            if (category is null) return 0;

            _dbContext.Categories.Remove(category);
            return _dbContext.SaveChanges();
        }

        public int Update(Category category)
        {
            _dbContext.Categories.Update(category);
            return _dbContext.SaveChanges();
        }
    }
}
