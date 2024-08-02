using DowjonesAPI.Models;

namespace DowjonesAPI.Repositories
{
	public interface IPersonRepository
	{
		void AddPerson(Person person);
		Task<List<Person>> GetPeople();
		Task<Person?> GetPerson(int id);
		void RemovePerson(Person person);
		void UpdatePerson(Person person);
		Task<bool> PersonExists(int id);
	}
}
