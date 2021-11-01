using BDDPOC;
using BDDPOC.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;

namespace SpecFlowProjectBDDPOC.StepDefinitions
{
    [Binding]
    public class UsersStepDefinitions : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly ScenarioContext scenarioContext;
        private readonly HttpClient client;
        private List<User> users;

        public UsersStepDefinitions(WebApplicationFactory<Startup> fixture)
        {
            this.client = fixture.CreateClient();
        }

        [Given(@"Always")]
        public void GivenAlways()
        {
        }

        [When(@"A request is made to gather user data")]
        public async Task WhenARequestIsMadeToGatherUserData()
        {
            var userServiceResponse = await this.client.GetAsync("/users").ConfigureAwait(false);
            string usersStringResponse = await userServiceResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
            var serializationOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            this.users = JsonSerializer.Deserialize<List<User>>(usersStringResponse, serializationOptions);
        }

        [Then(@"Should return all users")]
        public void ThenShouldReturnAllUsers()
        {
            this.users.Count.Should().Be(3);
        }
    }
}
