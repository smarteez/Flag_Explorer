using Flag_Explorer.Domain.DTO;
using Flag_Explorer.UseCase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Flag_Explorer.API.Controllers
{
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly GetAllCountriesUseCase _getAllCountriesUseCase;
        private readonly GetByNameCountryUseCase _getByNameCountryUseCase;
        public CountryController(GetAllCountriesUseCase getAllCountriesUseCase, GetByNameCountryUseCase getByNameCountryUseCase)
        {
            _getAllCountriesUseCase = getAllCountriesUseCase;
            _getByNameCountryUseCase = getByNameCountryUseCase;
        }
        [HttpGet]
        [Route("api/countries")]
        [SwaggerResponse(200, Type = typeof(CountryDTO))]
        public async Task<IActionResult> GetAllCountries()
        {
            var countries = await _getAllCountriesUseCase.ExecuteAsync();
            return Ok(countries);
        }
        [HttpGet]
        [Route("api/countries/{countryName}")]
        [SwaggerResponse(200, Type = typeof(CountryDetailsDTO))]
        public async Task<IActionResult> GetCountryByName(string countryName)
        {
            var countryDetails = await _getByNameCountryUseCase.Execute(countryName);
            if (countryDetails == null)
            {
                return NotFound();
            }
            return Ok(countryDetails);
        }
    }
}
