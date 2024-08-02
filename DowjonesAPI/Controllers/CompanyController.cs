using DowjonesAPI.Models;
using DowjonesAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DowjonesAPI.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CompanyController : ControllerBase
	{
		private readonly ICompanyRepository _companyRepository;

		public CompanyController(ICompanyRepository companyRepository)
		{
			_companyRepository = companyRepository;
		}

		// GET: api/Entities
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Company>>> GetCompanies()
		{
			return await _companyRepository.GetCompanies();
		}

		// GET: api/Entities/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Company>> GetCompany(int id)
		{
			var company = await _companyRepository.GetCompany(id);

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
			_companyRepository.AddCompany(company);
			return CreatedAtAction("GetCompany", new { id = company.Id }, company);
		}

		//// PUT: api/Entities
		[HttpPut]
		public async Task<ActionResult<Company>> PutCompany(Company company)
		{
			var id = company.Id;
			var companyExists = await CompanyExists(id);
			if (!companyExists)
			{
				return BadRequest($"Company with Id: {id} does not exist.");
			}

			_companyRepository.UpdateCompany(company);

			return CreatedAtAction("GetCompany", new { id }, company);
		}

		//// DELETE: api/Entities/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCompany(int id)
		{
			var company = await _companyRepository.GetCompany(id);
			if (company == null)
			{
				return NotFound();
			}

			_companyRepository.RemoveCompany(company);

			return NoContent();
		}

		private async Task<bool> CompanyExists(int id)
		{
			var company = await _companyRepository.GetCompany(id);
			return company is not null;
		}
	}
}
