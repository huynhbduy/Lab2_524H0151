using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration; 


namespace WindowsFormsApp2
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string conn =
              ConfigurationManager.ConnectionStrings["SchoolDBConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();

                string sql = "SELECT COUNT(*) FROM tblUsers WHERE Username=@u AND Password=@p";

                SqlCommand cmd = new SqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@u", txtUsername.Text);
                cmd.Parameters.AddWithValue("@p", txtPassword.Text);

                int kq = (int)cmd.ExecuteScalar();

                if (kq == 1)
                {
                    this.Hide();
                    MainForm f = new MainForm();
                    f.Show();
                }
                else
                {
                    MessageBox.Show("Sai tài khoản!");
                }
            }
        }
    }
}
