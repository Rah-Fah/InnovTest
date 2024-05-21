using Newtonsoft.Json;
using RestSharp;
using TicketManager.Models.ViewModels;
using TicketManager.Models;

namespace TicketManager.Services
{
	public class UserService
	{
		public static UserTicketViewModel GetAll()
		{
			List<User> userList = new List<User>();		
			RestResponse responseUser = TicketApi("/Users", Method.Get);
			if (responseUser.IsSuccessful)
			{
				userList = JsonConvert.DeserializeObject<List<User>>(responseUser.Content);
			}
			else
			{
				Console.WriteLine("Erreur lors de la requête : " + responseUser.ErrorMessage);
			}		
			return new UserTicketViewModel(userList); ;
		}



		public static User GetById(int id)
		{
			User user = new User();
			var urlApi = "https://localhost:7013/api";
			var client = new RestClient(urlApi);
			var requestUser = new RestRequest("/Users/{id}", Method.Get);
			requestUser.AddUrlSegment("id", id);
			var responseUser = client.Execute(requestUser);
			if (responseUser.IsSuccessful)
			{
				user = JsonConvert.DeserializeObject<User>(responseUser.Content);
			}
			else
			{
				Console.WriteLine("Erreur lors de la requête : " + responseUser.ErrorMessage);
			}
			
			return user;
		}



        public static User GetByUserEmail(string email)
        {
            User user = new User();
            var urlApi = "https://localhost:7013/api";
            var client = new RestClient(urlApi);
            var requestUser = new RestRequest("/Users/by-email/{email}", Method.Get);
            requestUser.AddUrlSegment("email", email);
            var responseUser = client.Execute(requestUser);
            if (responseUser.IsSuccessful)
            {
                return JsonConvert.DeserializeObject<User>(responseUser.Content);
            }
            else
            {
                Console.WriteLine("Erreur lors de la requête : " + responseUser.ErrorMessage);
            }

            return null;
        }

        public static void Save(User user)
		{
			var urlApi = "https://localhost:7013/api";
			var client = new RestClient(urlApi);
			var requestUser = new RestRequest("/Users/", Method.Post);
			string json = JsonConvert.SerializeObject(user);
			requestUser.AddJsonBody(json);
			var response = client.Execute<User>(requestUser);
			if (response.IsSuccessful)
			{

			}
			else
			{

			}
		}

		public static void Update(int id,User user)
		{
			var urlApi = "https://localhost:7013/api";
			var client = new RestClient(urlApi);
			var requestUser = new RestRequest("/Users/{id}", Method.Put);
			string json = JsonConvert.SerializeObject(user);
			requestUser.AddUrlSegment("id", id.ToString());
			requestUser.AddJsonBody(json);
			var response = client.Execute<User>(requestUser);
			if (response.IsSuccessful)
			{

			}
			else
			{

			}
		}
		

		private static RestResponse TicketApi(string url, Method method)
		{
			var urlApi = "https://localhost:7013/api";
			var client = new RestClient(urlApi);
			var request = new RestRequest(url, method);
			var response = client.Execute(request);
			return response;
		}
	}
}
