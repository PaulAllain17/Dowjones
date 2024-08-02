using DowjonesAPI.Models;

namespace DowjonesAPI.Repositories
{
	public interface IPersonRepository
	{
		bool AddPerson(Person person);
		Task<List<Person>> GetPeople();
		Task<Person?> GetPerson(int id);
		void RemovePerson(Person person);
		void UpdatePerson(Person person);
	}
}
