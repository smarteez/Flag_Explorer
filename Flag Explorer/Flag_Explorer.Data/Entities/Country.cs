﻿using Flag_Explorer.Data.Private;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flag_Explorer.Data.Entities
{
    public class Country
    {
        public Name name { get; set; }
        public string official { get; set; }
        public List<string> capital { get; set; }
        public int population { get; set; }
        public Flags flags { get; set; }
    }




}
