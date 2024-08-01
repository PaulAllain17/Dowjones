using DowjonesAPI.Models;

namespace DowjonesAPI.Repositories
{
	public interface ICompanyRepository
	{
		List<Company> GetAllCompanies();
	}
}
