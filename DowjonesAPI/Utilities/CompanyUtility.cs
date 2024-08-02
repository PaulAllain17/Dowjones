using DowjonesAPI.Data;
using DowjonesAPI.Models;

namespace DowjonesAPI.Utilities
{
	public class CompanyUtility : ICompanyUtility
	{
		private readonly IMockedDatabase _mockedDatabase;

		public CompanyUtility(IMockedDatabase mockedDatabase)
		{
			_mockedDatabase = mockedDatabase;
		}

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
							_mockedDatabase.UpdateCompany(new Company
							{
								Id = companyFromList.Id,
								Name = companyFromList.Name,
								OwnedCompanies = companyFromList.OwnedCompanies,
								IsControlled = true
							});
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
							_mockedDatabase.UpdateCompany(new Company
							{
								Id = companyFromList.Id,
								Name = companyFromList.Name,
								OwnedCompanies = companyFromList.OwnedCompanies,
								IsControlled = true
							});
						}
						else if (previousOwnedCompanies.Find(c => c.CompanyId == ownedCompany.CompanyId).Percentage > 60)
						{
							_mockedDatabase.UpdateCompany(new Company
							{
								Id = companyFromList.Id,
								Name = companyFromList.Name,
								OwnedCompanies = companyFromList.OwnedCompanies,
								IsControlled = false
							});
						}
					}
				}
			}
		}
	}
}
