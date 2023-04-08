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
namespace Collage_Management_Software_CMS_
{
    public partial class Teachar : Form
    {
        public Teachar()
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
            // Fetch Data From The TeacherTbl
            string query = "select * from TeacharTbl";
            //Create The object For SqlDataAdapter and Sartround the DATA in sda Object
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            // Create The New Object For SqlCommandBuilder builder
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            // crete the ds variable 
            var ds = new DataSet();
            // Fill the DATA inti ds variable 
            sda.Fill(ds);
            // Insert data in DATAGRIDVIEW 
            TeachDGV.DataSource = ds.Tables[0];
            // Close the Connection
            Con.Close();
        }
        // Fill Department Combo Box
        private void fillDepartment()   // Create Function or Method For Connect The Department Name In Teachar Combo Box
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select DepName from DepartmentTbl", Con);
            SqlDataReader rdr;
            rdr= cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("DepName",typeof(string));
            dt.Load(rdr);
            TdepCB.ValueMember = "DepName";
            TdepCB.DataSource = dt;

            Con.Close();
        }
        private void Teachar_Load(object sender, EventArgs e)
        {
            fillDepartment();   // call in main from
            populate(); // Call the populate method
        }

        // Add Button Code
        private void button1_Click(object sender, EventArgs e)
        {
            // Using Try And Catch For Better handelling The Exception
            try
            {
                // If Input Box are can Not Empty
                if (Tid.Text == "" || TName.Text == "" || Tph.Text=="" || Tadd.Text=="")
                {
                    // If Inbput Box Is empty Then show the Message!!!!!
                    MessageBox.Show("Missing Information!");
                }
                else
                {
                    Con.Open();
                    // Insert DATA Into DATABASE  
                    SqlCommand cmd = new SqlCommand("insert into TeacharTbl values(" + Tid.Text + ",'" + TName.Text + "','" + TGendrCB.SelectedItem.ToString() + "','" + TdOb.Text + "','" + Tph.Text + "','" + TdepCB.SelectedValue.ToString() + "','" +Tadd.Text+"')", Con);
                    // Execuce The Query By Object {cmd}
                    cmd.ExecuteNonQuery();
                    // Shoiw the Message That User has Been Sucessfully Added
                    MessageBox.Show("Teachar Added Successfully");
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

        private void TeachDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // For Select Data In Input Box --> ID
           Tid.Text = TeachDGV.SelectedRows[0].Cells[0].Value.ToString();
           
            TName.Text = TeachDGV.SelectedRows[0].Cells[1].Value.ToString();

            TGendrCB.SelectedItem = TeachDGV.SelectedRows[0].Cells[2].Value.ToString();

            Tph.Text = TeachDGV.SelectedRows[0].Cells[4].Value.ToString();

            Tadd.Text= TeachDGV.SelectedRows[0].Cells[6].Value.ToString();
        }

        // Delete Button code
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tid.Text == "") // Teachar Id Not be Empty
                {
                    MessageBox.Show("Please Enter Teachar ID");
                }
                else
                {
                    Con.Open();
                    string query = "delete  from TeacharTbl where Id='" + Tid.Text + "';";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Teachar Delete has Been successfully");
                    Con.Close();
                    populate();
                }
            }
            catch
            {
                // Handel All The Exception During The Process!!
                MessageBox.Show("Oops... Sorry \n Something Went Wrong Teachar Not Deleted");
            }
        }

        // Edit Button code
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // If Input Box are can Not Empty
                if (Tid.Text == "" || TName.Text == "" || Tadd.Text == "" || Tph.Text=="")
                {
                    // If Inbput Box Is empty Then show the Message!!!!!
                    MessageBox.Show("Missing Data!");
                }
                else
                {
                    Con.Open();
                    // Update from Teachar Table
                    string query = "update TeacharTbl set TeacharName='" + TName.Text + "',TeacharGender='" + TGendrCB.SelectedItem.ToString() + "',TeacharDOB='"+TdOb.Text+"',TeacharPhone='"+Tph.Text+"',TeacharDep='"+TdepCB.SelectedValue.ToString()+"',TeacharAdd='"+Tadd.Text+"' where Id='" + Tid.Text + "';";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Teachar Updated Sucessfully :-)");
                    Con.Close();
                    populate();
                }
            }
            catch
            {

                // Handel All The Exception During The Process!!
                MessageBox.Show("Oops... Sorry \n Something Went Wrong Teachar Not Updated!!!");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Go to home page
            MasterForm home = new MasterForm();
            home.Show();
            this.Hide();
        }

        private void guna2ControlBox2_Click(object sender, EventArgs e)
        {
            // Shut down the application
            Application.Exit();
        }
    }
}
