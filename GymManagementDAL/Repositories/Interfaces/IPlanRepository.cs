using GymManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Interfaces
{
    public interface IPlanRepository
    {
        IEnumerable<Plan> GetAllPlans();
        //GetById
        Plan? GetPlanById(int id);
        int Update(Plan plan);
    }
}
