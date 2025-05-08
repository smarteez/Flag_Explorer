using Flag_Explorer.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flag_Explorer.Repository.Contracts
{
    public interface ICountryRepository
    {
       Task<List<CountryDTO?>> GetAllCountriesAsync();
       Task<CountryDetailsDTO?> GetCountryByNameAsync(string countryName);
    }
}
