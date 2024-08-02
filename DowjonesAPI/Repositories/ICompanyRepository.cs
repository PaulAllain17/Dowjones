using DowjonesAPI.Models;

namespace DowjonesAPI.Repositories
{
	public interface ICompanyRepository
	{
		void AddCompany(Company company);
		Task<List<Company>> GetCompanies();
		Task<Company?> GetCompany(int id);
		void RemoveCompany(Company company);
		void UpdateCompany(Company company);
		Task<bool> CompanyExists(int id);
	}
}
