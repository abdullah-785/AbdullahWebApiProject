using AssignmentWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AssignmentWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactApiController : ControllerBase
    {
        
        private readonly ContactContext _dbContext;
        
        //Create constructor
        public ContactApiController(ContactContext dbContaxt)
        {
            _dbContext = dbContaxt;
        }



        //Get all record from database
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> GetContact()
        {
            if (_dbContext.contact == null)
            {
                return NotFound();
            }
            return await _dbContext.contact.ToListAsync();
        }


        // Get specific record from database using id
        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> getcontact(int id)
        {
            if (_dbContext.contact == null)
            {
                return NotFound();
            }

            var contact = await _dbContext.contact.FindAsync(id);
            if(contact == null)
            {
                return NotFound();
            }
            return contact;
        }


        //Store data in database
        [HttpPost]
        public async Task<ActionResult<Contact>> saveContact(Contact con_us)
        {
            _dbContext.contact.Add(con_us);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetContact), new {id = con_us.Id}, con_us);
        }


        //Update data in database
        [HttpPut]
        public async Task<ActionResult<Contact>> putContact(int id, Contact contact)
        {
            if(id != contact.Id)
            {
                return BadRequest();
            }

            _dbContext.Entry(contact).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactAvailable(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok();
        }


        //This method is used to check contact have or not in database using id
        private bool ContactAvailable(int id)
        {
            return (_dbContext.contact?.Any(x => x.Id == id)).GetValueOrDefault();
        }


        //Delete Record From database
        //[HttpDelete("{id}")]

        //public async Task<IActionResult> deleteContact(int id)
        //{
        //    if(_dbContext.contact == null)
        //    {
        //        return NotFound();
        //    }
        //    var con = await _dbContext.contact.FindAsync(id);

        //    if(con != null)
        //    {
        //        return NotFound();
        //    }
            
        //    _dbContext.contact.Remove(con);
        //    await _dbContext.SaveChangesAsync();

        //    return Ok();

        //}
    }
}
