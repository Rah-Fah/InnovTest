namespace TicketManager.Models.ViewModels
{
	public class UserTicketViewModel
	{
		public List<UserTicket> _userTicket { get; set; }
        public List<User> _users { get; set; }
        public UserTicketViewModel()
        {
            
        }

        public UserTicketViewModel(List<UserTicket> userTicket)
        {
            _userTicket = userTicket;
        }

        public UserTicketViewModel(List<User> users)
        {
            _users = users;
        }

    }
}
