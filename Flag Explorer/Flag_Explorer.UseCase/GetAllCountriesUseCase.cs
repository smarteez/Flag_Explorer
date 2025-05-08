using Flag_Explorer.Domain.DTO;
using Flag_Explorer.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flag_Explorer.UseCase
{
    public class GetAllCountriesUseCase
    {
        private readonly ICountryRepository ICountryRepository;

        public GetAllCountriesUseCase(ICountryRepository countryRepository)
        {
            ICountryRepository = countryRepository ?? throw new ArgumentNullException(nameof(countryRepository));
        }

        public async Task<List<CountryDTO?>> ExecuteAsync()
        {
            try
            {
                var countries = await ICountryRepository.GetAllCountriesAsync();
                if (countries == null)
                {
                    throw new Exception("The country list returned is null.");
                }
                return countries.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching countries: {ex.Message}", ex);
            }
        }
    }
}
