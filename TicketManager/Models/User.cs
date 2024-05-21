using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TicketManager.Models
{
	public class User
	{
		public int IdUser { get; set; }
        [BindProperty]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Le champ Nom ne peut contenir que des lettres.")]
        [Required(ErrorMessage = "Le champ Nom est requis.")]
        [StringLength(50, ErrorMessage = "Le champ Nom ne peut pas dépasser {1} caractères.")]
        public string UserName { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Le champ Email est requis.")]
        [EmailAddress(ErrorMessage = "Le champ Email doit être une adresse email valide.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public bool isEditMode { get; set; } = false;
        [BindProperty]
        [Required(ErrorMessage = "Le champ Nom est requis.")]
        public Boolean IsAdmin { get; set; } = false;
    }
}
