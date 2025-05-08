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
                return new CountryDetailsDTO
                {
                    HasErrors = true,
                    ErrorMessage = "Country name cannot be null or empty."
                };
            }
                return await ICountryRepository.GetCountryByNameAsync(countryName);
               
        }
    }
}
