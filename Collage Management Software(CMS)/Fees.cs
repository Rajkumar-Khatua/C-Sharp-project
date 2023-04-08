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

namespace Collage_Management_Software_CMS_
{
    public partial class Fees : Form
    {
        public Fees()
        {
            InitializeComponent();
        }
        // Let's Create The SqlDatabase Connection
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Raj\Documents\CollegeBD.mdf;Integrated Security=True;Connect Timeout=30");
        // Fill Department Combo Box
        private void fillDepartment()   // Create Function or Method For Connect The Department Name In Teachar Combo Box
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select StdId from StudentTbl", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("StdId", typeof(int));
            dt.Load(rdr);
            StdIdCB.ValueMember = "StdId";
            StdIdCB.DataSource = dt;
            Con.Close();
        }
        private void populate()
        {
            // Open The Connection For Database activing
            Con.Open();
            // Fetch Data From The UserTbl
            string query = "select * from FeesTbl";
            //Create The object For SqlDataAdapter and Sartround the DATA in sda Object
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            // Create The New Object For SqlCommandBuilder builder
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            // crete the ds variable 
            var ds = new DataSet();
            // Fill the DATA inti ds variable 
            sda.Fill(ds);
            // Insert data in DATAGRIDVIEW 
            FeesDGV.DataSource = ds.Tables[0];
            // Close the Connection
            Con.Close();
        }
        private void Fees_Load(object sender, EventArgs e)
        {
            fillDepartment();
            populate();
        }

        private void StdIdCB_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Con.Open();
            string query = "select * from StudentTbl where stdId=" + StdIdCB.SelectedValue.ToString() +"";
            SqlCommand cmd = new SqlCommand(query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach(DataRow dr in dt.Rows) 
            {
                StdName.Text = dr["StdName"].ToString();  
               
            }
            Con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Using Try And Catch For Better handelling The Exception
            try
            {
                // If Input Box are can Not Empty
                if (Num.Text == "" || StdName.Text == "" || StdPayAmou.Text == "")
                {
                    // If Inbput Box Is empty Then show the Message!!!!!
                    MessageBox.Show("Missing Information!");
                }
                else
                {
                    string date = StdTimeP.Value.Year.ToString();
                    Con.Open();
                    // Insert DATA Into DATABASE  
                    SqlCommand cmd = new SqlCommand("insert into FeesTbl values(" + Num.Text + "," + StdIdCB.SelectedValue.ToString() + ",'" + StdName + "','" +date+ "'," + StdPayAmou.Text + ")", Con);
                    // Execuce The Query By Object {cmd}
                    cmd.ExecuteNonQuery();
                    // Shoiw the Message That User has Been Sucessfully Added
                    MessageBox.Show("Fees Successfully Peyment");
                    // Close The DATABASE Connection
                    Con.Close();
                    // Select The DATA into Input Field
                    populate();
                }
            }
            catch (Exception ex)
            {
                // Handel All The Exception During The Process!!
                MessageBox.Show("Something Went Wrong"+ex);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MasterForm home = new MasterForm();
            home.Show();
            this.Hide();
        }

        private void guna2ControlBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
