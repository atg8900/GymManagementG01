using GymManagementBLL.BusinessServices.interfaces;
using GymManagementBLL.View_Models.Plan_VM;
using GymManagementDAL.Entities;
using GymManagementDAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.BusinessServices.implementaion
{
    public class PlanService : IPlanService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlanService( IUnitOfWork unitOfWork)
        {
           _unitOfWork = unitOfWork;
        }
        public IEnumerable<PlanViewModel> GetAllPlans()
        {
            var plan = _unitOfWork.GetRepository<Plan>().GetAll();
            if (plan is null || !plan.Any()) return [];

            return plan.Select(P=> new PlanViewModel()
            {
                Id=P.Id,
                Name= P.Name,
                Description = P.Description , 
                DurationDays = P.DurationDays,
                Price = P.Price,
                IsActive = P.IsActive
            });
        }

        public PlanViewModel? GetPlanDetails(int PlanId)
        {
            var plan = _unitOfWork.GetRepository<Plan>().GetById(PlanId);
            if (plan is null ) return null;

            return  new PlanViewModel()
            {
                Id = plan.Id,
                Name = plan.Name,
                Description = plan.Description,
                DurationDays = plan.DurationDays,
                Price = plan.Price,
                IsActive = plan.IsActive
            };
        }

        public PlanToUpdateViewModel? GetPlanToUpdate(int PlanId)
        {
            throw new NotImplementedException();
        }

        public bool UpdatePlan(int PlanId, PlanToUpdateViewModel planToUpdate)
        {
            throw new NotImplementedException();
        }

        bool IPlanService.TogggleStatus(int PlanId)
        {
            throw new NotImplementedException();
        }
    }
}
