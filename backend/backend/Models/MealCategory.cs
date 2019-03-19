using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class MealCategory
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CanteenId { get; set; }
        public Canteen Canteen { get; set; }
    }
}
