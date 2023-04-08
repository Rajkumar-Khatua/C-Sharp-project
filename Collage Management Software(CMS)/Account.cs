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
    public partial class Account : Form
    {
        public Account()
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
            rdr= cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("StdId",typeof(int));
            dt.Load(rdr);
            StdIdCB.ValueMember = "StdId";
            StdIdCB.DataSource = dt;

            Con.Close();
        }
        // craete The Function Or Method For Fetching The Information Form The database!!!!
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
        private void updatestd()
        {
            Con.Open();
            string query = "update StudentTbl set StdFees='" + StdPayAmou.Text + "' where StdId=" + StdIdCB.SelectedValue.ToString() + ";";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.ExecuteNonQuery();
           // MessageBox.Show("User Updated Sucessfully :-)");
            Con.Close();
        }
        private void Account_Load(object sender, EventArgs e)
        {
            fillDepartment();
            populate();
        }

        private void StdIdCB_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Con.Open();
            string query = "select * from StudentTbl where StdId=" + StdIdCB.SelectedValue.ToString() + "";
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

        // Payment Button code..
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
                    SqlDataAdapter da = new SqlDataAdapter("select count(*) from FeesTbl where StdId=" + StdIdCB.SelectedValue.ToString() + " and Period='" + date + "'", Con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows[0][0].ToString() == "1")
                    {
                        MessageBox.Show("No Dues For The Selected Period");
                        Con.Close();
                    }
                    else
                    {
                       // Con.Open();
                       // string date = StdTimeP.Value.Year.ToString();
                        // Insert DATA Into DATABASE  
                        SqlCommand cmd = new SqlCommand("insert into FeesTbl values(" + Num.Text + ",'" + StdIdCB.SelectedValue.ToString() + "','" + StdName.Text + "','" + date + "'," + StdPayAmou.Text + ")", Con);
                        // Execuce The Query By Object {cmd}
                        cmd.ExecuteNonQuery();
                        // Shoiw the Message That User has Been Sucessfully Added
                        MessageBox.Show("Fees Added Successfully");
                        // Close The DATABASE Connection
                        Con.Close();
                        // Select The DATA into Input Field
                        populate();
                        updatestd();
                    }
                }
            }
            catch (Exception Ex)
            {
                // Handel All The Exception During The Process!!
                MessageBox.Show("Something Went Wrong" + Ex);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MasterForm home = new MasterForm();
            home.Show();
            this.Hide();
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }

        // For printing The Fees
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
           /* e.Graphics.DrawString("Fees Receipe Copy", new Font("Century", 25, FontStyle.Bold), Brushes.Red, new Point(230));
            e.Graphics.DrawString("Receipe Number: ", new Font("Century", 20, FontStyle.Bold), Brushes.BlueViolet, new Point(40,50));
            e.Graphics.DrawString(FeesDGV.SelectedRows[0].Cells[0].Value.ToString(), new Font("Century", 20, FontStyle.Bold), Brushes.Black, new Point(300, 50));
            e.Graphics.DrawString("Student UsN: ", new Font("Century", 20, FontStyle.Bold), Brushes.BlueViolet, new Point(40, 80));
            e.Graphics.DrawString(FeesDGV.SelectedRows[0].Cells[1].Value.ToString(), new Font("Century", 20, FontStyle.Bold), Brushes.Black, new Point(300, 80));
            e.Graphics.DrawString("Student Name: ", new Font("Century", 20, FontStyle.Bold), Brushes.BlueViolet, new Point(40, 110));
            e.Graphics.DrawString(FeesDGV.SelectedRows[0].Cells[2].Value.ToString(), new Font("Century", 20, FontStyle.Bold), Brushes.Black, new Point(300, 110));
            e.Graphics.DrawString("Time Period: ", new Font("Century", 20, FontStyle.Bold), Brushes.BlueViolet, new Point(40, 140));
            e.Graphics.DrawString(FeesDGV.SelectedRows[0].Cells[3].Value.ToString(), new Font("Century", 20, FontStyle.Bold), Brushes.Black, new Point(300, 140));
            e.Graphics.DrawString("Amount Rs. ", new Font("Century", 20, FontStyle.Bold), Brushes.BlueViolet, new Point(40, 170));
            e.Graphics.DrawString(FeesDGV.SelectedRows[0].Cells[4].Value.ToString(), new Font("Century", 20, FontStyle.Bold), Brushes.Black, new Point(300, 170));
            e.Graphics.DrawString("Royel Institute Of Engineering(RIE)", new Font("Century", 25, FontStyle.Bold), Brushes.Red, new Point(230,250));
           */
        }
        // For printing The Fees
        private void FeesDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            /* try
             {
                 if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                 {
                     printDocument1.Print();
                 }
                 else
                 {
                     MessageBox.Show("No Printers Are avilable for Print The Document");
                 }

             }catch(Exception Ex)
             {
                 MessageBox.Show("Something Went Wrong "+Ex);
             }
            */



            // For Select Data In Input Box --> ID
            Num.Text = FeesDGV.SelectedRows[0].Cells[0].Value.ToString();

            StdName.Text = FeesDGV.SelectedRows[0].Cells[2].Value.ToString();

            StdIdCB.SelectedValue = FeesDGV.SelectedRows[0].Cells[1].Value.ToString();

            StdPayAmou.Text = FeesDGV.SelectedRows[0].Cells[4].Value.ToString();

            //StdTimeP.SelectedValue = FeesDGV.SelectedRows[0].Cells[3].Value.ToString();


        }

        // Delete Button code
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // If Input Box are can Not Empty
                if (Num.Text == "" || StdName.Text == "" || StdPayAmou.Text == "")
                {
                    // If Inbput Box Is empty Then show the Message!!!!!
                    MessageBox.Show("Missing Data!");
                }
                else
                {
                    Con.Open();
                    string query = "update FeesTbl set StdId='" + StdIdCB.SelectedValue.ToString() + "',StdName='" + StdName.Text + "',Period='" + StdTimeP.Text + "',Amount='" + StdPayAmou.Text + "' where FeesNum='" + Num.Text + "';";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Payment Updated Sucessfully :-)");
                    Con.Close();
                    populate();
                }
            }
            catch(Exception ex)
            {

                // Handel All The Exception During The Process!!
                MessageBox.Show("Oops... Sorry \n Something Went Wrong Teachar Not Updated!!!"+ex);
            }
        }

        // Delete Button Code..
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (Num.Text == "")
                {
                    MessageBox.Show("Please Enter Num Of Fees ID");
                }
                else
                {
                    Con.Open();
                    string query = "delete  from FeesTbl where FeesNum='" + Num.Text + "';";
                    SqlCommand cmd = new SqlCommand(query, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Student's Fees Delete has Been successfully");
                    Con.Close();
                    populate();
                }
            }
            catch(Exception ex)
            {
                // Handel All The Exception During The Process!!
                MessageBox.Show("Oops... Sorry \n Something Went Wrong Teachar Not Deleted"+ex);
            }
        }

        private void guna2ControlBox2_Click(object sender, EventArgs e)
        {
            // Shut Down The applicaton
            Application.Exit();
        }

        private void guna2GradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            
            labtotal.Text= "0"; ;
            for(int i=0;i<FeesDGV.Rows.Count;i++)
            {
                labtotal.Text = Convert.ToString(double.Parse(labtotal.Text) + double.Parse(FeesDGV.Rows[i].Cells[4].Value.ToString()));
            }
        }

        private void labtotal_Click(object sender, EventArgs e)
        {
            DashBoard db = new DashBoard();
            
        }
    }
}
