using Flag_Explorer.Data.Entities;
using Flag_Explorer.Data.Private;
using Flag_Explorer.Domain.DTO;
using Flag_Explorer.Repository.Modules;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System.Net;

namespace Flag_Explorer.Repository.Test
{
    [TestFixture]

    public class CountryRepositoryTests
    {
        private Mock<HttpMessageHandler> _httpMessageHandlerMock;
        private HttpClient _httpClient;
        private IOptions<ApiSettings> _apiSettings;
        private CountryRepository _countryRepository;


        [SetUp]
        public void Setup()
        {
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_httpMessageHandlerMock.Object);
            _apiSettings = Options.Create(new ApiSettings
            {
                RestCountriesUrl = "https://restcountries.com/v3.1/all",
                RestCountriesUrlByName = "https://restcountries.com/v3.1/name/"
            });

            _countryRepository = new CountryRepository(_apiSettings);
        }


        [Test]
        public async Task GetAllCountriesAsync_ShouldReturnCountries_WhenApiRespondsSuccessfully()
        {
            var result = await _countryRepository.GetAllCountriesAsync();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.GreaterThan(0));
            Assert.That(result[0].Name, Is.EqualTo("Åland Islands"));
        }



        [Test]
        public async Task GetCountryByNameAsync_ValidCountry_ReturnsDetails()
        {
            var result = await _countryRepository.GetCountryByNameAsync("Ukraine");
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo("Ukraine"));
        }


        [Test]
        public async Task GetCountryByNameAsync_InValidCountry_ReturnsHasError()
        {
            var result = await _countryRepository.GetCountryByNameAsync("Atlantis");
            Assert.That(result.HasErrors, Is.True);
        }



    }
}
