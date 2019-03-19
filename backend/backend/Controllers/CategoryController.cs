using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/canteens/{canteenId}/categories")]
    public class CategoryController : Controller
    {
        // GET: api/canteens/5/categories
        [HttpGet]
        public IEnumerable<string> Get(int categoryId)
        {
            return new string[] { categoryId.ToString() };
        }

        // GET api/canteens/5/categories/3
        [HttpGet("{id}")]
        public string Get(int categoryId, int id)
        {
            return categoryId.ToString() + " " + id.ToString();
        }
    }
}
