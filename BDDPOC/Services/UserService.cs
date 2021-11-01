using BDDPOC.Configurations;
using BDDPOC.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace BDDPOC.Services
{
    public class UserService : IUserService
    {
        private readonly List<ServiceProviderConfiguration> serviceProviderConfigurations;
        private readonly HttpClient httpClient;

        public UserService(List<ServiceProviderConfiguration> serviceProviderConfigurations)
        {
            this.serviceProviderConfigurations = serviceProviderConfigurations;
            this.httpClient = new HttpClient
            {
                BaseAddress = new Uri(this.serviceProviderConfigurations.FirstOrDefault(spc => spc.Name == this.GetType().Name).Host)
            };
        }

        public async Task<List<User>> GetUsers()
        {
            HttpResponseMessage userServiceResponse = await this.httpClient.GetAsync("users").ConfigureAwait(false);
            string usersStringResponse = await userServiceResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
            var serializationOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return JsonSerializer.Deserialize<List<User>>(usersStringResponse, serializationOptions);
        }
    }
}
