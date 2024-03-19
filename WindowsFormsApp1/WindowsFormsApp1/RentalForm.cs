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
using System.Net.NetworkInformation;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;
using System.Security.Cryptography;

namespace WindowsFormsApp1
{
    public partial class RentalForm : Form
    {
        public RentalForm()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\nyalegama\OneDrive - Infor\Documents\CarRentalDB.mdf"";Integrated Security=True;Connect Timeout=30");

        private void fillCombo()
        {
            Con.Open();
            string query = "select RegNum from CarTbl where Available='"+"Yes"+"'";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("RegNum", typeof(string));
            dt.Load(rdr);
            CarRegCb.ValueMember = "RegNum";
            CarRegCb.DataSource = dt;
            Con.Close();
        }

        private void fillCustomer()
        {
            Con.Open();
            string query = "select CustId from CustomerTbl";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CustId", typeof(string));
            dt.Load(rdr);
            CustCb.ValueMember = "CustId";
            CustCb.DataSource = dt;
            Con.Close();
        }

        private void fetchCustName()
        {
            Con.Open();
            string query = "select * from CustomerTbl where CustId="+CustCb.Text+"";
            SqlCommand cmd = new SqlCommand(query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                CustomerNameTb.Text = dr["CustName"].ToString();
            }
            Con.Close() ;
        }

        private void populate()
        {
            Con.Open();
            string query = "select * from RentalTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            guna2DataGridView1.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void UpdateRent()
        {
            Con.Open();
            string query = "update CarTbl set Available ='"+"No"+"' where RegNum='"+ CarRegCb.SelectedValue.ToString() +"' ";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.ExecuteNonQuery();
            Con.Close();
        }

        private void UpdateRentDelete()
        {
            Con.Open();
            string query = "update CarTbl set Available ='" + "Yes" + "' where RegNum='" + CarRegCb.SelectedValue.ToString() + "' ";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.ExecuteNonQuery();
            // MessageBox.Show("Car Details Successfully Added");
            Con.Close();
        }

        private void RentalForm_Load(object sender, EventArgs e)
        {
            fillCombo();
            fillCustomer();
            populate();
        }

        private void CarRegCb_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }

        private void CustCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            fetchCustName();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm mainForm = new MainForm();
            mainForm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // if text fields are empty, then show message
            if (IdTb.Text == "" || CustomerNameTb.Text == "" || FeesTb.Text == "")
            {
                MessageBox.Show("Missing Information. Please Check Again");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "insert into RentalTbl values(" + IdTb.Text + ",'" + CarRegCb.SelectedValue.ToString() + "','" + CustomerNameTb.Text + "', '" + RentalDateTb.Text + "','" + ReturnDateTb.Text + "', '" + FeesTb.Text + "')";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Car Successfully Rented");
                    Con.Close();
                    UpdateRent();
                    populate();
                }
                catch (Exception MyException)
                {
                    MessageBox.Show(MyException.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (IdTb.Text == "")
            {
                MessageBox.Show("Please Enter ID Number and Click Delete");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "delete from RentalTbl where RentId='" + IdTb.Text + "';";
                    SqlCommand command = new SqlCommand(query, Con);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Rental Order Deleted Successfully");
                    Con.Close();
                    populate();
                }
                catch (Exception MyExeption)
                {
                    MessageBox.Show($"{MyExeption.Message}");
                }
            }
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            IdTb.Text = guna2DataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            CarRegCb.SelectedValue = guna2DataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            CustomerNameTb.Text = guna2DataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            FeesTb.Text = guna2DataGridView1.SelectedRows[0].Cells[5].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Checking field is empty
            if (IdTb.Text == "")
            {
                MessageBox.Show("Please Check Information");
            }
            else
            {
                try
                {
                    Con.Open();
                 //   string query = "update RentalTbl set RentDate='" + RentalDateTb.Text + "', ReturnDate='" + ReturnDateTb.Text + "', RentFee='" + FeesTb.Text + "' where RentId=" + IdTb.Text + ";";
                    string query = "update RentalTbl set RentDate='" + RentalDateTb.Text + "', ReturnDate='" + ReturnDateTb.Text + "', RentFee='" + FeesTb.Text + "' where RentId=" + IdTb.Text + ";";
                    SqlCommand command = new SqlCommand(query, Con);
                    command.ExecuteNonQuery();
                    MessageBox.Show("User Successfully Updated");
                    Con.Close();
                    populate();
                }
                catch (Exception MyExeption)
                {
                    MessageBox.Show($"{MyExeption.Message}");
                }
            }
        }
    }
}
