using DowjonesAPI.Models;

namespace DowjonesAPI.Repositories
{
	public interface ICompanyRepository
	{
		bool AddCompany(Company company);
		Task<List<Company>> GetCompanies();
		Task<Company?> GetCompany(int id);
		void RemoveCompany(Company company);
		void UpdateCompany(Company company);
	}
}
