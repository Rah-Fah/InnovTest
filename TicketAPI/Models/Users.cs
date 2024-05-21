using System.ComponentModel.DataAnnotations;

namespace TicketAPI.Models
{
	public class Users
	{
		[Key]
		public int IdUser { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email {  get; set; }
        public bool IsAdmin { get; set; }
    }
}
