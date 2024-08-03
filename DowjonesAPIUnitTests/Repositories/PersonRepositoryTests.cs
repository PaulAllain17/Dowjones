using DowjonesAPI.Data;
using DowjonesAPI.Models;
using DowjonesAPI.Repositories;
using Moq;

namespace DowjonesAPIUnitTests.Repositories
{
	public class PersonRepositoryTests
	{
		private Mock<IMockedDatabase> _mockedDatabase;

		private PersonRepository _personRepository;

		private List<Person> _people;
		private Person _person;

		[SetUp]
		public void Setup()
		{
			_mockedDatabase = new Mock<IMockedDatabase>();
			_people = [new Person { Name = "Apple" }, new Person { Name = "Microsoft" }];
			_person = new Person { Name = "Apple", Id = 1 };

			_mockedDatabase.Setup(m => m.GetPerson(1)).ReturnsAsync(_person);
			_mockedDatabase.Setup(m => m.GetPeople()).ReturnsAsync(_people);

			_personRepository = new PersonRepository(_mockedDatabase.Object);
		}

		[Test]
		public void AddPerson_CallDatabase()
		{
			_personRepository.AddPerson(_person);

			_mockedDatabase.Verify(m => m.AddPerson(_person), Times.Once());
		}

		[Test]
		public async Task GetPeople_CallDatabase()
		{
			var result = await _personRepository.GetPeople();

			_mockedDatabase.Verify(m => m.GetPeople(), Times.Once());
			Assert.That(result, Is.SameAs(_people));
		}

		[Test]
		public async Task GetPerson_IdProvided_CallService()
		{
			var result = await _personRepository.GetPerson(1);

			_mockedDatabase.Verify(m => m.GetPerson(1), Times.Once());
			Assert.That(result, Is.SameAs(_person));
		}

		[Test]
		public void RemovePerson_PersonProvided_CallService()
		{
			_personRepository.RemovePerson(_person);

			_mockedDatabase.Verify(m => m.RemovePerson(_person), Times.Once());
		}

		[Test]
		public void UpdatePerson_PersonProvided_CallService()
		{
			_personRepository.UpdatePerson(_person);

			_mockedDatabase.Verify(m => m.UpdatePerson(_person), Times.Once());
		}

		[Test]
		public async Task PersonExists_PersonDoesExist_ReturnTrue()
		{
			var result = await _personRepository.PersonExists(1);

			_mockedDatabase.Verify(m => m.GetPerson(1), Times.Once());
			Assert.IsTrue(result);
		}

		[Test]
		public async Task PersonExists_PersonDoesNotExist_ReturnFalse()
		{
			var result = await _personRepository.PersonExists(2);

			_mockedDatabase.Verify(m => m.GetPerson(2), Times.Once());
			Assert.IsFalse(result);
		}
	}
}
