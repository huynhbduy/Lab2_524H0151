using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Xml.Linq;

namespace WindowsFormsApp2
{
    public partial class StudentsForm : Form
    {
        string conn = ConfigurationManager
            .ConnectionStrings["SchoolDBConnectionString"]
            .ConnectionString;

        public StudentsForm()
        {
            InitializeComponent();
        }

        // LOAD DATA
        void LoadData()
        {
            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();

                string sql = "SELECT * FROM tblStudents";

                SqlDataAdapter da = new SqlDataAdapter(sql, con);

                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvStudents.DataSource = dt;
            }
        }

        // FORM LOAD
        private void StudentsForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        // LOAD BUTTON
        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        // ADD
        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();

                string sql =
                    "INSERT INTO tblStudents(Name, Class) VALUES(@name,@class)";

                SqlCommand cmd = new SqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@class", txtClass.Text);

                cmd.ExecuteNonQuery();

                LoadData();
            }
        }

        // UPDATE
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();

                string sql =
                "UPDATE tblStudents SET Name=@name, Class=@class WHERE StudentID=@id";

                SqlCommand cmd = new SqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@id", txtID.Text);
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@class", txtClass.Text);

                cmd.ExecuteNonQuery();

                LoadData();
            }
        }


        // DELETE
        private void btnDelete_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();

                string sql =
            "DELETE FROM tblStudents WHERE StudentID=@id";

                SqlCommand cmd = new SqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@id", txtID.Text);

                cmd.ExecuteNonQuery();

                LoadData();

            }
        }

        // CLICK ROW
        private void dgvStudents_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtID.Text =
                    dgvStudents.Rows[e.RowIndex].Cells[0].Value.ToString();

                txtName.Text =
                    dgvStudents.Rows[e.RowIndex].Cells[1].Value.ToString();

                txtClass.Text =
                    dgvStudents.Rows[e.RowIndex].Cells[2].Value.ToString();
            }
        }


        private void btnEdit_Click(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();

                string sql =
                "SELECT * FROM tblStudents WHERE Name LIKE @name";

                SqlCommand cmd = new SqlCommand(sql, con);

                cmd.Parameters.AddWithValue("@name", "%" + txtSearch.Text + "%");

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvStudents.DataSource = dt;
            }
        }

    }
}
