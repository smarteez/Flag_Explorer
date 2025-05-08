using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flag_Explorer.Domain.DTO
{
    public class CountryDetailsDTO : CountryDTO
    {
        public string Official { get; set; }
        public int Population { get; set; }
        public string Capital { get; set; }

    }
}
