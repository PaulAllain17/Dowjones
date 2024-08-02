using DowjonesAPI.Models;

namespace DowjonesAPI.Services
{
	public interface ICompanyService
	{
		void AddCompany(Company company);
		Task<bool> CompanyExists(int id);
		Task<List<Company>> GetCompanies();
		Task<Company?> GetCompany(int id);
		void RemoveCompany(Company company);
		void UpdateCompany(Company company);
	}
}