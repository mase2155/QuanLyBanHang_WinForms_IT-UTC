using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyBanHang.Classes; // thêm namespace để dùng ConnectData

namespace QuanLyBanHang.Forms
{
    public partial class frmlogin : Form
    {
        ConnectData dtbase = new ConnectData();

        public frmlogin()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string tk = txtTenTK.Text.Trim();
            string mk = txtMK.Text.Trim();

            if (tk == "")
            {
                MessageBox.Show("Vui lòng nhập tên tài khoản!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenTK.Focus();
                return;
            }

            if (mk == "")
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMK.Focus();
                return;
            }

            // ✅ Gọi hàm kiểm tra đăng nhập từ ConnectData
            bool hopLe = dtbase.KiemTraDangNhap(tk, mk);

            if (hopLe)
            {
                MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                frmMain main = new frmMain();
                this.Hide(); // ẩn form login
                main.ShowDialog();
                this.Show(); // hiện lại sau khi đóng form chính
            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtMK_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
