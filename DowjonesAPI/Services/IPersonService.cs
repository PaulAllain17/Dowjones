using DowjonesAPI.Models;

namespace DowjonesAPI.Services
{
	public interface IPersonService
	{
		void AddPerson(Person person);
		Task<List<Person>> GetPeople();
		Task<Person?> GetPerson(int id);
		Task<bool> PersonExists(int id);
		void RemovePerson(Person person);
		void UpdatePerson(Person person);
	}
}