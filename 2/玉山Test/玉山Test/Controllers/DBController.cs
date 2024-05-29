using Microsoft.AspNetCore.Mvc;

namespace 玉山Test.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DBController : ControllerBase
    {
        [HttpGet( "Select")]
        public List<SQL> GetData(string company)
        {
            SQL sql = new();
            return sql.Select(company);
        }
        [HttpPost("Insert")]
        public String InsertData(string Date,string YM,string Company_Code,string Company_Name,string Industry,int OI_TM,int OI_LM,int OI_TMLY,double OI_LM_ID,double OI_TMLY_ID,int Diff_TM,int Diff_LY,double Diff_PC,string Remark)
        {
            SQL sql = new();
            return sql.Insert(Date, YM, Company_Code, Company_Name, Industry, OI_TM, OI_LM, OI_TMLY, OI_LM_ID, OI_TMLY_ID, Diff_TM, Diff_LY, Diff_PC, Remark);
        }
    }
}
