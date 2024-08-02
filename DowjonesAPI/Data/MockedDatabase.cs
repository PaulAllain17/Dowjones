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

		public void AddPerson(Person person)
		{
			persons.Add(person);
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

		public void AddCompany(Company company)
		{
			companies.Add(company);
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
	}
}
