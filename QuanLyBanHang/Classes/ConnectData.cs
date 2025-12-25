using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyBanHang.Classes
{
    internal class ConnectData
    {
        // ✅ Kết nối tới SQL Server của bạn
        // Thay "DESKTOP-VSQ93CP" bằng tên server trong SSMS của bạn (hiện bạn dùng chính tên đó)
        static string strConnect = "Data Source=DESKTOP-VSQ93CP;Initial Catalog=DuLieuC;Integrated Security=True;TrustServerCertificate=True";

        public SqlConnection sqlConn = null;

        // 🔹 Mở kết nối
        public void OpenConnect()
        {
            sqlConn = new SqlConnection(strConnect);
            if (sqlConn.State != ConnectionState.Open)
                sqlConn.Open();
        }

        // 🔹 Đóng kết nối
        public void CloseConnect()
        {
            if (sqlConn != null && sqlConn.State != ConnectionState.Closed)
            {
                sqlConn.Close();
                sqlConn.Dispose();
            }
        }

        // 🔹 Thực thi lệnh INSERT / UPDATE / DELETE
        public void UpdateData(string sql)
        {
            OpenConnect();
            using (SqlCommand sqlComm = new SqlCommand(sql, sqlConn))
            {
                sqlComm.ExecuteNonQuery();
            }
            CloseConnect();
        }

        // 🔹 Đọc dữ liệu, trả về DataTable
        public DataTable ReadData(string sqlSelect)
        {
            DataTable dt = new DataTable();
            OpenConnect();
            using (SqlDataAdapter sqldata = new SqlDataAdapter(sqlSelect, sqlConn))
            {
                sqldata.Fill(dt);
            }
            CloseConnect();
            return dt;
        }
        public bool KiemTraDangNhap(string username, string password)
        {
            try
            {
                OpenConnect();
                string sql = "SELECT COUNT(*) FROM tblUser WHERE UserName = @user AND Password = @pass";
                using (SqlCommand cmd = new SqlCommand(sql, sqlConn))
                {
                    cmd.Parameters.AddWithValue("@user", username);
                    cmd.Parameters.AddWithValue("@pass", password);

                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đăng nhập: " + ex.Message);
                return false;
            }
            finally
            {
                CloseConnect();
            }
        }

    }
}
