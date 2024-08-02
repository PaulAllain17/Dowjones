using DowjonesAPI.Models;

namespace DowjonesAPI.Utilities
{
	public interface ICompanyUtility
	{
		void ProcessOwnedCompaniesOnCreation(
			List<OwnedCompany> ownedCompanies,
			List<Company> companies);
		public void ProcessOwnedCompaniesOnUpdate(
			List<OwnedCompany> ownedCompanies,
			List<OwnedCompany> previousOwnedCompanies,
			List<Company> companies);
	}
}