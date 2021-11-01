using BDDPOC;
using BDDPOC.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace BDDPOCTest
{
    public class UsersControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient client;

        // See: https://timdeschryver.dev/blog/how-to-test-your-csharp-web-api#a-simple-test
        public UsersControllerTests(WebApplicationFactory<Startup> fixture)
        {
            this.client = fixture.CreateClient();
        }

        [Fact]
        public async Task Test1()
        {
            // Arrange

            // Act
            var userServiceResponse = await this.client.GetAsync("/users").ConfigureAwait(false);
            string usersStringResponse = await userServiceResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
            var serializationOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var users = JsonSerializer.Deserialize<List<User>>(usersStringResponse, serializationOptions);

            // Assert
        }
    }
}
