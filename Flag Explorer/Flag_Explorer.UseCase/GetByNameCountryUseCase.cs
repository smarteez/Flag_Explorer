using Flag_Explorer.Domain.DTO;
using Flag_Explorer.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flag_Explorer.UseCase
{
    public class GetByNameCountryUseCase
    {
        private readonly ICountryRepository ICountryRepository;
        public GetByNameCountryUseCase(ICountryRepository countryRepository)
        {
            ICountryRepository = countryRepository;
        }
        public async Task<CountryDetailsDTO?> Execute(string countryName)
        {
            if (string.IsNullOrWhiteSpace(countryName))
            {
                throw new ArgumentException("Country name cannot be null or empty.", nameof(countryName));
            }
            try
            {
                var countryDetails = await ICountryRepository.GetCountryByNameAsync(countryName);
                if (countryDetails == null)
                {
                    throw new Exception($"Country with name {countryName} not found.");
                }
                return countryDetails;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching country details: {ex.Message}", ex);
            }
        }
    }
}
