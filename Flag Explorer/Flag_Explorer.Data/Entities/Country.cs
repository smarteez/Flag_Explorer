using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flag_Explorer.Data.Entities
{
    public class Country
    {
        public string Name { get; set; }
        public string OfficialName { get; set; }
        public string Region { get; set; }
        public string Subregion { get; set; }
        public List<string> Borders { get; set; }
        public long Population { get; set; }
        public Dictionary<string, string> Languages { get; set; }
        public string Flag { get; set; }
        public Dictionary<string, Currency> Currencies { get; set; }

    }
}
