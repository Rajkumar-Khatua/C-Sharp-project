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
    public partial class DashBoard : Form
    {
        public DashBoard()
        {
            InitializeComponent();
        }
        // Let's Create The SqlDatabase Connection
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Raj\Documents\CollegeBD.mdf;Integrated Security=True;Connect Timeout=30");

        private void DashBoard_Load(object sender, EventArgs e)
        {
            Con.Open(); // Open the Connection
            SqlDataAdapter sda1 = new SqlDataAdapter("select count(*) from StudentTbl",Con);    // Fetch how many records have in Student  Table  (By Count Function * means  SQL query is the name of the column you want to retrieve for each record you are getting. You can obviously retrieve multiple columns for each record, and (only if you want to retrieve all the columns) you can replace the list of them with * , which means "all columns".) 
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            StudentCount.Text = dt1.Rows[0][0].ToString();


            // This Function is Close For certain poblem Total Amount Can see in Payment page Goto Payment page and see...
           /* SqlDataAdapter sda2 = new SqlDataAdapter("select count(*) from FeesTbl", Con);      // Fetch how many records have in Fees Table  (By Count Function * means  SQL query is the name of the column you want to retrieve for each record you are getting. You can obviously retrieve multiple columns for each record, and (only if you want to retrieve all the columns) you can replace the list of them with * , which means "all columns".) 
            DataTable dt2 = new DataTable();
            sda2.Fill(dt2);
            TotalFessCollected.Text = "Rs."+Convert.ToDouble(dt2.Rows[0][0].ToString())*25000;
           */


            SqlDataAdapter sda3 = new SqlDataAdapter("select count(*) from TeacharTbl", Con);    // Fetch how many records have in Teachar Table  (By Count Function * means  SQL query is the name of the column you want to retrieve for each record you are getting. You can obviously retrieve multiple columns for each record, and (only if you want to retrieve all the columns) you can replace the list of them with * , which means "all columns".) 
            DataTable dt3 = new DataTable();
            sda3.Fill(dt3);
            TotalTeachar.Text = dt3.Rows[0][0].ToString();


            SqlDataAdapter sda4 = new SqlDataAdapter("select count(*) from DepartmentTbl", Con);   // Fetch how many records have in Department Table  (By Count Function * means  SQL query is the name of the column you want to retrieve for each record you are getting. You can obviously retrieve multiple columns for each record, and (only if you want to retrieve all the columns) you can replace the list of them with * , which means "all columns".) 
            DataTable dt4 = new DataTable();
            sda4.Fill(dt4);
            TotalDep.Text = dt4.Rows[0][0].ToString();
            Con.Close();
        }

        // Go to Home page
        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            MasterForm home = new MasterForm();
            home.Show();
            this.Hide();
        }

        private void guna2ControlBox2_Click(object sender, EventArgs e)
        {
            // Shut down the application
            Application.Exit();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void guna2GradientPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TotalFessCollected_Click(object sender, EventArgs e)
        {

        }
    }
}
