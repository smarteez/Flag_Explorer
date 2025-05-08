using Flag_Explorer.API.Controllers;
using Flag_Explorer.Domain.DTO;
using Flag_Explorer.Repository.Contracts;
using Flag_Explorer.Repository.Modules;
using Flag_Explorer.UseCase;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Flag_Explorer.API.Test
{
    [TestFixture]
    public class CountryControllerTests
    {
        private TestServer _server;
        private HttpClient _client;

        [SetUp]
        public void SetUp()
        {
            var apiSettings = Options.Create(new ApiSettings
            {
                RestCountriesUrl = "https://restcountries.com/v3.1/all",  
                RestCountriesUrlByName = "https://restcountries.com/v3.1/name/" 
            });


            _server = new TestServer(new WebHostBuilder()
               .UseEnvironment("Development")
                .ConfigureServices(services =>
                {
                   // services.AddControllers();
                    services.AddControllers().AddApplicationPart(typeof(CountryController).Assembly);
                    services.AddSingleton(apiSettings);  
                    services.AddScoped<ICountryRepository, CountryRepository>(); 
                    services.AddScoped<GetAllCountriesUseCase>();
                    services.AddScoped<GetByNameCountryUseCase>();
                })
                .Configure(app =>
                {
                    app.UseRouting();
                    app.UseEndpoints(endpoints =>
                    {
                        endpoints.MapControllers();
                    });
                }));

            _client = _server.CreateClient();
         
        }

        [TearDown]
        public void TearDown()
        {
            _client.Dispose();
            _server.Dispose();
        }

        [Test]
        public async Task GetAllCountries_ShouldReturnOk_WithCountriesList()
        {

            // Act
            var response = await _client.GetAsync("/api/countries"); 
            var content = await response.Content.ReadAsStringAsync();

            var countries = JsonConvert.DeserializeObject<List<CountryDTO>>(content);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
            Assert.That(countries, Is.Not.Null);
            Assert.That(countries.Count, Is.GreaterThan(0));
        }

        public static IEnumerable<TestCaseData> CountryNames()
        {
            yield return new TestCaseData("People's Republic of China");
            yield return new TestCaseData("Ukraine");
            yield return new TestCaseData("Republic of Botswana");
        }


        [Test]
        [TestCaseSource(nameof(CountryNames))]

        public async Task GetCountryByName_ShouldReturnOk_WhenCountryExists(string countryName)
        {
            // Act
            var response = await _client.GetAsync($"/api/countries/{countryName}");
            var content = await response.Content.ReadAsStringAsync();
            var countryDetails = JsonConvert.DeserializeObject<CountryDetailsDTO>(content);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
            Assert.That(countryDetails, Is.Not.Null);
            Assert.That(countryDetails.Name, Is.EqualTo(countryName));
        }

        public static IEnumerable<TestCaseData> InvalidCountryNames()
        {
            yield return new TestCaseData("abc");
            yield return new TestCaseData("Atlantis");
        }

        [Test]
        [TestCaseSource(nameof(InvalidCountryNames))]
        public async Task GetCountryByName_ShouldReturnNotFound_WhenCountryDoesNotExist(string invalidCountryName)
        {
            // Arrange

            // Act
            var response = await _client.GetAsync($"/api/countries/{invalidCountryName}");

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NotFound));
        }
    }
}