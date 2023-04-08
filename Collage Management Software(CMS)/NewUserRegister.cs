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

namespace Collage_Management_Software_CMS_
{
    public partial class NewUserRegister : Form
    {
        public NewUserRegister()
        {
            InitializeComponent();
        }


        // Let's Create The SqlDatabase Connection
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Raj\Documents\CollegeBD.mdf;Integrated Security=True;Connect Timeout=30");

        private void guna2CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
           /* if (guna2CheckBox1.Checked == true)
            {
                UserPass.UseSystemPasswordChar = true;
            }
            else
            {
                UserPass.UseSystemPasswordChar = false;
            }*/
           
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            // Using Try And Catch For Better handelling The Exception
            try
            {
                // If Input Box are can Not Empty
                if (UserID.Text == "" || UserName.Text == "" || UserPass.Text == "")
                {
                    // If Inbput Box Is empty Then show the Message!!!!!
                    MessageBox.Show("Missing Information!");
                    
                }
                else
                {

                    Con.Open();
                    SqlCommand cmdToCheckUIDisExist = new SqlCommand("Select Convert(varchar(50),UserId) from UserTbl where Convert(varchar(50), UserId)='"+ UserID.Text + "'", Con);
                    string Uid = (string)cmdToCheckUIDisExist.ExecuteScalar();
                    Con.Close();
                    if (Uid == UserID.Text)
                    {
                        MessageBox.Show("User Id Is already Taken Please Select another One");
                    }
                    
                    else
                    {
                        Con.Open();
                       // Insert DATA Into DATABASE  
                        SqlCommand cmd = new SqlCommand("insert into UserTbl values(" + UserID.Text + ",'" + UserName.Text + "','" + UserPass.Text + "')", Con);
                        // Execuce The Query By Object {cmd}
                        cmd.ExecuteNonQuery();
                        // Shoiw the Message That User has Been Sucessfully Added
                        MessageBox.Show("Congratulations You Have Successfully registerd ❤\n Go to Login Page To Login -->");
                        MessageBox.Show("remembar Your User Name  And Password");
                        // Close The DATABASE Connection
                        Con.Close();
                        // Select The DATA into Input Field
                        // populate();
                    }
                }
            }
            catch(Exception ex) 
            {
                // Handel All The Exception During The Process!!
                MessageBox.Show("Something Went Wrong"+ex);
            }
        }

        private void guna2ControlBox2_Click(object sender, EventArgs e)
        {
            // Shut Down The Application
            Application.Exit();
        }

        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            // Go To login Page
            Login lg = new Login();
            lg.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void guna2ToggleSwitch1_CheckedChanged(object sender, EventArgs e)
        {
           UserPass.PasswordChar=default(char);
        }
    }
}
