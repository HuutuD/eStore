using BussinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IMemberRepository
    {
        Task SaveMemberAsync(Member p);
        Task<Member> GetMemberByIdAsync(int id);
        Task DeleteMemberAsync(Member p);
        Task UpdateMemberAsync(Member p);
        Task<List<Member>> GetMembersAsync();
    }

}
