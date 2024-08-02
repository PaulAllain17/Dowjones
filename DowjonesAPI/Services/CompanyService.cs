using DowjonesAPI.Models;
using DowjonesAPI.Repositories;
using DowjonesAPI.Utilities;

namespace DowjonesAPI.Services
{
	public class CompanyService : ICompanyService
	{
		private readonly ICompanyRepository _companyRepository;
		private readonly ICompanyUtility _companyUtility;
		public CompanyService(
			ICompanyRepository companyRepository,
			ICompanyUtility companyUtility)
		{
			_companyRepository = companyRepository;
			_companyUtility = companyUtility;
		}

		public async void AddCompany(Company company)
		{
			_companyRepository.AddCompany(company);
			var companies = await _companyRepository.GetCompanies();
			_companyUtility.ProcessOwnedCompaniesOnCreation(company.OwnedCompanies, companies);
		}

		public Task<bool> CompanyExists(int id)
		{
			return _companyRepository.CompanyExists(id);
		}

		public Task<List<Company>> GetCompanies()
		{
			return _companyRepository.GetCompanies();
		}

		public Task<Company?> GetCompany(int id)
		{
			return _companyRepository.GetCompany(id);
		}

		public void RemoveCompany(Company company)
		{
			_companyRepository.RemoveCompany(company);
		}

		public async void UpdateCompany(Company company)
		{
			var companyExists = await _companyRepository.CompanyExists(company.Id);
			if (companyExists)
			{
				var previousCompany = await _companyRepository.GetCompany(company.Id);
				var companies = await _companyRepository.GetCompanies();

				_companyRepository.UpdateCompany(company);
				_companyUtility.ProcessOwnedCompaniesOnUpdate(
					company.OwnedCompanies,
					previousCompany.OwnedCompanies,
					companies);
			}
		}
	}
}
