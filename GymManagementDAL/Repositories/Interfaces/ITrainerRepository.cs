using GymManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Interfaces
{
    interface ITrainerRepository
    {
        //GetAll
        IEnumerable<Trainer> GetAllTrainers();
        //GetById
        Trainer? GetTrainerById(int id);
        //Add
        int Add(Trainer trainer);
        //Update
        int Update(Trainer trainer);
        //Delete
        int Remove(int id);
    }
}
