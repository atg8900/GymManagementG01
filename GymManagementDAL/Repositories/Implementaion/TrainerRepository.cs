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
    class TrainerRepository : ITrainerRepository
    {
        private readonly GymDbContext _dbContext;

        // private readonly GymDbContext _dbContext = new GymDbContext();

        // Ask CLR to inject object from GymDb Context in the run time
        public TrainerRepository(GymDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public int Add(Trainer trainer)
        {
            _dbContext.Trainers.Add(trainer);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<Trainer> GetAllTrainers()
        => _dbContext.Trainers.ToList();

        public Trainer? GetTrainerById(int id)
        => _dbContext.Trainers.Find(id);

        public int Remove(int id)
        {
            var trainer = _dbContext.Trainers.Find(id);
            if (trainer is null) return 0;

            _dbContext.Trainers.Remove(trainer);
            return _dbContext.SaveChanges();
        }

        public int Update(Trainer trainer)
        {
            _dbContext.Trainers.Update(trainer);
            return _dbContext.SaveChanges();
        }
    }
}
