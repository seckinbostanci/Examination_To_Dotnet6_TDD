using IncirAgaci.API.Config;
using IncirAgaci.API.Models;
using Microsoft.Extensions.Options;
using System.Net;

namespace IncirAgaci.API.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly UsersApiOptions _apiConfig;

        public UserService(HttpClient httpClient, IOptions<UsersApiOptions> apiConfig)
        {
            _httpClient = httpClient;
            _apiConfig = apiConfig.Value;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var usersResponse = await _httpClient.GetAsync(_apiConfig.Endpoint);
            if (usersResponse.StatusCode == HttpStatusCode.NotFound)
            {
                return new List<User>();
            }
            var responseContent = usersResponse.Content;
            var allUsers = await responseContent.ReadFromJsonAsync<List<User>>();
            return allUsers.ToList();
        }
    }
}
