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

		public void AddPerson(Person person)
		{
			persons.Add(person);
		}

		public async Task<List<Person>> GetAllPersons() => persons;

		public async Task<Person> GetPerson(int id)
			=> persons.Find(p => p.Id == id);

		public void RemovePerson(Person person)
		{
			persons.Remove(person);
		}
	}
}
