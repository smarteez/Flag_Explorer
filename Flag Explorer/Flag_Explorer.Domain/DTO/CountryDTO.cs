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
        public string Name { get; set; }
        public string Flag { get; set; }
        public bool HasErrors { get; set; }
        public string ErrorMessage { get; set; } 
    }
}
