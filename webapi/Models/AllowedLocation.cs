using System;
namespace webapi.Models
{
    public class AllowedLocation
    {
        private AllowedLocation item;

        public AllowedLocation(string name)
        {
            this.name = name;
        }

        public int id { get; set; }
        public string name { get; set; }
    }
}
