using DowjonesAPI.Controllers;
using DowjonesAPI.Models;
using DowjonesAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace DowjonesAPIUnitTests.Controllers
{
	public class CompanyControllerTests
	{
		private Mock<ICompanyService> _companyServiceMock;

		private CompanyController _companyController;

		private List<Company> _companies;
		private Company _company;

		[SetUp]
		public void Setup()
		{
			_companyServiceMock = new Mock<ICompanyService>();
			_companies = [new Company { Name = "Apple" }, new Company { Name = "Microsoft" }];
			_company = new Company { Name = "Apple", Id = 1 };

			_companyServiceMock.Setup(m => m.GetCompany(1)).ReturnsAsync(_company);
			_companyServiceMock.Setup(m => m.GetCompanies()).ReturnsAsync(_companies);

			_companyController = new CompanyController(_companyServiceMock.Object);
		}

		[Test]
		public async Task GetCompanies_CallService()
		{
			var result = await _companyController.GetCompanies();

			_companyServiceMock.Verify(m => m.GetCompanies(), Times.Once());
			Assert.That(result.Value, Is.SameAs(_companies));
		}

		[Test]
		public async Task GetCompany_IdProvided_CallService()
		{
			var result = await _companyController.GetCompany(1);

			_companyServiceMock.Verify(m => m.GetCompany(1), Times.Once());
			Assert.That(result.Value, Is.SameAs(_company));
		}

		[Test]
		public void PostCompany_CompanyProvided_CallService()
		{
			var result = _companyController.PostCompany(_company);

			_companyServiceMock.Verify(m => m.AddCompany(_company), Times.Once());
			var createdAtActionResult = result.Result as CreatedAtActionResult;
			Assert.That(createdAtActionResult.Value, Is.SameAs(_company));
			Assert.That(createdAtActionResult.ActionName, Is.EqualTo("GetCompany"));
		}

		[Test]
		public async Task PutCompany_CompanyExists_CallService()
		{
			_companyServiceMock.Setup(m => m.CompanyExists(1)).ReturnsAsync(true);
			var result = await _companyController.PutCompany(_company);

			_companyServiceMock.Verify(m => m.UpdateCompany(_company), Times.Once());
			var createdAtActionResult = result.Result as CreatedAtActionResult;
			Assert.That(createdAtActionResult.Value, Is.SameAs(_company));
			Assert.That(createdAtActionResult.ActionName, Is.EqualTo("GetCompany"));
		}

		[Test]
		public async Task PutCompany_CompanyDoesNotExist_ReturnBadRequest()
		{
			_companyServiceMock.Setup(m => m.CompanyExists(1)).ReturnsAsync(false);
			var result = await _companyController.PutCompany(_company);

			_companyServiceMock.Verify(m => m.UpdateCompany(_company), Times.Never);
			var createdAtActionResult = result.Result as BadRequestObjectResult;
			Assert.That(createdAtActionResult.Value, Is.EqualTo("Company with Id: 1 does not exist."));
		}

		[Test]
		public async Task DeleteCompany_CompanyExists_CallService()
		{
			var result = await _companyController.DeleteCompany(1);

			_companyServiceMock.Verify(m => m.RemoveCompany(_company), Times.Once);
			Assert.IsTrue(result is NoContentResult);
		}

		[Test]
		public async Task DeleteCompany_CompanyDoesNotExists_ReturnNotFound()
		{
			var result = await _companyController.DeleteCompany(2);

			_companyServiceMock.Verify(m => m.RemoveCompany(It.IsAny<Company>()), Times.Never);
			Assert.That(result is NotFoundResult);
		}
	}
}
