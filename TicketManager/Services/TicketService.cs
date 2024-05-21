
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Security.Policy;
using System.Text;
using TicketManager.Models;
using TicketManager.Models.ViewModels;

namespace TicketManager.Services
{
	public static class TicketService
	{
		public static UserTicketViewModel GetAll()
		{		
			List<UserTicket> userTicket = new List<UserTicket>();
			List<Ticket> ticketList = new List<Ticket>();
			List<User> userList = new List<User>();
			RestResponse responseTicket = TicketApi("/Tickets", Method.Get);
			if (responseTicket.IsSuccessful)
			{
				ticketList = JsonConvert.DeserializeObject<List<Ticket>>(responseTicket.Content);				
			}
			else
			{
				Console.WriteLine("Erreur lors de la requête : " + responseTicket.ErrorMessage);
			}
			RestResponse responseUser = TicketApi("/Users", Method.Get);
			if (responseTicket.IsSuccessful)
			{
				userList = JsonConvert.DeserializeObject<List<User>>(responseUser.Content);
			}
			else
			{
				Console.WriteLine("Erreur lors de la requête : " + responseUser.ErrorMessage);
			}
			if(ticketList.Count!= null && ticketList.Any()) {
				foreach (var item in ticketList)
				{

                    userTicket.Add(
						new UserTicket()
						{
							IdTicket = item.IdTicket,
							Description =  item.Description,
							Status = item.Status,
							Title = item.Title,
							UserName = item.UserId !=null ? userList.Where(_ => _.IdUser == item.UserId).FirstOrDefault().UserName : string.Empty,
						}
					);
				}
			}
            return new UserTicketViewModel(userTicket);
		}

		public static Ticket GetById(int id)
		{
			List<UserTicketViewModel> result = new List<UserTicketViewModel>();
			List<UserTicket> userTicket = new List<UserTicket>();
            Ticket ticket = new Ticket();
			List<User> userList = new List<User>();
			var urlApi = "https://localhost:7013/api";
			var client = new RestClient(urlApi);
			var requestTicket = new RestRequest("/Tickets/{id}", Method.Get);
			requestTicket.AddUrlSegment("id",id);
			var responseTicket = client.Execute(requestTicket);
			if (responseTicket.IsSuccessful)
			{
				return JsonConvert.DeserializeObject<Ticket>(responseTicket.Content);
			}
			else
			{
				Console.WriteLine("Erreur lors de la requête : " + responseTicket.ErrorMessage);
			}
			return null;
		}


		public static void Save(Ticket ticket)
		{
			var urlApi = "https://localhost:7013/api";
			var client = new RestClient(urlApi);
			var requestTicket = new RestRequest("/Tickets", Method.Post);           
            string json = JsonConvert.SerializeObject(ticket);
			requestTicket.AddJsonBody(json); 
			var response = client.Execute<Ticket>(requestTicket);
			if (response.IsSuccessful)
			{
				
			}
			else
			{
				
			}
		}

		public static void Update(Ticket ticket)
		{
			var urlApi = "https://localhost:7013/api";
			var client = new RestClient(urlApi);
			var requestTicket = new RestRequest("/Tickets/{id}", Method.Put);
            requestTicket.AddUrlSegment("id", ticket.IdTicket);
            string json = JsonConvert.SerializeObject(ticket);
			requestTicket.AddJsonBody(json);
			var response = client.Execute<Ticket>(requestTicket);
			if (response.IsSuccessful)
			{
				
			}
			else
			{
				
			}
		}
		public static void AssignTicket(int ticketId,int? userId)
		{
			var urlApi = "https://localhost:7013/api";
			var client = new RestClient(urlApi);
			var requestTicket = new RestRequest("/Tickets/{id}/assign/{userId}", Method.Put);
			requestTicket.AddUrlSegment("id", ticketId.ToString());
			requestTicket.AddUrlSegment("userId", userId.ToString());
			var response = client.Execute<Ticket>(requestTicket);
			if (response.IsSuccessful)
			{
				
			}
			else
			{
				
			}
		}
		public static void Delete(int ticketId)
		{
			var urlApi = "https://localhost:7013/api";
			var client = new RestClient(urlApi);
			var requestTicket = new RestRequest("/Tickets/{id}", Method.Delete);
			requestTicket.AddUrlSegment("id", ticketId.ToString());
			var response = client.Execute<Ticket>(requestTicket);
			if (response.IsSuccessful)
			{

			}
			else
			{

			}
		}

		private static RestResponse TicketApi(string url,Method method)
		{
			var urlApi = "https://localhost:7013/api";
			var client = new RestClient(urlApi);
			var request = new RestRequest(url, method);
			var response = client.Execute(request);
			return response;
		}
	}
}
