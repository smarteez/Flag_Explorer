using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Flag_Explorer.Domain.DTO;
using Flag_Explorer.Repository.Contracts;
using Microsoft.Extensions.Options;


namespace Flag_Explorer.Repository.Modules
{
    public class CountryRepository : ICountryRepository
    {
        private readonly string _apiAllUrl;
        private readonly string _apiNameUrl;

        public CountryRepository(IOptions<ApiSettings> apiSettings)
        {
            _apiAllUrl = apiSettings.Value.RestCountriesUrl;
            _apiNameUrl = apiSettings.Value.RestCountriesUrlByName;
        }


        public async Task<List<CountryDTO?>> GetAllCountriesAsync()
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(_apiAllUrl);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var countries = JsonSerializer.Deserialize<List<CountryDTO>>(content);
                    return countries;
                }
                else
                {
                    throw new Exception("Failed to fetch countries");
                }
            }
        }

        public async Task<CountryDetailsDTO?> GetCountryByNameAsync(string countryName)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(_apiNameUrl+ countryName);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var countryDetails = JsonSerializer.Deserialize<CountryDetailsDTO>(content);
                    return countryDetails;
                }
                else
                {
                    throw new Exception("Failed to fetch country Details");
                }
            }
        }


    }
}
