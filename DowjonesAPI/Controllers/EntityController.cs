using DowjonesAPI.Data;
using DowjonesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DowjonesAPI.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class EntityController : ControllerBase
	{
		private readonly AppDbContext _context;

		public EntityController(AppDbContext context)
		{
			_context = context;
		}

		// GET: api/Entities
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Entity>>> GetEntity()
		{
			return await _context.Entities.ToListAsync();
		}

		// GET: api/Entities/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Entity>> GetEntity(int id)
		{
			var product = await _context.Entities.FindAsync(id);

			if (product == null)
			{
				return NotFound();
			}

			return product;
		}

		// PUT: api/Entities/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutEntity(int id, Entity entity)
		{
			if (id != entity.Id)
			{
				return BadRequest();
			}

			_context.Entry(entity).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!EntityExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return NoContent();
		}

		// POST: api/Entities
		[HttpPost]
		public async Task<ActionResult<Entity>> PostEntity(Entity entity)
		{
			_context.Entities.Add(entity);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetProduct", new { id = entity.Id }, entity);
		}

		// DELETE: api/Entities/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteEntity(int id)
		{
			var product = await _context.Entities.FindAsync(id);
			if (product == null)
			{
				return NotFound();
			}

			_context.Entities.Remove(product);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool EntityExists(int id)
		{
			return _context.Entities.Any(e => e.Id == id);
		}
	}
}
