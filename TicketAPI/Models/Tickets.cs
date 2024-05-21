using System.ComponentModel.DataAnnotations;

namespace TicketAPI.Models
{
	public class Tickets
	{
		[Key]
		public int IdTicket { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public int? UserId {  get; set; }
		public string Status { get; set; }
	}
}
