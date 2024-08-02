using DowjonesAPI.Data;
using DowjonesAPI.Models;

namespace DowjonesAPI.Repositories
{
	public class PersonRepository : IPersonRepository
	{
		private readonly IMockedDatabase _mockedDatabase;

		public PersonRepository(IMockedDatabase mockDatabase)
		{
			_mockedDatabase = mockDatabase;
		}

		public void AddPerson(Person person)
		{
			_mockedDatabase.AddPerson(person);
		}

		public Task<List<Person>> GetPeople()
		{
			return _mockedDatabase.GetPeople();
		}

		public Task<Person?> GetPerson(int id)
		{
			return _mockedDatabase.GetPerson(id);
		}

		public void RemovePerson(Person person)
		{
			_mockedDatabase.RemovePerson(person);
		}

		public void UpdatePerson(Person person)
		{
			_mockedDatabase.UpdatePerson(person);
		}
	}
}
