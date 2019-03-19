using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api")]
    public class CategoryController : Controller
    {
        // GET: api/canteens/5/categories
        [HttpGet("canteens/{canteenId}/categories")]
        public IEnumerable<string> GetCanteenCategories(int canteenId)
        {
            return new string[] { canteenId.ToString() };
        }

        // GET api/categories/3
        [HttpGet("categories/{id}")]
        public string GetCategory(int id)
        {
            return id.ToString();
        }
    }
}
