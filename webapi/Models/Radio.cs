using System;
using System.Collections.Generic;

namespace webapi.Models
{
    public class Radio
    {
        
            public int id { get; set; }

            public string alias { get; set; }

            public List<string> allowed_locations { get; set; }

            public string location { get; set; } = "undefined";

    }
}
