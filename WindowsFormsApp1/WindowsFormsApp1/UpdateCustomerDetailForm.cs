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
using System.Security.Cryptography;

namespace WindowsFormsApp1
{
    public partial class UpdateCustomerDetailForm : Form
    {
        public UpdateCustomerDetailForm()
        {
            InitializeComponent();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\nyalegama\OneDrive - Infor\Documents\CarRentalDB.mdf"";Integrated Security=True;Connect Timeout=30");

        private void fetchCustName()
        {
            Con.Open();
            string query = "SELECT * FROM CustomerTbl WHERE CustName='"+ CustomerLoginForm.UserName + "'";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read()) {
                /*long dataInt64 = reader.GetInt64(0);
                string dataAsString = dataInt64.ToString();*/
                string name = reader.GetString(1);
                Cname.Text = name;

                string address = reader.GetString(2);
                Caddress.Text = address;

                string phone = reader.GetString(3);
                Cphone.Text = phone;

            }
            Con.Close();
        }
        private void UpdateCustomerDetailForm_Load(object sender, EventArgs e)
        {
            fetchCustName();
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

        private void button2_Click(object sender, EventArgs e)
        {
            if(Cname == null || Caddress== null || Cphone== null) {

                MessageBox.Show("Please Enter Value");

            }
            else
            {
                try
                {
                    Con.Open();
                    /*string query = "update CustomerTbl set CustName='" + Cname.Text + "', CustAdd='" + Caddress.Text + "', Phone='" + Cphone.Text + "' where CustName=" + CustomerLoginForm.UserName + ";";*/
                    string query = "update CustomerTbl set CustName='" + Cname.Text + "', CustAdd='" + Caddress.Text + "', Phone='" + Cphone.Text.ToString() + "' where CustName=" + CustomerLoginForm.UserName + ";";
                    SqlCommand command = new SqlCommand(query, Con);
                    command.ExecuteNonQuery();
                    MessageBox.Show("User Successfully Updated");
                    Con.Close();
                    fetchCustName();
                }
                catch (Exception MyExeption)
                {
                    MessageBox.Show($"{MyExeption.Message}");
                }
            }
        }
    }
}
