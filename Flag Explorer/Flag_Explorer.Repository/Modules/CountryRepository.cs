using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Flag_Explorer.Data.Entities;
using Flag_Explorer.Domain.DTO;
using Flag_Explorer.Repository.Contracts;
using Microsoft.Extensions.Options;


namespace Flag_Explorer.Repository.Modules
{
    public class CountryRepository : ICountryRepository
    {
        private string _apiAllUrl;
        private string _apiNameUrl;

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
                    try
                    {
                        var countries = JsonSerializer.Deserialize<List<CountryList>>(content);

                        if (countries == null)
                        {
                            throw new Exception("Failed to deserialize countries");
                        }

                        return countries.Select(x =>
                            x == null ? null : new CountryDTO
                            {
                                Name = x.name?.common ?? "Unknown",
                                Flag = x.flags?.png ?? string.Empty,
                            })
                            .OrderBy(x => x.Name)
                            .ToList();
                    }
                    catch (JsonException ex)
                    {
                        throw new Exception($"Failed to deserialize country details: {ex.Message}", ex);
                    }

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
                var response = await httpClient.GetAsync(_apiNameUrl + countryName);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    try
                    {
                        var countryDetails = JsonSerializer.Deserialize<List<Country>>(content);
                        if (countryDetails == null)
                        {
                            return null;
                        }


                        return countryDetails
                                        .Select(x => new CountryDetailsDTO
                                            {
                                                Name = x.name?.common ?? "Unknown",
                                                Official = x.name?.official ?? "Unknown",
                                                Capital = x.capital?.FirstOrDefault() ?? "Unknown",
                                                Population = x.population,
                                                Flag = x.flags?.png ?? string.Empty
                                            })
                                        .FirstOrDefault();
                    }
                    catch (JsonException ex)
                    {
                        throw new Exception($"Failed to deserialize country details: {ex.Message}", ex);
                    }

                }
                else
                {
                    throw new Exception("Failed to fetch country Details");
                }
            }
        }


    }
}
