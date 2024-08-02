namespace DowjonesAPI.Models
{
	public class OwnedCompany
	{
		public required Company Company { get; set; }

		public int Percentage {
			get
			{
				return Percentage;
			}
			set {
				if (value > 60)
				{
					Company.IsControlled = true;
				}
				else if (Percentage > 60)
				{
					Company.IsControlled = false;
				}

				Percentage = value;
			}
		}
	}
}
