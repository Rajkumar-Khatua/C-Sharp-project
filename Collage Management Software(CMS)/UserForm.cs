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
    public partial class UserForm : Form
    {
        public UserForm()
        {
            InitializeComponent();
        }
        // Let's Create The SqlDatabase Connection
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Raj\Documents\CollegeBD.mdf;Integrated Security=True;Connect Timeout=30");
        private void UserForm_Load(object sender, EventArgs e)
        {
            // Execute The populated Functiuon!!!
            populate();
        }
        // craete The Function Or Method For Fetching The Information Form The database!!!!
        private void populate()
        {
            // Open The Connection For Database activing
            Con.Open();
            // Fetch Data From The UserTbl
            string query = "select * from UserTbl";
            //Create The object For SqlDataAdapter and Sartround the DATA in sda Object
            SqlDataAdapter sda = new SqlDataAdapter(query,Con);
            // Create The New Object For SqlCommandBuilder builder
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            // crete the ds variable 
            var ds = new DataSet();
            // Fill the DATA inti ds variable 
            sda.Fill(ds);
            // Insert data in DATAGRIDVIEW 
            UserDGV.DataSource = ds.Tables[0];
            // Close the Connection
            Con.Close();
        }

        //Add Button Code..
        private void button1_Click(object sender, EventArgs e)
        {
            // Using Try And Catch For Better handelling The Exception
            try
            {
                // If Input Box are can Not Empty
                if (UIdTb.Text=="" || UnameTb.Text=="" || UpassTb.Text=="")
                {
                    // If Inbput Box Is empty Then show the Message!!!!!
                    MessageBox.Show("Missing Information!");
                }
                else 
                {
                    Con.Open();
                    // Insert DATA Into DATABASE  
                    SqlCommand cmd = new SqlCommand("insert into UserTbl values(" + UIdTb.Text + ",'" + UnameTb.Text + "','" + UpassTb.Text + "')", Con);
                    // Execuce The Query By Object {cmd}
                    cmd.ExecuteNonQuery();
                    // Shoiw the Message That User has Been Sucessfully Added
                    MessageBox.Show("User Added Successfully");
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

        private void UserDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            // For Select Data In Input Box --> ID
            UIdTb.Text = UserDGV.SelectedRows[0].Cells[0].Value.ToString();
            // For Select Data In Input Box --> Name
            UnameTb.Text = UserDGV.SelectedRows[0].Cells[1].Value.ToString();
            // For Select Data In Input Box --> Pasword
            UpassTb.Text = UserDGV.SelectedRows[0].Cells[2].Value.ToString();
        }
        // Delete Button code..
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if(UIdTb.Text == "")
                {
                    MessageBox.Show("Please Enter The User Id");
                }
                else
                {
                    Con.Open();
                    // delete from user table
                    string query = "delete  from UserTbl where UserId=" + UIdTb.Text + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Deleted has Been successfully");
                    Con.Close();
                    populate();
                }
            }
            catch(Exception ex) 
            {
                // Handel All The Exception During The Process!!
                MessageBox.Show("Oops... Sorry \n Something Went Wrong User Not Deleted");
            }
        }

        // Edit Button
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // If Input Box are can Not Empty
                if (UIdTb.Text == "" || UnameTb.Text == "" || UpassTb.Text == "")
                {
                    // If Inbput Box Is empty Then show the Message!!!!!
                    MessageBox.Show("Missing Data!");
                }
                else
                {
                    Con.Open();
                    // Update in User Table
                    string query = "update UserTbl set UserName='" + UnameTb.Text + "',password='" + UpassTb.Text + "' where UserId=" + UIdTb.Text + ";";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Updated Sucessfully :-)");
                    Con.Close();
                    populate();
                }
            }
            catch(Exception ex)
            {

                // Handel All The Exception During The Process!!
                MessageBox.Show("Oops... Sorry \n Something Went Wrong User Not Updated!!!"+ex);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Go to Home Page
            MasterForm home = new MasterForm();
            home.Show();
            this.Hide();
        }

        private void guna2ControlBox2_Click(object sender, EventArgs e)
        {
            // Shut down the application
            Application.Exit();
        }

        private void guna2ControlBox2_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
