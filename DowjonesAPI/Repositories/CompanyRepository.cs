﻿using DowjonesAPI.Data;
using DowjonesAPI.Models;

namespace DowjonesAPI.Repositories
{
	public class CompanyRepository : ICompanyRepository
	{
		private readonly IMockedDatabase _mockedDatabase;

		public CompanyRepository(IMockedDatabase mockDatabase)
		{
			_mockedDatabase = mockDatabase;
		}

		public bool AddCompany(Company company)
		{
			return _mockedDatabase.AddCompany(company);
		}

		public Task<List<Company>> GetCompanies()
		{
			return _mockedDatabase.GetCompanies();
		}

		public async Task<Company?> GetCompany(int id)
		{
			return await _mockedDatabase.GetCompany(id);
		}

		public void RemoveCompany(Company company)
		{
			_mockedDatabase.RemoveCompany(company);
		}

		public void UpdateCompany(Company company)
		{
			_mockedDatabase.UpdateCompany(company);
		}
	}
}
