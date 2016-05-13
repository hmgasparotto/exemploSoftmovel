using ExemploSoftmovel.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ExemploSoftmovel.Services
{
    public class SoftmovelServices
    {
        private HttpClient client;
        
        public SoftmovelServices()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://jsonplaceholder.typicode.com/");
        }

        public async Task<IEnumerable<User>> GetAllUsersFromApi()
        {
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var resultUsers = await client.GetAsync("users");
            if (resultUsers.IsSuccessStatusCode)
            {
                string json = await resultUsers.Content.ReadAsStringAsync();
                IEnumerable<User> users = JsonConvert.
                    DeserializeObject<IEnumerable<User>>(json);
                foreach (User u in users)
                {
                    u.Password = "";
                }
                return users;
            }
            return null;
        }
    }
}
