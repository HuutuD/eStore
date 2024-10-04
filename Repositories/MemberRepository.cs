using BussinessObject;
using DataAccess;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class MemberRepository : IMemberRepository
    {
        public async Task DeleteMemberAsync(Member p) => await MemberDAO.DeleteMemberAsync(p);
        public async Task<Member> GetMemberByIdAsync(int id) => await MemberDAO.FindMemberByIdAsync(id);
        public async Task<List<Member>> GetMembersAsync() => await MemberDAO.GetMembersAsync();
        public async Task SaveMemberAsync(Member p) => await MemberDAO.SaveMemberAsync(p);
        public async Task UpdateMemberAsync(Member p) => await MemberDAO.UpdateMemberAsync(p);

    }
}
