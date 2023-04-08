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
using System.Security.Cryptography;

namespace Collage_Management_Software_CMS_
{
    public partial class Students : Form
    {
        public Students()
        {
            InitializeComponent();
        }
        // Let's Create The SqlDatabase Connection
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Raj\Documents\CollegeBD.mdf;Integrated Security=True;Connect Timeout=30");

        // Fill Department Combo Box
        private void fillDepartment()   // Create Function or Method For Connect The Department Name In Teachar Combo Box
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select DepName from DepartmentTbl", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("DepName", typeof(string));
            dt.Load(rdr);
            StuDepCB.ValueMember = "DepName";
            StuDepCB.DataSource = dt;

            Con.Close();
        }
        // craete The Function Or Method For Fetching The Information Form The database!!!!
        private void populate()
        {
            // Open The Connection For Database activing
            Con.Open();
            // Fetch Data From The UserTbl
            string query = "select * from StudentTbl";
            //Create The object For SqlDataAdapter and Sartround the DATA in sda Object
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            // Create The New Object For SqlCommandBuilder builder
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            // crete the ds variable 
            var ds = new DataSet();
            // Fill the DATA inti ds variable 
            sda.Fill(ds);
            // Insert data in DATAGRIDVIEW 
            StuDGV.DataSource = ds.Tables[0];
            // Close the Connection
            Con.Close();
        }

        // craete The Function Or Method For Fetching The Information Form The database!!!!
        private void Noduelist()
        {
            // Open The Connection For Database activing
            Con.Open();
            // Fetch Data From The UserTbl
            string query = "select * from StudentTbl where StdFees >'"+0+"'";
            //Create The object For SqlDataAdapter and Sartround the DATA in sda Object
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            // Create The New Object For SqlCommandBuilder builder
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            // crete the ds variable 
            var ds = new DataSet();
            // Fill the DATA inti ds variable 
            sda.Fill(ds);
            // Insert data in DATAGRIDVIEW 
            StuDGV.DataSource = ds.Tables[0];
            // Close the Connection
            Con.Close();
        }



        private void label2_Click(object sender, EventArgs e)
        {

        }

        // Add DATA or Information into DATABASE
        private void button1_Click(object sender, EventArgs e)
        {
            // Using Try And Catch For Better handelling The Exception
            try
            {
                // If Input Box are can Not Empty
                if (StuId.Text == "" || StuName.Text == "" || StuPh.Text == "" || StuFess.Text == "")
                {
                    // If Inbput Box Is empty Then show the Message!!!!!
                    MessageBox.Show("Missing Information!");
                }
                else
                {
                    Con.Open();
                    // Insert DATA Into DATABASE  
                    SqlCommand cmd = new SqlCommand("insert into StudentTbl values(" + StuId.Text + ",'" + StuName.Text + "','" + StuGen.SelectedItem.ToString() + "','" + StuDoB.Text + "','" + StuPh.Text + "','" + StuDepCB.SelectedValue.ToString() + "','" + StuFess.Text + "')", Con);
                    // Execuce The Query By Object {cmd}
                    cmd.ExecuteNonQuery();
                    // Shoiw the Message That User has Been Sucessfully Added
                    MessageBox.Show("Student Added Successfully");
                    // Close The DATABASE Connection
                    Con.Close();
                    // Select The DATA into Input Field
                    populate(); // Call the populate Method to fetch the whole DATABASE
                }
            }
            catch (Exception Ex)
            {
                // Handel All The Exception During The Process!!
                MessageBox.Show("Something Went Wrong"+Ex);
            }
        }

        private void Students_Load(object sender, EventArgs e)
        {
            populate(); // Call in Main Function 
            fillDepartment();   // Call in Main Function 
        }

        private void StuDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            // For Select Data In Input Box --> To Edit Or Delete the items
            StuId.Text = StuDGV.SelectedRows[0].Cells[0].Value.ToString();

            StuName.Text = StuDGV.SelectedRows[0].Cells[1].Value.ToString();

            StuGen.SelectedItem = StuDGV.SelectedRows[0].Cells[2].Value.ToString();

            StuPh.Text = StuDGV.SelectedRows[0].Cells[4].Value.ToString();

            StuFess.Text = StuDGV.SelectedRows[0].Cells[6].Value.ToString();
        }

        // Delete The Student record
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (StuId.Text == "")
                {
                    MessageBox.Show("Please Enter Student ID");
                }
                else
                {
                    Con.Open(); // Open the Connection
                    string query = "delete  from StudentTbl where StdId=" + StuId.Text + ";";   // Delete from student Table
                    SqlCommand cmd = new SqlCommand(query, Con);    // Sql Command
                    cmd.ExecuteNonQuery();   // Sql Command
                    MessageBox.Show("Student Deleted has Been successfully");
                    Con.Close();    // Close The Connection
                    populate();
                }
            }
            catch
            {
                // Handel All The Exception During The Process!!
                MessageBox.Show("Oops... Sorry \n Something Went Wrong Student Not Deleted");
            }
        }

        // Edit The Student Details
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // If Input Box are can Not Empty
                if (StuId.Text == "" || StuName.Text == "" || StuName.Text == "" || StuFess.Text == "")
                {
                    // If Inbput Box Is empty Then show the Message!!!!!
                    MessageBox.Show("Missing field!");
                }
                else
                {
                    Con.Open();
                    // Update in Student Table
                    string query = "update StudentTbl set StdName='" + StuName.Text + "',StdGender='" + StuGen.SelectedItem.ToString() + "',StdDOB='" + StuDoB.Text + "',StdPhone='" + StuPh.Text + "',StdDep='" + StuDepCB.SelectedValue.ToString() + "',StdFees='" + StuFess.Text + "' where StdId='" + StuId.Text + "';";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Student Updated Sucessfully :-)");
                    Con.Close();
                    populate();
                }
            }
            catch(Exception ex) 
            {

                // Handel All The Exception During The Process!!
                MessageBox.Show("Oops... Sorry \n Something Went Wrong Student Not Updated!!!"+ex);
            }
        }

        // Go to Home Page
        private void button4_Click(object sender, EventArgs e)
        {
            MasterForm home = new MasterForm();
            home.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            populate();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Noduelist();
        }

        private void guna2ControlBox2_Click(object sender, EventArgs e)
        {
            // Shut down the application
            Application.Exit();
        }
    }
}
