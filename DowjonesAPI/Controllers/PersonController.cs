using DowjonesAPI.Models;
using DowjonesAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DowjonesAPI.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class PersonController : ControllerBase
	{
		private readonly IPersonService _personService;

		public PersonController(IPersonService personService)
		{
			_personService = personService;
		}

		// GET: api/Entities
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Person>>> GetPeople()
		{
			return await _personService.GetPeople();
		}

		// GET: api/Entities/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Person>> GetPerson(int id)
		{
			var person = await _personService.GetPerson(id);

			if (person == null)
			{
				return NotFound();
			}

			return person;
		}

		//// POST: api/Entities
		[HttpPost]
		public ActionResult<Person> PostPerson(Person person)
		{
			_personService.AddPerson(person);
			return CreatedAtAction("GetPerson", new { id = person.Id }, person);
		}

		//// PUT: api/Entities
		[HttpPut]
		public async Task<ActionResult<Person>> PutPerson(Person person)
		{
			var id = person.Id;
			var personExists = await _personService.PersonExists(id);
			if (!personExists)
			{
				return BadRequest($"Person with Id: {id} does not exist.");
			}

			_personService.UpdatePerson(person);

			return CreatedAtAction("GetPerson", new { id }, person);
		}

		//// DELETE: api/Entities/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeletePerson(int id)
		{
			var person = await _personService.GetPerson(id);
			if (person == null)
			{
				return NotFound();
			}

			_personService.RemovePerson(person);

			return NoContent();
		}
	}
}
