using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace WindowsFormsApp1
{
    public partial class CustomerCarForm : Form
    {
        public CustomerCarForm()
        {
            InitializeComponent();
            String UserName = CustomerLoginForm.UserName;
            CustomerNameTb.Text = UserName;
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\nyalegama\OneDrive - Infor\Documents\CarRentalDB.mdf"";Integrated Security=True;Connect Timeout=30");


        private void populate()
        {
            Con.Open();
            string query = "select * from CarTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            guna2DataGridView1.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void CustomerCarForm_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            CustomerDashBoardForm customerDashBoardForm = new CustomerDashBoardForm();  
            customerDashBoardForm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            {
                IdTb.Text = guna2DataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                CarRegCb.Text = guna2DataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                FeesTb.Text= guna2DataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (IdTb.Text == "" )
            {
                MessageBox.Show("Missing Information. Please Check Again");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "insert into RentalTbl values(" + IdTb.Text + ",'" + CarRegCb.Text + "','" + CustomerNameTb.Text + "', '" + RentalDateTb.Text + "','" + ReturnDateTb.Text + "', '" + FeesTb.Text + "')";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Car Successfully Rented");
                    Con.Close();
                    populate();
                }
                catch (Exception MyException)
                {
                    MessageBox.Show(MyException.Message);
                }
            }
        }
    }
}
