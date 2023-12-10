namespace MellonBank.Models
{
	public class AccountData
	{
		public int Id { get; set; }
		public string AccountId { get; set; } = string.Empty;
		public string CustomerAfm { get; set; } = string.Empty;
		public decimal AccountBalance { get; set; }
		public string AccountNumber { get; set; } = string.Empty;
		public string Branch { get; set; } = string.Empty;
		public string AccountType { get; set; } = string.Empty;
		public virtual User User { get; set; } = null!;
	}
}
