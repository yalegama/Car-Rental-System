using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class CustomerDashBoardForm : Form
    {
        public CustomerDashBoardForm()
        {
            InitializeComponent();
            UserNameLabel.Text = CustomerLoginForm.UserName;
        }

        private void CustomerDashBoardForm_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            CustomerCarForm customerCarForm = new CustomerCarForm();
            customerCarForm.Show();
        }

        private void guna2CirclePictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            CustomerRentalForm customerRentalForm = new CustomerRentalForm();
            customerRentalForm.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            UpdateCustomerDetailForm updateCustomerDetailForm = new UpdateCustomerDetailForm();
            updateCustomerDetailForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            CustomerLoginForm customerLoginForm = new CustomerLoginForm();
            customerLoginForm.Show();
        }
    }
}
