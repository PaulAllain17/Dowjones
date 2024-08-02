using DowjonesAPI.Models;

namespace DowjonesAPI.Data
{
	public class MockedDatabase : IMockedDatabase
	{
		private List<Person> persons = [
			new Person()
			{
				Id = 1,
				Name = "Gilbert"
			},
			new Person()
			{
				Id = 2,
				Name = "Gaston"
			}
		];

		private List<Company> companies = [
			new Company()
			{
				Id = 1,
				Name = "Microsoft"
			},
			new Company()
			{
				Id = 2,
				Name = "Apple"
			}
		];

		public bool AddPerson(Person person)
		{
			var ownedCompanies = person.Companies;

			var success = ProcessOwnedCompanies(ownedCompanies);
			if (!success)
			{
				return false;
			}
			persons.Add(person);
			return true;
		}

		public async Task<List<Person>> GetPeople() => persons;

		public async Task<Person?> GetPerson(int id)
			=> persons.Find(p => p.Id == id);

		public void RemovePerson(Person person)
		{
			persons.Remove(person);
		}

		public void UpdatePerson(Person person)
		{
			var index = persons.FindIndex(p => p.Id == person.Id);
			if (index >= 0)
			{
				persons[index] = person;
			}
		}

		public async Task<List<Company>> GetCompanies() => companies;

		public async Task<Company?> GetCompany(int id)
			=> companies.Find(c => c.Id == id);

		public bool AddCompany(Company company)
		{
			var ownedCompanies = company.Companies;

			var success = ProcessOwnedCompanies(ownedCompanies);
			if (!success)
			{
				return false;
			}
			companies.Add(company);
			return true;
		}

		public void UpdateCompany(Company company)
		{
			var index = companies.FindIndex(p => p.Id == company.Id);
			if (index >= 0)
			{
				companies[index] = company;
			}
		}

		public void RemoveCompany(Company company)
		{
			companies.Remove(company);
		}

		private bool ProcessOwnedCompanies(List<OwnedCompany> ownedCompanies)
		{
			if (ownedCompanies != null)
			{
				foreach (var ownedCompany in ownedCompanies)
				{
					var companyFromList = companies.Find(c => c.Id == ownedCompany.CompanyId);
					if (companyFromList == null)
					{
						return false;
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

			return true;
		}
	}
}
