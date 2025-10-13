using GymManagementBLL.BusinessServices.interfaces;
using GymManagementBLL.View_Models;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Implementaion;
using GymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.BusinessServices.implementaion
{
    class MemberService : IMemberService
    {
        private readonly IGenericRepository<Member> _memberRepository;

        public MemberService(IGenericRepository<Member> memberRepository)
        {
            this._memberRepository = memberRepository;
        }

        public bool CreateMember(CreateMemberViewModel createMember)
        {
            var EmailExit = _memberRepository.GetAll(E=>E.Email == createMember.Email).Any();
            var PhoneExit = _memberRepository.GetAll(P=>P.Phone == createMember.Phone).Any();


            if (EmailExit || PhoneExit)
                return false;

            var member = new Member()
            {
                Name = createMember.Name,
                Email = createMember.Email,
                Phone = createMember.Phone,
                Gender = createMember.Gender,
                DateOfBirth = createMember.DateOfBirth,
                Address = new Address() 
                {
                   BuildingNumber =createMember.BuildingNumber,
                   City =createMember.City,
                   Street =createMember.Street,
                } ,
                HealthRecord = new HealthRecord()
                {
                    Height= createMember.HealthRecord.Height,
                    Weight= createMember.HealthRecord.Weight,
                    BloodType= createMember.HealthRecord.BloodType,
                    Note= createMember.HealthRecord.Note,

                }

            };

            return _memberRepository.Add(member)>0;

        }

        public IEnumerable<MemberViewModel> GetAllMembers()
        {
            var members = _memberRepository.GetAll();
            if (members is null || !members.Any()) return [];

            #region Manula Mapping First Way

            //var listOfMembersViewModel = new List<MemberViewModel>();
            //foreach (var member in members)
            //{
            //    var memberViewModel = new MemberViewModel()
            //    {
            //        Id = member.Id,
            //        Name = member.Name,
            //        Phone = member.Phone,
            //        Photo = member.Photo,
            //        Email = member.Email,
            //        Gender = member.Gender.ToString()
            //    }
            //    ;
            //    listOfMembersViewModel.Add(memberViewModel);
            //}
            //return listOfMembersViewModel; 

            #endregion

            #region Manual Mapping Second Way 

            var membersViewModel = members.Select(M=> new MemberViewModel 
            {
                Id = M.Id,
                Name = M.Name,
                Phone = M.Phone,
                Photo = M.Photo,
                Email = M.Email,
                Gender = M.Gender.ToString()

            });
            return membersViewModel;
            #endregion
        }


    }
}
