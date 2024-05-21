using Microsoft.AspNetCore.Mvc;
using TicketAPI.Models;
using TicketAPI.Repository;

namespace TicketAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TicketsController : ControllerBase
	{
		private readonly IRepository<Tickets> _ticketRepository;

		public TicketsController(IRepository<Tickets> ticketRepository)
		{
			_ticketRepository = ticketRepository;
		}

		[HttpGet]
		public ActionResult<IEnumerable<Tickets>> Get()
		{
			return Ok(_ticketRepository.GetAll());
		}

		[HttpGet("{id}")]
		public ActionResult<Tickets> GetById(int id)
		{
			return Ok(_ticketRepository.GetById(id));
		}

        [HttpPost]
		public IActionResult Post([FromBody] Tickets ticket)
		{
			_ticketRepository.Add(ticket);
			return Ok($"POST request réussie avec la valeur : {ticket.Title}");
		}

		[HttpPut("{id}")]
		public IActionResult Put(int id, [FromBody] Tickets ticket)
		{
			var updTicket = _ticketRepository.GetById(id);
			if (updTicket != null)
			{
				updTicket.Title = ticket.Title;
				updTicket.Description = ticket.Description;
				updTicket.Status = ticket.Status;
				_ticketRepository.Update(updTicket);
				return Ok($"PUT request réussie pour l'ID : {id} avec la valeur : {ticket.Title}");
			}
			return Ok($"PUT request erreur pour l'ID : {id} avec la valeur : {ticket.Title}");
		}

		[HttpPut("{id}/assign/{userId}")]
		public IActionResult Put(int id,int userId)
		{
			var updTicket = _ticketRepository.GetById(id);
			if (updTicket != null)
			{
				updTicket.UserId = userId;
				_ticketRepository.Update(updTicket);
				return Ok($"PUT request réussie pour l'ID : {id} avec la valeur : {userId}");
			}
			
			return Ok($"PUT request erreur pour l'ID : {id} avec la valeur : {userId}");
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var updTicket = _ticketRepository.GetById(id);
			if (updTicket != null)
			{
				_ticketRepository.Delete(updTicket);
				return Ok($"DELETE request réussie pour l'ID : {id}");
			}			
			return Ok($"DELETE request erreur pour l'ID : {id}");
		}
	}
}
