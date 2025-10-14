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
        private readonly IGenericRepository<Membership> _membershipRepository;
        private readonly IPlanRepository _planRepository;
        private readonly IGenericRepository<HealthRecord> _healthRecordRepository;

        public MemberService(IGenericRepository<Member> memberRepository ,
            IGenericRepository<Membership> membershipRepository ,
             IPlanRepository planRepository,
              IGenericRepository<HealthRecord> healthRecordRepository)
        {
            _memberRepository = memberRepository;
            _membershipRepository = membershipRepository;
            _planRepository = planRepository;
            _healthRecordRepository = healthRecordRepository;
        }

        public bool CreateMember(CreateMemberViewModel createMember)
        {


            if (IsEmailExit(createMember.Email) || IsPhoneExit(createMember.Phone))
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

     
        MemberViewModel? IMemberService.GetMemberDetails(int memberId)
        {
            var member = _memberRepository.GetById(memberId);
            if (member is null) return null;
            
                var MemberViewModel = new MemberViewModel()
                {
                    Name = member.Name,
                    Email = member.Email,
                    Phone = member.Phone,
                    Gender = member.Gender.ToString(),
                    DateOfBirth = member.DateOfBirth.ToShortDateString(),
                    Address = $"{member.Address.BuildingNumber }-{member.Address.City}-{member.Address.Street}",
                    Photo = member.Photo,
                };
                var memership = _membershipRepository
                                .GetAll( M=> M.MemberId == member.Id && M.Status == "Active")
                                .FirstOrDefault();
                if (memership is null ) return null;
            
                MemberViewModel.MembershipStartDate = memership.CreatedAt.ToShortDateString();
                MemberViewModel.MembershipEndDate = memership.EndDate.ToShortDateString();
                var plan = _planRepository.GetPlanById(memership.PlanId);
                if (plan is null) return null;

                MemberViewModel.PlanName = plan.Name;

                return MemberViewModel;
            
            
        }

       
        HealthRecordViewModel? IMemberService.GetMemberHealthDetails(int memberId)
        {
            var memberHealtRecord = _healthRecordRepository.GetById(memberId);
            if (memberHealtRecord is null) return null;
            return new HealthRecordViewModel()
            {
                Weight = memberHealtRecord.Weight,
                Height = memberHealtRecord.Height,
                BloodType = memberHealtRecord.BloodType,
                Note = memberHealtRecord.Note

            };

        }

        MemberToUpdateViewModel? IMemberService.GetMemberDetailsToUpdate(int memberId)
        {
            var member = _memberRepository.GetById(memberId);
            if (member is null) return null;
           return new MemberToUpdateViewModel()
            {
              Name = member.Name,
              Email = member.Email,
              Phone =member.Phone,
              Photo = member.Photo,
              BuildingNumber = member.Address.BuildingNumber,
              City = member.Address.City,
              Street = member.Address.Street
           };
            
        }

        bool IMemberService.UpdaeMember(int memberId, MemberToUpdateViewModel memberToUpdate)
        {

            try
            {

               


                if (IsEmailExit(memberToUpdate.Email) || IsPhoneExit(memberToUpdate.Phone))
                    return false;

                var member = _memberRepository.GetById(memberId);
                if (member is null) return false;



                member.Email = memberToUpdate.Email;
                member.Phone = memberToUpdate.Phone;
                member.Address.BuildingNumber = memberToUpdate.BuildingNumber;
                member.Address.City = memberToUpdate.City;
                member.Address.Street = memberToUpdate.Street;
                member.UpdatedAt = DateTime.Now;




                return _memberRepository.Update(member) > 0;

            }
            catch (Exception e)
            {

                return false;
            }
        }


        #region Helper Methods

        private bool IsEmailExit(string email)
        {
            return _memberRepository.GetAll(E => E.Email == email).Any();
        }
        private bool IsPhoneExit(string phone)
        {
            return _memberRepository.GetAll(E => E.Phone == phone).Any();
        }

        #endregion
    }
}
