using TicketAPI.Context;
using TicketAPI.Models;

namespace TicketAPI.Repository
{
	internal class Repository<T> : IRepository<T> where T : class
	{
		private readonly TicketManagerDbContext _dbContext;

		public Repository(TicketManagerDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public IEnumerable<T> GetAll()
		{
			return _dbContext.Set<T>().ToList();
		}

		public T GetById(int id)
		{
			return _dbContext.Set<T>().Find(id);
		}

		public void Add(T entity)
		{
			_dbContext.Set<T>().Add(entity);
			_dbContext.SaveChanges();
		}

		public void Update(T entity)
		{
			_dbContext.Set<T>().Update(entity);
			_dbContext.SaveChanges();
		}

		public void Delete(T entity)
		{
			_dbContext.Set<T>().Remove(entity);
			_dbContext.SaveChanges();
		}

        public Users GetByUserEmail(string email)
        {
            return _dbContext.Set<Users>().FirstOrDefault(_ => _.Email == email);
        }
    }
}
