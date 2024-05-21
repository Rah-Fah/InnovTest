using System.ComponentModel.DataAnnotations;

namespace TicketManager.Models
{
	public class Ticket
	{
		public int IdTicket { get; set; }
        [Required(ErrorMessage = "Le champ Titre est requis.")]
        [StringLength(100, ErrorMessage = "Le champ Titre ne peut pas dépasser {1} caractères.")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Le champ Titre ne peut contenir que des lettres.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Le champ Description est requis.")]
        public string Description { get; set; }
		public int? UserId { get; set; }
        [Required(ErrorMessage = "Le champ Status est requis.")]
        public string Status { get; set; }
        public bool isEditMode { get; set; }
    }
}
