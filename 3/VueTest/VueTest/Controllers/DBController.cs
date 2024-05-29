using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VueTest.Controllers
{
    [Route("get/[controller]")]
    [ApiController]
    public class DBController : ControllerBase
    {
        [HttpGet("Select")]
        public List<SQL> GetData(string company)
        {
            SQL sql = new();
            return sql.Select(company);
        }
    }
}
