using DowjonesAPI.Data;
using DowjonesAPI.Models;

namespace DowjonesAPI.Repositories
{
	public class PersonRepository : IPersonRepository
	{
		private readonly IMockedDatabase _mockDatabase;

		public PersonRepository(IMockedDatabase mockDatabase)
		{
			_mockDatabase = mockDatabase;
		}

		public void AddPerson(Person person)
		{
			_mockDatabase.AddPerson(person);
		}

		public Task<List<Person>> GetPeople()
		{
			return _mockDatabase.GetPeople();
		}

		public Task<Person?> GetPerson(int id)
		{
			return _mockDatabase.GetPerson(id);
		}

		public void RemovePerson(Person person)
		{
			_mockDatabase.RemovePerson(person);
		}

		public void UpdatePerson(Person person)
		{
			_mockDatabase.UpdatePerson(person);
		}
	}
}
