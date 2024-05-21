using TicketAPI.Models;

namespace TicketAPI.Repository
{
	public interface IRepository<T>
	{
		IEnumerable<T> GetAll();
		T GetById(int id);
		void Add(T entity);
		void Update(T entity);
		void Delete(T entity);

        Users GetByUserEmail(string email);
	}
}
