using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Flag_Explorer.Domain.DTO
{
    public class CountryDTO
    {
        [JsonPropertyName("common")]

        public string Name { get; set; }
        public string Flag { get; set; }
    }
}
