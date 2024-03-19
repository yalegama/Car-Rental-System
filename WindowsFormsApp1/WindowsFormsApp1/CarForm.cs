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
    public partial class CarForm : Form
    {
        public CarForm()
        {
            InitializeComponent();
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

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

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
            if (Cregnum.Text == "" || Cbrand.Text == "" || Cmodel.Text == "" || Cprice.Text == "" || Cavailable.Text == "")
            {
                MessageBox.Show("Missing Information. Please Check Again");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "insert into CarTbl values('" + Cregnum.Text + "','" + Cbrand.Text + "', '" + Cmodel.Text + "','" + Cprice.Text + "','" + Cavailable.SelectedItem.ToString() + "')";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Car Details Successfully Added");
                    Con.Close();
                    populate();
                }
                catch (Exception MyException)
                {
                    MessageBox.Show(MyException.Message);
                }
            }
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Cregnum.Text = guna2DataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            Cbrand.Text = guna2DataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            Cmodel.Text = guna2DataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            Cprice.Text = guna2DataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            Cavailable.Text = guna2DataGridView1.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Cregnum.Text == "")
            {
                MessageBox.Show("Please Check Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    string query = "update CarTbl set Brand='" + Cbrand.Text + "', Model='" + Cmodel.Text+ "', Price='" + Cprice.Text+ "',Available='" + Cavailable.ToString()+ "' where RegNum=" + Cregnum.Text + ";";
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

        private void CarForm_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(Cregnum.Text == "")
            {
                MessageBox.Show("Please Enter Register Number and Click Delete");
            }else
            {
                try
                {
                    Con.Open();
                    string query = "delete from CarTbl where RegNum='" + Cregnum.Text + "';";
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Cbrand_TextChanged(object sender, EventArgs e)
        {

        }

        private void Cregnum_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Cmodel_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void Cprice_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void Cavailable_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
