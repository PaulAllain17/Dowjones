using DowjonesAPI.Data;
using DowjonesAPI.Models;
using DowjonesAPI.Repositories;
using Moq;

namespace DowjonesAPIUnitTests.Repositories
{
	public class CompanyRepositoryTests
	{
		private Mock<IMockedDatabase> _mockedDatabase;

		private CompanyRepository _companyRepository;

		private List<Company> _companies;
		private Company _company;

		[SetUp]
		public void Setup()
		{
			_mockedDatabase = new Mock<IMockedDatabase>();
			_companies = [new Company { Name = "Apple" }, new Company { Name = "Microsoft" }];
			_company = new Company { Name = "Apple", Id = 1 };

			_mockedDatabase.Setup(m => m.GetCompany(1)).ReturnsAsync(_company);
			_mockedDatabase.Setup(m => m.GetCompanies()).ReturnsAsync(_companies);

			_companyRepository = new CompanyRepository(_mockedDatabase.Object);
		}

		[Test]
		public void AddCompany_CallDatabase()
		{
			_companyRepository.AddCompany(_company);

			_mockedDatabase.Verify(m => m.AddCompany(_company), Times.Once());
		}

		[Test]
		public async Task GetCompanies_CallDatabase()
		{
			var result = await _companyRepository.GetCompanies();

			_mockedDatabase.Verify(m => m.GetCompanies(), Times.Once());
			Assert.That(result, Is.SameAs(_companies));
		}

		[Test]
		public async Task GetCompany_IdProvided_CallService()
		{
			var result = await _companyRepository.GetCompany(1);

			_mockedDatabase.Verify(m => m.GetCompany(1), Times.Once());
			Assert.That(result, Is.SameAs(_company));
		}

		[Test]
		public void RemoveCompany_CompanyProvided_CallService()
		{
			_companyRepository.RemoveCompany(_company);

			_mockedDatabase.Verify(m => m.RemoveCompany(_company), Times.Once());
		}

		[Test]
		public void UpdateCompany_CompanyProvided_CallService()
		{
			_companyRepository.UpdateCompany(_company);

			_mockedDatabase.Verify(m => m.UpdateCompany(_company), Times.Once());
		}

		[Test]
		public async Task CompanyExists_CompanyDoesExist_ReturnTrue()
		{
			var result = await _companyRepository.CompanyExists(1);

			_mockedDatabase.Verify(m => m.GetCompany(1), Times.Once());
			Assert.IsTrue(result);
		}

		[Test]
		public async Task CompanyExists_CompanyDoesNotExist_ReturnFalse()
		{
			var result = await _companyRepository.CompanyExists(2);

			_mockedDatabase.Verify(m => m.GetCompany(2), Times.Once());
			Assert.IsFalse(result);
		}
	}
}
