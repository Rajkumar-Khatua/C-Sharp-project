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

namespace Collage_Management_Software_CMS_
{
    public partial class Department : Form
    {
        public Department()
        {
            InitializeComponent();
        }
        // Let's Create The SqlDatabase Connection
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Raj\Documents\CollegeBD.mdf;Integrated Security=True;Connect Timeout=30");
        // craete The Function Or Method For Fetching The Information Form The database!!!!
        private void populate()
        {
            // Open The Connection For Database activing
            Con.Open();
            // Fetch Data From The UserTbl
            string query = "select * from DepartmentTbl";
            //Create The object For SqlDataAdapter and Sartround the DATA in sda Object
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            // Create The New Object For SqlCommandBuilder builder
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            // crete the ds variable 
            var ds = new DataSet();
            // Fill the DATA inti ds variable 
            sda.Fill(ds);
            // Insert data in DATAGRIDVIEW 
            DepDGV.DataSource = ds.Tables[0];
            // Close the Connection
            Con.Close();
        }
        // Add button code
        private void button1_Click(object sender, EventArgs e)
        {

            // Using Try And Catch For Better handelling The Exception
            try
            {
                // If Input Box are can Not Empty
                if (DepNameTb.Text == "" || DepDescTb.Text == "" || DepDurTb.Text == "")
                {
                    // If Inbput Box Is empty Then show the Message!!!!!
                    MessageBox.Show("Missing Information!");
                }
                else
                {
                    Con.Open();
                    // Insert DATA Into DATABASE  
                    SqlCommand cmd = new SqlCommand("insert into DepartmentTbl values('" + DepNameTb.Text + "','" + DepDescTb.Text + "','" + DepDurTb.Text + "')", Con);
                    // Execuce The Query By Object {cmd}
                    cmd.ExecuteNonQuery();
                    // Shoiw the Message That User has Been Sucessfully Added
                    MessageBox.Show("Department Added Successfully");
                    // Close The DATABASE Connection
                    Con.Close();
                    // Select The DATA into Input Field
                    populate();
                }
            }
            catch
            {
                // Handel All The Exception During The Process!!
                MessageBox.Show("Something Went Wrong");
            }
        }

        private void Department_Load(object sender, EventArgs e)
        {
            populate(); // call the populate method
        }

        // Delete button code
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (DepNameTb.Text == "")   // Department name can'be Null
                {
                    MessageBox.Show("Please Enter The Department Name");
                }
                else
                {
                    Con.Open();
                    string query = "delete  from DepartmentTbl where DepName='" + DepNameTb.Text + "';";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Department Deleted has Been successfully");
                    Con.Close();
                    populate();
                }
            }
            catch (Exception ex)
            {
                // Handel All The Exception During The Process!!
                MessageBox.Show("Oops... Sorry \n Something Went Wrong Department Not Deleted");
            }
        }

        private void DepDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // For Select Data In Input Box --> ID
            DepNameTb.Text = DepDGV.SelectedRows[0].Cells[0].Value.ToString();
            // For Select Data In Input Box --> Name
            DepDescTb.Text = DepDGV.SelectedRows[0].Cells[1].Value.ToString();
            // For Select Data In Input Box --> Pasword
            DepDurTb.Text = DepDGV.SelectedRows[0].Cells[2].Value.ToString();
        }

        // Edit Button Code
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // If Input Box are can Not Empty
                if (DepNameTb.Text == "" || DepDescTb.Text == "" || DepDurTb.Text == "")
                {
                    // If Inbput Box Is empty Then show the Message!!!!!
                    MessageBox.Show("Missing Data!");
                }
                else
                {
                    Con.Open();
                    // Update in Department Table
                    string query = "update DepartmentTbl set DepDesc='" + DepDescTb.Text + "',DepDuration=" + DepDurTb.Text + " where DepName='" + DepNameTb.Text + "';";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Department Updated Sucessfully :-)");
                    Con.Close();
                    populate();
                }
            }
            catch
            {

                // Handel All The Exception During The Process!!
                MessageBox.Show("Oops... Sorry \n Something Went Wrong Department Not Updated!!!");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // go to Home Page
            MasterForm home = new MasterForm();
            home.Show();
            this.Hide();
        }

        private void guna2ControlBox2_Click(object sender, EventArgs e)
        {
            // Close the application or shut down the application!
            Application.Exit();
        }
    }
}
