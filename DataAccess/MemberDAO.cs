using BussinessObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class MemberDAO
    {
        public static async Task<List<Member>> GetMembersAsync()
        {
            List<Member> members = new List<Member>();
            try
            {
                using (var context = new MyDbContext())
                {
                    members = await context.Members.AsNoTracking().ToListAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return members;
        }

        public static async Task<Member> FindMemberByIdAsync(int memberId)
        {
            Member member = null;
            try
            {
                using (var context = new MyDbContext())
                {
                    member = await context.Members.SingleOrDefaultAsync(x => x.Id == memberId);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return member;
        }

        public static async Task SaveMemberAsync(Member member)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    await context.Members.AddAsync(member);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task UpdateMemberAsync(Member member)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    context.Entry(member).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task DeleteMemberAsync(Member member)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    var existingMember = await context.Members.SingleOrDefaultAsync(c => c.Id == member.Id);
                    if (existingMember != null)
                    {
                        context.Members.Remove(existingMember);
                        await context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
