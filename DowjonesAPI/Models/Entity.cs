namespace DowjonesAPI.Models
{
	public class Entity
	{
		public int Id { get; set; }
		public required string Name { get; set; }
		public List<Company>? Companies { get; set; }
	}
}
