using Microsoft.AspNetCore.Mvc;
using TicketAPI.Models;
using TicketAPI.Repository;

namespace TicketAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IRepository<Users> _userRepository;

		public UsersController(IRepository<Users> userRepository)
		{
			_userRepository = userRepository;
		}

		[HttpGet]
		public ActionResult<IEnumerable<Users>> Get()
		{
			return Ok(_userRepository.GetAll());
		}

		[HttpGet("{id}")]
		public ActionResult<Users> GetById(int id)
		{
			return Ok(_userRepository.GetById(id));
		}

        [HttpGet("by-email/{email}")]
        public ActionResult<Users> GetByUserEmail(string email)
        {
            return Ok(_userRepository.GetByUserEmail(email));
        }


        [HttpPost]
		public IActionResult Post([FromBody] Users user)
		{
			// Traitez la valeur postée
			_userRepository.Add(user);
			return Ok($"POST request réussie avec la valeur : {user.UserName}");
		}

		[HttpPut("{id}")]
		public IActionResult Put(int id, [FromBody] Users user)
		{
			// Traitez la mise à jour avec l'ID spécifié
			var updUser = _userRepository.GetById(id);
			if(updUser != null) {
				updUser.UserName = user.UserName;
				updUser.Email = user.Email;
				updUser.IsAdmin = user.IsAdmin;
			}
			_userRepository.Update(updUser);
			return Ok($"PUT request réussie pour l'ID : {id} avec la valeur : {user.UserName}");
		}
	}
}
