﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dating.Logic.DTO
{
    public class SearchResultDTO
    {
        public int ResultsTotal { get; set; }

        public IEnumerable<SearchResultProfile> Profiles { get; set; }
    }

    public class SearchResultProfile
    {
        public int Age { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }
    }
}
