using System.Data.SqlClient;

namespace VueTest
{
    public class SQL
    {
        public string Publication_Date { get; set; } = "";
        public string Publication_YM { get; set; } = "";
        public string Company_Code { get; set; } = "";
        public string Company_Name { get; set; } = "";
        public string Industry { get; set; } = "";
        public int OI_TM { get; set; }
        public int OI_LM { get; set; }
        public int OI_TMLY { get; set; }
        public double OI_LM_ID { get; set; }
        public double OI_TMLY_ID { get; set; }
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
                        double.TryParse(rd["OI_LM_ID"].ToString(), out double lmID);
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
    }
}
