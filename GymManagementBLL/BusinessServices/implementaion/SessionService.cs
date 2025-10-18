using GymManagementBLL.BusinessServices.interfaces;
using GymManagementBLL.View_Models.Session_VM;
using GymManagementDAL.Entities;
using GymManagementDAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.BusinessServices.implementaion
{
    public class SessionService : ISessionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SessionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<SessionViewModel> GetAllSessions()
        {
            var sessionRepo = _unitOfWork.SessionRepository;
            var sessions = sessionRepo.GetAllWithCategoryAndTrainer();
            if (!sessions.Any())
                return [];
            return sessions.Select(S => new SessionViewModel()
            {
                Id = S.Id,
                Description = S.Description,
                StartDate = S.StartDate,
                EndDate = S.EndDate,
                TrainerName = S.Trainer.Name,
                CategoryName = S.Category.CategoryName,
                AvailableSlots = S.Capacity - sessionRepo.GetCountOfBookedSlots(S.Id)
            });
        }
    }
}
