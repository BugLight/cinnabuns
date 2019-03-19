using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class Canteen
    {
        public int Id { get; set; }

        public bool AcceptCards { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
    }
}
