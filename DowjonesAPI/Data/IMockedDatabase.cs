using DowjonesAPI.Models;

namespace DowjonesAPI.Data
{
	public interface IMockedDatabase
	{
		void AddPerson(Person person);
		Task<List<Person>> GetAllPersons();
		Task<Person> GetPerson(int id);
		void RemovePerson(Person person);
	}
}
