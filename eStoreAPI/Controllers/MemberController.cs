using BussinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.IRepository;

namespace eStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberRepository _context;

        public MemberController(IMemberRepository context)
        {
            _context = context;
        }

        // GET: api/Member
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> GetMember()
        {
            var Member = await _context.GetMembersAsync();
            if (Member == null || !Member.Any())
            {
                return BadRequest();
            }
            return Ok(Member);
        }

        // GET: api/Member/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Member>> GetMember(int id)
        {

            var Member = await _context.GetMemberByIdAsync(id);
            if (Member == null)
            {
                return BadRequest();
            }
            return Ok(Member);
        }

        // PUT: api/Member/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMember(int id, Member Member)
        {
            if (id != Member.Id)
            {
                return BadRequest();
            }

            try
            {
                await _context.UpdateMemberAsync(Member);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await MemberExists(id))
                {
                    return BadRequest();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Member
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Member>> PostMember(Member Member)
        {

            await _context.SaveMemberAsync(Member);
            return CreatedAtAction(nameof(GetMember), new { id = Member.Id }, Member);
        }

        // DELETE: api/Member/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(int id)
        {
            var Member = await _context.GetMemberByIdAsync(id);
            if (Member == null)
            {
                return BadRequest();
            }

            await _context.DeleteMemberAsync(Member);
            return NoContent();
        }

        private async Task<bool> MemberExists(int id)
        {
            var Member = await _context.GetMemberByIdAsync(id);
            return Member != null;
        }
    }
}
