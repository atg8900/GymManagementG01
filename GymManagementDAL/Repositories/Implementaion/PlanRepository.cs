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
    class PlanRepository: IPlanRepository
    {

        private readonly GymDbContext _dbContext;

        // private readonly GymDbContext _dbContext = new GymDbContext();

        // Ask CLR to inject object from GymDb Context in the run time
        public PlanRepository(GymDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public IEnumerable<Plan> GetAllPlans()
        => _dbContext.Plans.ToList();

        public Plan? GetPlanById(int id)=>
       _dbContext.Plans.Find(id);

        public int Update(Plan plan)
        {
            _dbContext.Plans.Update(plan);
            return _dbContext.SaveChanges();
        }
    }
}
