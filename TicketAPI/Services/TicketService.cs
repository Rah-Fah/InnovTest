using TicketAPI.Context;
using TicketAPI.Models;
using TicketAPI.Repository;

namespace TicketAPI.Services
{
	public class TicketService
	{
		private readonly IRepository<Tickets> _ticketRepository;
		public TicketService(IRepository<Tickets> ticketRepository)
		{
			_ticketRepository = ticketRepository;
		}
		public IEnumerable<Tickets> GetAllTicket()
		{
			return _ticketRepository.GetAll();
		}
		public void AddTicket(Tickets ticket)
		{
			_ticketRepository.Add(ticket);
		}

		public void UpdateTicket(Tickets ticket)
		{
			_ticketRepository.Update(ticket);
		}
		public void DeleteTicket(Tickets ticket)
		{
			_ticketRepository.Delete(ticket);
		}
		public Tickets GetByUserEmail(int id)
		{
			return _ticketRepository.GetById(id);
		}
	}
}
