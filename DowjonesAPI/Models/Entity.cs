namespace DowjonesAPI.Models
{
	public abstract class Entity
	{
		public int Id { get; set; }
		public required string Name { get; set; }
		public List<OwnedCompany>? OwnedCompanies { get; set; }
	}
}
