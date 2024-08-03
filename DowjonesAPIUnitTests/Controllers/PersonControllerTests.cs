using DowjonesAPI.Controllers;
using DowjonesAPI.Models;
using DowjonesAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace DowjonesAPIUnitTests.Controllers
{
	public class PersonControllerTests
	{
		private Mock<IPersonService> _personServiceMock;

		private PersonController _personController;

		private List<Person> _people;
		private Person _person;

		[SetUp]
		public void Setup()
		{
			_personServiceMock = new Mock<IPersonService>();
			_people = [new Person { Name = "Gilbert" }, new Person { Name = "Gaston" }];
			_person = new Person { Name = "Gilbert", Id = 1 };

			_personServiceMock.Setup(m => m.GetPerson(1)).ReturnsAsync(_person);
			_personServiceMock.Setup(m => m.GetPeople()).ReturnsAsync(_people);

			_personController = new PersonController(_personServiceMock.Object);
		}

		[Test]
		public async Task GetPeople_CallService()
		{
			var result = await _personController.GetPeople();

			_personServiceMock.Verify(m => m.GetPeople(), Times.Once());
			Assert.That(result.Value, Is.SameAs(_people));
		}

		[Test]
		public async Task GetPerson_IdProvided_CallService()
		{
			var result = await _personController.GetPerson(1);

			_personServiceMock.Verify(m => m.GetPerson(1), Times.Once());
			Assert.That(result.Value, Is.SameAs(_person));
		}

		[Test]
		public void PostPerson_PersonProvided_CallService()
		{
			var result = _personController.PostPerson(_person);

			_personServiceMock.Verify(m => m.AddPerson(_person), Times.Once());
			var createdAtActionResult = result.Result as CreatedAtActionResult;
			Assert.That(createdAtActionResult.Value, Is.SameAs(_person));
			Assert.That(createdAtActionResult.ActionName, Is.EqualTo("GetPerson"));
		}

		[Test]
		public async Task PutPerson_PersonExists_CallService()
		{
			_personServiceMock.Setup(m => m.PersonExists(1)).ReturnsAsync(true);
			var result = await _personController.PutPerson(_person);

			_personServiceMock.Verify(m => m.UpdatePerson(_person), Times.Once());
			var createdAtActionResult = result.Result as CreatedAtActionResult;
			Assert.That(createdAtActionResult.Value, Is.SameAs(_person));
			Assert.That(createdAtActionResult.ActionName, Is.EqualTo("GetPerson"));
		}

		[Test]
		public async Task PutPerson_PersonDoesNotExist_ReturnBadRequest()
		{
			_personServiceMock.Setup(m => m.PersonExists(1)).ReturnsAsync(false);
			var result = await _personController.PutPerson(_person);

			_personServiceMock.Verify(m => m.UpdatePerson(_person), Times.Never);
			var createdAtActionResult = result.Result as BadRequestObjectResult;
			Assert.That(createdAtActionResult.Value, Is.EqualTo("Person with Id: 1 does not exist."));
		}

		[Test]
		public async Task DeletePerson_PersonExists_CallService()
		{
			var result = await _personController.DeletePerson(1);

			_personServiceMock.Verify(m => m.RemovePerson(_person), Times.Once);
			Assert.IsTrue(result is NoContentResult);
		}

		[Test]
		public async Task DeletePerson_PersonDoesNotExists_ReturnNotFound()
		{
			var result = await _personController.DeletePerson(2);

			_personServiceMock.Verify(m => m.RemovePerson(It.IsAny<Person>()), Times.Never);
			Assert.That(result is NotFoundResult);
		}
	}
}
