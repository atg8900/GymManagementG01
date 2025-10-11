using GymManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Interfaces
{
    interface ICategoryRepository
    {
        //GetAll
        IEnumerable<Category> GetAllCategories();
        //GetById
        Category? GetCategoryById(int id);
        //Add
        int Add(Category category);
        //Update
        int Update(Category category);
        //Delete
        int Remove(int id);
    }
}
