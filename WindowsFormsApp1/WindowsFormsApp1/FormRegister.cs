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
using System.Data.SqlClient;

namespace WindowsFormsApp1
{
    public partial class FormRegister : Form
    {
        public FormRegister()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\nyalegama\OneDrive - Infor\Documents\CarRentalDB.mdf"";Integrated Security=True;Connect Timeout=30");

        private void FormRegister_Load(object sender, EventArgs e)
        {

        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            this.Hide();
            CustomerLoginForm frm = new CustomerLoginForm();  
            frm.Show();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            if (Cid.Text == "" || Cname.Text == "" || Caddress.Text == "" || Cphone.Text == "")
            {
                MessageBox.Show("Missing Information. Please Check Again");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "insert into CustomerTbl values('" + Cid.Text + "','" + Cname.Text + "', '" + Caddress.Text + "','" + Cphone.Text + "')";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Successfully Registerd");
                    Cid.Text = "";
                    Cname.Text = "";
                    Caddress.Text = "";
                    Cphone.Text = "";
                    Con.Close();
                   
                }
                catch (Exception MyException)
                {
                    MessageBox.Show(MyException.Message);
                }
            }
        }
    }
}
