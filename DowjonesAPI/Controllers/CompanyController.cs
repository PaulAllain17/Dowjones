using DowjonesAPI.Models;
using DowjonesAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DowjonesAPI.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CompanyController : ControllerBase
	{
		private readonly ICompanyService _companyService;

		public CompanyController(ICompanyService companyService)
		{
			_companyService = companyService;
		}

		// GET: api/Entities
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Company>>> GetCompanies()
		{
			return await _companyService.GetCompanies();
		}

		// GET: api/Entities/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Company>> GetCompany(int id)
		{
			var company = await _companyService.GetCompany(id);

			if (company == null)
			{
				return NotFound();
			}

			return company;
		}

		//// POST: api/Entities
		[HttpPost]
		public ActionResult<Company> PostCompany(Company company)
		{
			_companyService.AddCompany(company);
			return CreatedAtAction("GetCompany", new { id = company.Id }, company);
		}

		//// PUT: api/Entities
		[HttpPut]
		public async Task<ActionResult<Company>> PutCompany(Company company)
		{
			var id = company.Id;
			var companyExists = await _companyService.CompanyExists(id);
			if (!companyExists)
			{
				return BadRequest($"Company with Id: {id} does not exist.");
			}

			_companyService.UpdateCompany(company);

			return CreatedAtAction("GetCompany", new { id }, company);
		}

		//// DELETE: api/Entities/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCompany(int id)
		{
			var company = await _companyService.GetCompany(id);
			if (company == null)
			{
				return NotFound();
			}

			_companyService.RemoveCompany(company);

			return NoContent();
		}
	}
}
