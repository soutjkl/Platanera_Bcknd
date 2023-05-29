
 using Microsoft.AspNetCore.Mvc;
using bckPlatanera.Data.Models;
using Microsoft.AspNetCore.JsonPatch;
using bckPlatanera.Data;
using Microsoft.AspNetCore.Authorization;

namespace bckPlatanera.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly BdPlatContext _context;

        public PersonController(BdPlatContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        [Route("GetPerson")]
        public ActionResult<Person> GetPerson()
        {
            var client = _context.People.ToList();
            return Ok(client);
        }

        [HttpGet]
        [Route("GetByName")]
        public ActionResult<Person> GetByName(String nameUser)
        {
            try
            {
                var user = _context.People.ToList();
                if (nameUser != null)
                {
                    user = user.Where(x => x.NameUser.ToLower().IndexOf(nameUser) > -1).ToList();
                }
                return Ok(user);
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpGet]
        [Route("GetByTypeDocument")]
        public ActionResult<Person> GetByTypeDocument(String typeDocument)
        {
            try
            {
                var user = _context.People.ToList();
                if (typeDocument != null)
                {
                    user = user.Where(x => x.TypeDocument.ToLower().IndexOf(typeDocument) > -1).ToList();
                }
                return Ok(user);
            }
            catch
            {
                return BadRequest();
            }
        }

      

        [HttpPut]
        public async Task<IActionResult> Put(Person person)
        {
            var update = await _context.People.FindAsync(person.DocumentNumber);

            if (update == null)
                return BadRequest();

            update.DocumentNumber = person.DocumentNumber;
            update.NameUser = person.NameUser;
            update.SurnameUser = person.SurnameUser;
            update.TypeDocument = person.TypeDocument;
            update.Address = person.Address;
            update.Telephone = person.Telephone;
            update.Email = person.Email;
            update.Photo = person.Photo;

            var aux = await _context.SaveChangesAsync() > 0;
            if (!aux)
            {
                return NoContent();
            }
            return Ok(update);
        }


        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(string id, JsonPatchDocument<Person> _person)
        {
            var User = await _context.People.FindAsync(id);
            if (User == null)
            {
                return NotFound();
            }

            _person.ApplyTo(User, ModelState);

            await _context.SaveChangesAsync();
            return Ok(User);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> remove(string id, BdPlatContext _context)
        {
            var user = await _context.People.FindAsync(id);
            if (user == null)
            {
                return StatusCode(404);
            }
            _context.People.Remove(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }

    }

}
