using DowjonesAPI.Data;
using DowjonesAPI.Models;
using DowjonesAPI.Services;
using DowjonesAPI.Utilities;
using Moq;

namespace DowjonesAPIUnitTests
{
	public class CompanyUtilityTests
	{
		private IMock<IMockedDatabase> _mockDatabase;

		private CompanyUtility _companyService;

		[SetUp]
		public void Setup()
		{
			_mockDatabase = new Mock<IMockedDatabase>();
			_companyService = new CompanyUtility(_mockDatabase.Object);
		}

		[Test]
		public void Test1()
		{
			var ownedCompanies = new List<OwnedCompany>() {
				new OwnedCompany {
					CompanyId = 1,
					Percentage = 64 }
			};
			var companies = new List<Company>() {
				new Company {
					Id = 1,
					Name = "Apple" }
			};
			_companyService.ProcessOwnedCompaniesOnCreation(ownedCompanies, companies);
			Assert.Pass();
		}
	}
}