using DowjonesAPI.Models;
using DowjonesAPI.Repositories;
using DowjonesAPI.Services;
using DowjonesAPI.Utilities;
using Moq;

namespace DowjonesAPIUnitTests.Services
{
	public class CompanyServiceTests
	{
		private Mock<ICompanyRepository> _companyRepositoryMock;
		private Mock<ICompanyUtility> _companyUtilityMock;

		private CompanyService _companyService;

		[SetUp]
		public void Setup()
		{
			_companyRepositoryMock = new Mock<ICompanyRepository>();
			_companyUtilityMock = new Mock<ICompanyUtility>();

			_companyRepositoryMock.Setup(m => m.GetCompany(1)).ReturnsAsync(new Company { Name = "Apple" });
			_companyRepositoryMock.Setup(m => m.GetCompanies()).ReturnsAsync([new Company { Name = "Apple" }, new Company { Name = "Microsoft" }]);

			_companyUtilityMock.Setup(m => m.ProcessOwnedCompaniesOnUpdate(
				It.IsAny<List<OwnedCompany>>(),
				It.IsAny<List<OwnedCompany>>(),
				It.IsAny<List<Company>>()));

			_companyUtilityMock.Setup(m => m.ProcessOwnedCompaniesOnCreation(
				It.IsAny<List<OwnedCompany>>(),
				It.IsAny<List<Company>>()));

			_companyService = new CompanyService(
				_companyRepositoryMock.Object,
				_companyUtilityMock.Object);
		}

		[Test]
		public void UpdateCompany_CompanyExists_DoTheUpdate()
		{
			var company = new Company{
				Id = 1,
				Name = "Apple"
			};
			_companyRepositoryMock.Setup(m => m.CompanyExists(1)).ReturnsAsync(true);
			_companyRepositoryMock.Setup(m => m.UpdateCompany(company));

			_companyService.UpdateCompany(company);

			_companyRepositoryMock.Verify(m => m.UpdateCompany(It.IsAny<Company>()), Times.Once());
			_companyRepositoryMock.Verify(m => m.UpdateCompany(company), Times.Once());
			_companyRepositoryMock.Verify(m => m.GetCompanies(), Times.Once());
			_companyRepositoryMock.Verify(m => m.GetCompany(It.IsAny<int>()), Times.Once());
			_companyRepositoryMock.Verify(m => m.GetCompany(1), Times.Once());
			_companyUtilityMock.Verify(m => m.ProcessOwnedCompaniesOnUpdate(
				It.IsAny<List<OwnedCompany>>(),
				It.IsAny<List<OwnedCompany>>(),
				It.IsAny<List<Company>>()), Times.Once());
		}

		[Test]
		public void UpdateCompany_CompanyDoesNotExist_DoNothing()
		{
			var company = new Company
			{
				Id = 1,
				Name = "Apple"
			};
			_companyRepositoryMock.Setup(m => m.CompanyExists(1)).ReturnsAsync(false);
			_companyRepositoryMock.Setup(m => m.UpdateCompany(company));

			_companyService.UpdateCompany(company);

			_companyRepositoryMock.Verify(m => m.UpdateCompany(It.IsAny<Company>()), Times.Never);
			_companyRepositoryMock.Verify(m => m.GetCompanies(), Times.Never);
			_companyRepositoryMock.Verify(m => m.GetCompany(It.IsAny<int>()), Times.Never);
			_companyUtilityMock.Verify(m => m.ProcessOwnedCompaniesOnUpdate(
				It.IsAny<List<OwnedCompany>>(),
				It.IsAny<List<OwnedCompany>>(),
				It.IsAny<List<Company>>()), Times.Never);
		}

		[Test]
		public void AddCompany_CompanyProvided_AddCompany()
		{
			var company = new Company
			{
				Id = 1,
				Name = "Apple"
			};
			_companyRepositoryMock.Setup(m => m.AddCompany(company));

			_companyService.AddCompany(company);

			_companyRepositoryMock.Verify(m => m.AddCompany(company), Times.Once);
			_companyRepositoryMock.Verify(m => m.GetCompanies(), Times.Once);
			_companyUtilityMock.Verify(m => m.ProcessOwnedCompaniesOnCreation(
				It.IsAny<List<OwnedCompany>>(),
				It.IsAny<List<Company>>()), Times.Once);
		}

		[Test]
		public void CompanyExists_IdProvided_CallRepository()
		{
			_companyRepositoryMock.Setup(m => m.CompanyExists(1));

			_companyService.CompanyExists(1);

			_companyRepositoryMock.Verify(m => m.CompanyExists(1), Times.Once);
		}

		[Test]
		public void GetCompanies_CallRepository()
		{
			_companyRepositoryMock.Setup(m => m.GetCompanies());

			_companyService.GetCompanies();

			_companyRepositoryMock.Verify(m => m.GetCompanies(), Times.Once);
		}

		[Test]
		public void GetCompany_IdProvided_CallRepository()
		{
			_companyRepositoryMock.Setup(m => m.GetCompany(1));

			_companyService.GetCompany(1);

			_companyRepositoryMock.Verify(m => m.GetCompany(1), Times.Once);
		}

		[Test]
		public void RemoveCompany_CompanyProvided_CallRepository()
		{
			var company = new Company
			{
				Id = 1,
				Name = "Apple"
			};
			_companyRepositoryMock.Setup(m => m.RemoveCompany(company));

			_companyService.RemoveCompany(company);

			_companyRepositoryMock.Verify(m => m.RemoveCompany(company), Times.Once);
		}
	}
}
