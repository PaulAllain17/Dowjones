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
		public async Task<ActionResult<IEnumerable<Entity>>> GetPerson()
		{
			return await _personRepository.GetAllPersons();
		}

		// GET: api/Entities/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Entity>> GetPerson(int id)
		{
			var person = await _personRepository.GetPerson(id);

			if (person == null)
			{
				return NotFound();
			}

			return person;
		}

		//// PUT: api/Entities/5
		//[HttpPut("{id}")]
		//public async Task<IActionResult> PutPerson(int id, Person entity)
		//{
		//	if (id != entity.Id)
		//	{
		//		return BadRequest();
		//	}

		//	_personRepository.Entry(entity).State = EntityState.Modified;

		//	try
		//	{
		//		await _personRepository.SaveChangesAsync();
		//	}
		//	catch (DbUpdateConcurrencyException)
		//	{
		//		if (!PersonExists(id))
		//		{
		//			return NotFound();
		//		}
		//		else
		//		{
		//			throw;
		//		}
		//	}

		//	return NoContent();
		//}

		//// POST: api/Entities
		[HttpPost]
		public ActionResult<Person> PostPerson(Person person)
		{
			_personRepository.AddPerson(person);
			return CreatedAtAction("GetPerson", new { id = person.Id }, person);
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

		//private bool PersonExists(int id)
		//{
		//	return _personRepository.Persons.Any(e => e.Id == id);
		//}
	}
}
