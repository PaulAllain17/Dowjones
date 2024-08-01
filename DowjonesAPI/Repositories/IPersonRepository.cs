using DowjonesAPI.Models;

namespace DowjonesAPI.Repositories
{
	public interface IPersonRepository
	{
		void AddPerson(Person person);
		Task<List<Person>> GetAllPersons();
		Task<Person> GetPerson(int id);
		void RemovePerson(Person person);
	}
}
