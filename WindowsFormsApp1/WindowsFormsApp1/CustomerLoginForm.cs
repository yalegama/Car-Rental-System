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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Security.Cryptography;

namespace WindowsFormsApp1
{
    public partial class CustomerLoginForm : Form
    {
        public CustomerLoginForm()
        {
            InitializeComponent();
        }

        public static string UserName = "";
        public static string Phone = "";
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\nyalegama\OneDrive - Infor\Documents\CarRentalDB.mdf"";Integrated Security=True;Connect Timeout=30");

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if(Uname.Text== "" || PNum.Text == "")
            {
                MessageBox.Show("Please check fiealds");
            }
            else
            {
                string sql = "select count(*) from CustomerTbl where CustName='" + Uname.Text + "' and Phone='" + PNum.Text + "'";
                Con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(sql, Con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows[0][0].ToString() == "1")
                {
                    UserName = Uname.Text;
                    Phone = PNum.Text;
                    CustomerDashBoardForm customerDashBoardForm = new CustomerDashBoardForm();
                    customerDashBoardForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid Username or Password");
                }
                Con.Close();
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormRegister formRegister = new FormRegister();
            formRegister.Show();
        }
    }
}
