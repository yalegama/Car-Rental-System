using Guna.UI2.WinForms;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace WindowsFormsApp1
{
    public partial class ReturnForm : Form
    {
        public ReturnForm()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\nyalegama\OneDrive - Infor\Documents\CarRentalDB.mdf"";Integrated Security=True;Connect Timeout=30");
        private void populate()
        {
            Con.Open();
            string query = "select * from RentalTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            RentDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void populateReturn()
        {
            Con.Open();
            string query = "select * from ReturnTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            ReturnDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void DeleteReturn()
        {
            int rentId;
            rentId = Convert.ToInt32(RentDGV.SelectedRows[0].Cells[1].Value.ToString());
            Con.Open();
            string query = "delete from RentalTbl where RentId=" + IdTb.Text + ";";
            SqlCommand command = new SqlCommand(query, Con);
            command.ExecuteNonQuery();
           // MessageBox.Show("Rental Order Deleted Successfully");
            Con.Close();
            populate();
           // UpdateRentDelete();
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

        private void ReturnForm_Load(object sender, EventArgs e)
        {
            populate();
            populateReturn();
        }

        private void RentDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            IdTb.Text = RentDGV.SelectedRows[0].Cells[0].Value.ToString();
            CarIdTb.Text = RentDGV.SelectedRows[0].Cells[1].Value.ToString();
            CustomerNameTb.Text = RentDGV.SelectedRows[0].Cells[2].Value.ToString();
            ReturnDateTb.Text = RentDGV.SelectedRows[0].Cells[4].Value.ToString();
            DateTime d1 = ReturnDateTb.Value.Date;
            DateTime d2 = DateTime.Now;
            TimeSpan t = d2- d1;
            int NrOfDays = Convert.ToInt32(t.TotalDays);
            if(NrOfDays <= 0)
            {
                DelayTb.Text = "No Delay";
                FineTb.Text = "0";
            }
            else
            {
                DelayTb.Text = "" + NrOfDays;
                // Adding extra cgarges
                FineTb.Text = "" + (NrOfDays*1000);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(IdTb.Text == "" || CustomerNameTb.Text == "" || FineTb.Text == "" || DelayTb.Text=="")
            {
                MessageBox.Show("Missing Information. Please Check Again");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "insert into ReturnTbl values(" + IdTb.Text + ",'" + CarIdTb.Text + "','" + CustomerNameTb.Text + "', '" + ReturnDateTb.Text + "', '" + DelayTb.Text + "', '" + FineTb.Text + "')";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Car Dully Returned");
                    Con.Close();
                    //UpdateRent();
                    populateReturn();
                    DeleteReturn();
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
                    string query = "delete from RentalTbl where RentId=" + IdTb.Text + ";";
                    SqlCommand command = new SqlCommand(query, Con);
                    command.ExecuteNonQuery();
                    MessageBox.Show("User Deleted Successfully");
                    Con.Close();
                    populate();
                }
                catch (Exception MyExeption)
                {
                    MessageBox.Show($"{MyExeption.Message}");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (IdTb.Text == "")
            {
                MessageBox.Show("Please Check Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "update RentalTbl set ReturnDate='" + ReturnDateTb.Text + "', Delay='" + DelayTb.Text + "', Fine='" + FineTb.Text + "' where ReturnId=" + IdTb.Text + ";";
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
