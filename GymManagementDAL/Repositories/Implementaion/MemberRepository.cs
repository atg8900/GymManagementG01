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
    class MemberRepository : IMemberRepository
    {
        private readonly GymDbContext _dbContext = new GymDbContext();
        public int Add(Member member)
        {
           _dbContext.Members.Add(member);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<Member> GetAllMembers() => _dbContext.Members.ToList();


        public Member? GetMemberById(int id) => _dbContext.Members.Find(id);



        public int Remove(int id)
        {
            var member = _dbContext.Members.Find(id);
            if (member is null) return 0;

            _dbContext.Members.Remove(member);
            return _dbContext.SaveChanges();
        }

        public int Update(Member member)
        {
            _dbContext.Members.Update(member);
            return _dbContext.SaveChanges();
        }
    }
}
