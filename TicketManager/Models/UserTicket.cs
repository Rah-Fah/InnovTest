namespace TicketManager.Models
{
	public class UserTicket
	{
		public int IdTicket { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string Status { get; set; }
		public string? UserName { get; set; }
	}
}
