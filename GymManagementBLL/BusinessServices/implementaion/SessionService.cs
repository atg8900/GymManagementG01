using AutoMapper;
using GymManagementBLL.BusinessServices.interfaces;
using GymManagementBLL.View_Models.Session_VM;
using GymManagementDAL.Entities;
using GymManagementDAL.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace GymManagementBLL.BusinessServices.implementaion
{
    public class SessionService : ISessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SessionService(IUnitOfWork unitOfWork ,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public IEnumerable<SessionViewModel> GetAllSessions()
        {
            var sessionRepo = _unitOfWork.SessionRepository;
            var sessions = sessionRepo.GetAllWithCategoryAndTrainer();
            if (!sessions.Any())
                return [];
            #region Manula mapping
            //return sessions.Select(S => new SessionViewModel()
            //{
            //    Id = S.Id,
            //    Description = S.Description,
            //    StartDate = S.StartDate,
            //    EndDate = S.EndDate,
            //    Capacity = S.Capacity,
            //    TrainerName = S.Trainer.Name,
            //    CategoryName = S.Category.CategoryName,
            //    AvailableSlots = S.Capacity - sessionRepo.GetCountOfBookedSlots(S.Id)
            //}); 
            #endregion

            var mappedSessions = _mapper.Map<IEnumerable< Session>,IEnumerable< SessionViewModel>>(sessions);
            foreach (var session in mappedSessions)
            {
                session.AvailableSlots = session.Capacity - sessionRepo.GetCountOfBookedSlots(session.Id);
                
            }
            return mappedSessions;
        }

        public SessionViewModel? GetSessionDetails(int sessionId)
        {
            var sessionRepo = _unitOfWork.SessionRepository;
            var session = sessionRepo.GetByIdWithTrainerAndCategory(sessionId);
            if (session is null)
                return null;
            #region manual mapping

            //return new SessionViewModel()
            //{
            //    Id = session.Id,
            //    Description = session.Description,
            //    StartDate = session.StartDate,
            //    EndDate = session.EndDate,
            //    Capacity = session.Capacity,
            //    TrainerName = session.Trainer.Name,
            //    CategoryName = session.Category.CategoryName,
            //    AvailableSlots = session.Capacity - sessionRepo.GetCountOfBookedSlots(session.Id)
            //};

            #endregion

            #region Auto mapper

            var mappedSession = _mapper.Map<Session,SessionViewModel>(session);
            mappedSession.AvailableSlots = session.Capacity - sessionRepo.GetCountOfBookedSlots(session.Id);
            return mappedSession;

            #endregion
        }
    }
}
