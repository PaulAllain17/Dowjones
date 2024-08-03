using DowjonesAPI.Models;
using DowjonesAPI.Repositories;
using DowjonesAPI.Services;
using DowjonesAPI.Utilities;
using Moq;

namespace DowjonesAPIUnitTests.Services
{
    public class PersonServiceTests
    {
		private Mock<IPersonRepository> _personRepositoryMock;
		private Mock<ICompanyRepository> _companyRepositoryMock;
		private Mock<ICompanyUtility> _companyUtilityMock;

		private PersonService _personService;

		[SetUp]
		public void Setup()
		{
			_personRepositoryMock = new Mock<IPersonRepository>();
			_companyRepositoryMock = new Mock<ICompanyRepository>();
			_companyUtilityMock = new Mock<ICompanyUtility>();

			_personRepositoryMock.Setup(m => m.GetPerson(1)).ReturnsAsync(new Person { Name = "Apple" });
			_companyRepositoryMock.Setup(m => m.GetCompanies()).ReturnsAsync([new Company { Name = "Apple" }, new Company { Name = "Microsoft" }]);

			_companyUtilityMock.Setup(m => m.ProcessOwnedCompaniesOnUpdate(
				It.IsAny<List<OwnedCompany>>(),
				It.IsAny<List<OwnedCompany>>(),
				It.IsAny<List<Company>>()));

			_companyUtilityMock.Setup(m => m.ProcessOwnedCompaniesOnCreation(
				It.IsAny<List<OwnedCompany>>(),
				It.IsAny<List<Company>>()));

			_personService = new PersonService(
				_personRepositoryMock.Object,
				_companyRepositoryMock.Object,
				_companyUtilityMock.Object);
		}

		[Test]
		public void PersonCompany_PersonExists_DoTheUpdate()
		{
			var person = new Person
			{
				Id = 1,
				Name = "Gilbert"
			};
			_personRepositoryMock.Setup(m => m.PersonExists(1)).ReturnsAsync(true);
			_personRepositoryMock.Setup(m => m.UpdatePerson(person));

			_personService.UpdatePerson(person);

			_personRepositoryMock.Verify(m => m.UpdatePerson(It.IsAny<Person>()), Times.Once());
			_personRepositoryMock.Verify(m => m.UpdatePerson(person), Times.Once());
			_companyRepositoryMock.Verify(m => m.GetCompanies(), Times.Once());
			_personRepositoryMock.Verify(m => m.GetPerson(It.IsAny<int>()), Times.Once());
			_personRepositoryMock.Verify(m => m.GetPerson(1), Times.Once());
			_companyUtilityMock.Verify(m => m.ProcessOwnedCompaniesOnUpdate(
				It.IsAny<List<OwnedCompany>>(),
				It.IsAny<List<OwnedCompany>>(),
				It.IsAny<List<Company>>()), Times.Once());
		}

		[Test]
		public void UpdatePerson_PersonDoesNotExist_DoNothing()
		{
			var person = new Person
			{
				Id = 1,
				Name = "Gilbert"
			};
			_personRepositoryMock.Setup(m => m.PersonExists(1)).ReturnsAsync(false);
			_personRepositoryMock.Setup(m => m.UpdatePerson(person));

			_personService.UpdatePerson(person);

			_personRepositoryMock.Verify(m => m.UpdatePerson(It.IsAny<Person>()), Times.Never);
			_companyRepositoryMock.Verify(m => m.GetCompanies(), Times.Never);
			_personRepositoryMock.Verify(m => m.GetPerson(It.IsAny<int>()), Times.Never);
			_companyUtilityMock.Verify(m => m.ProcessOwnedCompaniesOnUpdate(
				It.IsAny<List<OwnedCompany>>(),
				It.IsAny<List<OwnedCompany>>(),
				It.IsAny<List<Company>>()), Times.Never);
		}

		[Test]
		public void AddPerson_PersonProvided_AddPerson()
		{
			var person = new Person
			{
				Id = 1,
				Name = "Gilbert"
			};
			_personRepositoryMock.Setup(m => m.AddPerson(person));

			_personService.AddPerson(person);

			_personRepositoryMock.Verify(m => m.AddPerson(person), Times.Once);
			_companyRepositoryMock.Verify(m => m.GetCompanies(), Times.Once);
			_companyUtilityMock.Verify(m => m.ProcessOwnedCompaniesOnCreation(
				It.IsAny<List<OwnedCompany>>(),
				It.IsAny<List<Company>>()), Times.Once);
		}

		[Test]
		public void CompanyExists_IdProvided_CallRepository()
		{
			_personRepositoryMock.Setup(m => m.PersonExists(1));

			_personService.PersonExists(1);

			_personRepositoryMock.Verify(m => m.PersonExists(1), Times.Once);
		}

		[Test]
		public void GetPeople_CallRepository()
		{
			_personRepositoryMock.Setup(m => m.GetPeople());

			_personService.GetPeople();

			_personRepositoryMock.Verify(m => m.GetPeople(), Times.Once);
		}

		[Test]
		public void GetPerson_IdProvided_CallRepository()
		{
			_personRepositoryMock.Setup(m => m.GetPerson(1));

			_personService.GetPerson(1);

			_personRepositoryMock.Verify(m => m.GetPerson(1), Times.Once);
		}

		[Test]
		public void RemovePerson_PersonProvided_CallRepository()
		{
			var person = new Person
			{
				Id = 1,
				Name = "Gaston"
			};
			_personRepositoryMock.Setup(m => m.RemovePerson(person));

			_personService.RemovePerson(person);

			_personRepositoryMock.Verify(m => m.RemovePerson(person), Times.Once);
		}
	}
}
