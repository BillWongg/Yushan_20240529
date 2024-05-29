using System.Data.SqlClient;

namespace 玉山Test
{
    public class SQL
    {
        public string Publication_Date { get; set; } = "";
        public string Publication_YM { get; set; } = "";
        public string Company_Code { get; set; } = "";
        public string Company_Name { get; set; } = "";
        public string Industry { get; set; } = "";
        public int  OI_TM { get; set; }
        public int OI_LM { get; set; }
        public int OI_TMLY { get; set; }
        public double  OI_LM_ID { get; set; }
        public double  OI_TMLY_ID { get; set; }
        public int Diff_TM { get; set; }
        public int Diff_LY { get; set; }
        public double Diff_PC { get; set; }
        public string Remark { get; set; } = "";

        /// <summary>
        /// 查詢資料(上市公司每月營業收入彙總表)
        /// </summary>
        /// <param name="company">公司名稱或all(呈現所有公司)</param>
        /// <returns>Publication_Date(出表日期)、Publication_YM(資料年月)、Company_Code(公司代號)、Company_Name(公司名稱)、Industry(產業別)、OI_TM(營業收入-當月營收)、OI_LM(營業收入-上月營收)、OI_TMLY(營業收入-去年當月營收)、OI_LM_ID(營業收入-上月比較增減(%))、OI_TMLY_ID(營業收入-去年同月增減(%))、Diff_TM(累計營業收入-當月累計營收)、Diff_LY(累計營業收入-去年累計營收)、Diff_PC(累計營業收入-前期比較增減(%))、Remark(備註)</returns>
        public List<SQL> Select(string company)
        {
            List<SQL> reItem = new();
            SqlConnectionStringBuilder connStr = new()
            {
                DataSource = ".",
                InitialCatalog = "Yushan",
                UserID = "ys_test",
                Password = "test123"
            };
            using (SqlConnection conn = new(connStr.ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new("EXEC Select_OperatingIncome @Name;", conn))
                {
                    cmd.Parameters.AddWithValue("@Name", company);
                    using SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        int.TryParse(rd["OI_TM"].ToString(), out int tm);
                        int.TryParse(rd["OI_LM"].ToString(), out int lm);
                        int.TryParse(rd["OI_TMLY"].ToString(), out int tmly);
                        double.TryParse(rd["OI_LM_ID"].ToString(), out double  lmID);
                        double.TryParse(rd["OI_TMLY_ID"].ToString(), out double tmlyID);
                        int.TryParse(rd["Diff_TM"].ToString(), out int dtm);
                        int.TryParse(rd["Diff_LY"].ToString(), out int dly);
                        double.TryParse(rd["Diff_PC"].ToString(), out double dpc);
                        SQL sql = new()
                        {
                            Publication_Date = rd["Date"].ToString()!,
                            Publication_YM = rd["YM"].ToString()!,
                            Company_Code = rd["Company_Code"].ToString()!,
                            Company_Name = rd["Company_Name"].ToString()!,
                            Industry = rd["Industry"].ToString()!,
                            OI_TM = tm,
                            OI_LM = lm,
                            OI_TMLY = tmly,
                            OI_LM_ID = lmID,
                            OI_TMLY_ID = tmlyID,
                            Diff_TM = dtm,
                            Diff_LY = dly,
                            Diff_PC = dpc,
                            Remark = rd["Remark"].ToString()!
                        };

                        reItem.Add(sql);
                    }
                    rd.Close();
                }
                conn.Close();
                conn.Dispose();
            }
            return reItem;
        }
        /// <summary>
        /// 新增資料(上市公司每月營業收入彙總表)
        /// </summary>
        /// <param name="Date">出表日期</param>
        /// <param name="YM">資料年月</param>
        /// <param name="Company_Code">公司代號</param>
        /// <param name="Company_Name">公司名稱</param>
        /// <param name="Industry">產業別</param>
        /// <param name="OI_TM">營業收入-當月營收</param>
        /// <param name="OI_LM">營業收入-上月營收</param>
        /// <param name="OI_TMLY">營業收入-去年當月營收</param>
        /// <param name="OI_LM_ID">營業收入-上月比較增減(%)</param>
        /// <param name="OI_TMLY_ID">營業收入-去年同月增減(%))</param>
        /// <param name="Diff_TM">累計營業收入-當月累計營收</param>
        /// <param name="Diff_LY">累計營業收入-去年累計營收</param>
        /// <param name="Diff_PC">累計營業收入-前期比較增減(%)</param>
        /// <param name="Remark">備註</param>
        /// <returns>執行後訊息</returns>
        public string Insert(string Date, string YM, string Company_Code, string Company_Name, string Industry, int OI_TM, int OI_LM, int OI_TMLY, double OI_LM_ID, double OI_TMLY_ID, int Diff_TM, int Diff_LY, double Diff_PC, string Remark)
        {
            string reMsg = "";
            List<SQL> reItem = new();
            SqlConnectionStringBuilder connStr = new()
            {
                DataSource = ".",
                InitialCatalog = "Yushan",
                UserID = "ys_test",
                Password = "test123"
            };
            using (SqlConnection conn = new(connStr.ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new("EXEC [Insert_OperatingIncome] @Date,@YM,@C_Code,@C_Name,@I_Name,@OI_TM,@OI_LM,@OI_TMLY,@OI_LM_ID,@OI_TMLY_ID,@Diff_TM,@Diff_LY,@Diff_PC,@Remark;", conn))
                {
                    cmd.Parameters.AddWithValue("@Date", Date);
                    cmd.Parameters.AddWithValue("@YM", YM);
                    cmd.Parameters.AddWithValue("@C_Code", Company_Code);
                    cmd.Parameters.AddWithValue("@C_Name", Company_Name);
                    cmd.Parameters.AddWithValue("@I_Name", Industry);
                    cmd.Parameters.AddWithValue("@OI_TM", OI_TM);
                    cmd.Parameters.AddWithValue("@OI_LM", OI_LM);
                    cmd.Parameters.AddWithValue("@OI_TMLY", OI_TMLY);
                    cmd.Parameters.AddWithValue("@OI_LM_ID", OI_LM_ID);
                    cmd.Parameters.AddWithValue("@OI_TMLY_ID", OI_TMLY_ID);
                    cmd.Parameters.AddWithValue("@Diff_TM", Diff_TM);
                    cmd.Parameters.AddWithValue("@Diff_LY", Diff_LY);
                    cmd.Parameters.AddWithValue("@Diff_PC", Diff_PC);
                    if(Remark.Equals("-"))
                        cmd.Parameters.AddWithValue("@Remark", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@Remark", Remark);
                    using (SqlDataReader rd =cmd.ExecuteReader())
                    {
                        if(rd.Read())
                        {
                            reMsg = rd["MSG"].ToString()!;
                        }
                        rd.Close();
                    }
                }
                conn.Close();
                conn.Dispose();
            }
            return reMsg;
        }
    }
}
