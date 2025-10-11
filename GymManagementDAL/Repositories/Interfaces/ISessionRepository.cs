using GymManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace GymManagementDAL.Repositories.Interfaces
{
    interface ISessionRepository
    {
        //GetAll
        IEnumerable<Session> GetAllSessions();
        //GetById
        Session? GetSessionById(int id);
        //Add
        int Add(Session session);
        //Update
        int Update(Session session);
        //Delete
        int Remove(int id);
    }
}
