using DowjonesAPI.Models;

namespace DowjonesAPI.Utilities
{
	public class CompanyUtility : ICompanyUtility
	{
		public void ProcessOwnedCompaniesOnCreation(List<OwnedCompany> ownedCompanies, List<Company> companies)
		{
			if (ownedCompanies != null)
			{
				foreach (var ownedCompany in ownedCompanies)
				{
					var companyFromList = companies.Find(c => c.Id == ownedCompany.CompanyId);
					if (companyFromList == null)
					{
						continue;
					}
					else
					{
						if (ownedCompany.Percentage > 60)
						{
							companyFromList.IsControlled = true;
						}
					}
				}
			}
		}

		public void ProcessOwnedCompaniesOnUpdate(
			List<OwnedCompany> ownedCompanies,
			List<OwnedCompany> previousOwnedCompanies,
			List<Company> companies)
		{
			if (ownedCompanies != null)
			{
				foreach (var ownedCompany in ownedCompanies)
				{
					var companyFromList = companies.Find(c => c.Id == ownedCompany.CompanyId);
					if (companyFromList == null)
					{
						continue;
					}
					else
					{
						if (ownedCompany.Percentage > 60)
						{
							companyFromList.IsControlled = true;
						}
						else if (previousOwnedCompanies.Find(c => c.CompanyId == ownedCompany.CompanyId).Percentage > 60)
						{
							companyFromList.IsControlled = false;
						}
					}
				}
			}
		}
	}
}
