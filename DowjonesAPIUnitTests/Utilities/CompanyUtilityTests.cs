using DowjonesAPI.Data;
using DowjonesAPI.Models;
using DowjonesAPI.Utilities;
using Moq;

namespace DowjonesAPIUnitTests.Utilities
{
    public class CompanyUtilityTests
    {
        private Mock<IMockedDatabase> _mockedDatabase;

        private CompanyUtility _companyUtility;

        [SetUp]
        public void Setup()
        {
            _mockedDatabase = new Mock<IMockedDatabase>();
            _mockedDatabase.Setup(m => m.UpdateCompany(It.IsAny<Company>()));
            _companyUtility = new CompanyUtility(_mockedDatabase.Object);
        }

        [Test]
        public void ProcessOwnedCompaniesOnCreation_1CompanyIsControlled_ControlledCompanyFlagGetsSetTo1()
        {
            var ownedCompanies = new List<OwnedCompany>() {
                new OwnedCompany {
                    CompanyId = 1,
                    Percentage = 64 },
                new OwnedCompany {
                    CompanyId = 2,
                    Percentage = 50 },
                new OwnedCompany {
                    CompanyId = 3,
                    Percentage = 40 }
            };
            var companies = new List<Company>() {
                new Company {
                    Id = 1,
                    Name = "Apple" },
                new Company {
                    Id = 2,
                    Name = "Microsoft" }
            };
            _companyUtility.ProcessOwnedCompaniesOnCreation(ownedCompanies, companies);
            _mockedDatabase.Verify(m => m.UpdateCompany(It.IsAny<Company>()), Times.Once());
            _mockedDatabase.Verify(m => m.UpdateCompany(It.Is<Company>(c => c.IsControlled == true &&
                                                                            c.Id == 1 &&
                                                                            c.Name == "Apple")), Times.Once());
        }

        [Test]
        public void ProcessOwnedCompaniesOnUpdate_1CompanyIsControlled_ControlledCompanyFlagGetsSetTo1()
        {
            var previousOwnedCompanies = new List<OwnedCompany>() {
                new OwnedCompany {
                    CompanyId = 1,
                    Percentage = 64 },
                new OwnedCompany {
                    CompanyId = 2,
                    Percentage = 70 },
                new OwnedCompany {
                    CompanyId = 3,
                    Percentage = 40 }
            };
            var ownedCompanies = new List<OwnedCompany>() {
                new OwnedCompany {
                    CompanyId = 1,
                    Percentage = 64 },
                new OwnedCompany {
                    CompanyId = 2,
                    Percentage = 50 },
                new OwnedCompany {
                    CompanyId = 3,
                    Percentage = 40 }
            };
            var companies = new List<Company>() {
                new Company {
                    Id = 1,
                    Name = "Apple" },
                new Company {
                    Id = 2,
                    Name = "Microsoft" }
            };
            _companyUtility.ProcessOwnedCompaniesOnUpdate(ownedCompanies, previousOwnedCompanies, companies);
            _mockedDatabase.Verify(m => m.UpdateCompany(It.IsAny<Company>()), Times.Exactly(2));
            _mockedDatabase.Verify(m => m.UpdateCompany(It.Is<Company>(c => c.IsControlled == true &&
                                                                            c.Id == 1 &&
                                                                            c.Name == "Apple")), Times.Once());
            _mockedDatabase.Verify(m => m.UpdateCompany(It.Is<Company>(c => c.IsControlled == false &&
                                                                            c.Id == 2 &&
                                                                            c.Name == "Microsoft")), Times.Once());
        }
    }
}