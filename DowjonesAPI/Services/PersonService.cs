using DowjonesAPI.Models;
using DowjonesAPI.Repositories;
using DowjonesAPI.Utilities;

namespace DowjonesAPI.Services
{
	public class PersonService : IPersonService
	{
		private readonly IPersonRepository _personRepository;
		private readonly ICompanyRepository _companyRepository;
		private readonly ICompanyUtility _companyUtility;

		public PersonService(
			IPersonRepository personRepository,
			ICompanyRepository companyRepository,
			ICompanyUtility companyUtility)
		{
			_personRepository = personRepository;
			_companyRepository = companyRepository;
			_companyUtility = companyUtility;
		}

		public async void AddPerson(Person person)
		{
			_personRepository.AddPerson(person);
			var companies = await _companyRepository.GetCompanies();
			_companyUtility.ProcessOwnedCompaniesOnCreation(person.OwnedCompanies, companies);
		}

		public Task<List<Person>> GetPeople()
		{
			return _personRepository.GetPeople();
		}

		public Task<Person?> GetPerson(int id)
		{
			return _personRepository.GetPerson(id);
		}

		public Task<bool> PersonExists(int id)
		{
			return _personRepository.PersonExists(id);
		}

		public void RemovePerson(Person person)
		{
			_personRepository.RemovePerson(person);
		}

		public async void UpdatePerson(Person person)
		{
			var personExists = await _personRepository.PersonExists(person.Id);
			if (personExists)
			{
				var previousPerson = await _personRepository.GetPerson(person.Id);
				var companies = await _companyRepository.GetCompanies();

				_personRepository.UpdatePerson(person);
				_companyUtility.ProcessOwnedCompaniesOnUpdate(
					person.OwnedCompanies,
					previousPerson.OwnedCompanies,
					companies);
			}
			
		}
	}
}
