using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace GymManagementDAL.Repositories.Implementaion
{
    class SessionRepository: ISessionRepository
    {
        private readonly GymDbContext _dbContext = new GymDbContext();

        public int Add(Session session)
        {
            _dbContext.Sessions.Add(session);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<Session> GetAllSessions()
       => _dbContext.Sessions.ToList();

        public Session? GetSessionById(int id)
        => _dbContext.Sessions.Find(id);

        public int Remove(int id)
        {
            var member = _dbContext.Sessions.Find(id);
            if (member is null) return 0;

            _dbContext.Sessions.Remove(member);
            return _dbContext.SaveChanges();
        }

        public int Update(Session session)
        {
            _dbContext.Sessions.Update(session);
            return _dbContext.SaveChanges();
        }
    }
}
