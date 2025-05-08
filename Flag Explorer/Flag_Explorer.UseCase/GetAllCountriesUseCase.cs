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

                return await ICountryRepository.GetAllCountriesAsync();
            
        }
    }
}
