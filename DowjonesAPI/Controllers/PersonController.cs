using DowjonesAPI.Models;
using DowjonesAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DowjonesAPI.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class PersonController : ControllerBase
	{
		private readonly IPersonRepository _personRepository;

		public PersonController(IPersonRepository personRepository)
		{
			_personRepository = personRepository;
		}

		// GET: api/Entities
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Person>>> GetPeople()
		{
			return await _personRepository.GetPeople();
		}

		// GET: api/Entities/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Person>> GetPerson(int id)
		{
			var person = await _personRepository.GetPerson(id);

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
			var success = _personRepository.AddPerson(person);

			if (success)
			{
				return CreatedAtAction("GetPerson", new { id = person.Id }, person);
			}
			else
			{
				return BadRequest();
			}
		}

		//// PUT: api/Entities
		[HttpPut]
		public async Task<ActionResult<Person>> PutPerson(Person person)
		{
			var id = person.Id;
			var personExists = await PersonExists(id);
			if (!personExists)
			{
				return BadRequest($"Person with Id: {id} does not exist.");
			}

			_personRepository.UpdatePerson(person);

			return CreatedAtAction("GetPerson", new { id }, person);
		}

		//// DELETE: api/Entities/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeletePerson(int id)
		{
			var person = await _personRepository.GetPerson(id);
			if (person == null)
			{
				return NotFound();
			}

			_personRepository.RemovePerson(person);

			return NoContent();
		}

		private async Task<bool> PersonExists(int id)
		{
			var person = await _personRepository.GetPerson(id);
			return person is not null;
		}
	}
}
