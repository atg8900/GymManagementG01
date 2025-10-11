using GymManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Interfaces
{
    interface IMemberRepository
    {
        //GetAll
        IEnumerable<Member> GetAllMembers();
        //GetById
        Member? GetMemberById(int id);
        //Add
        int Add(Member member);
        //Update
        int Update(Member member);
        //Delete
        int Remove(int id);
    }
}
