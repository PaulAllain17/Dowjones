using DowjonesAPI.Models;

namespace DowjonesAPI.Data
{
	public interface IMockedDatabase
	{
		bool AddPerson(Person person);
		Task<List<Person>> GetPeople();
		Task<Person?> GetPerson(int id);
		void RemovePerson(Person person);
		void UpdatePerson(Person person);
		Task<List<Company>> GetCompanies();
		Task<Company?> GetCompany(int id);
		bool AddCompany(Company company);
		void UpdateCompany(Company company);
		void RemoveCompany(Company company);
	}
}
