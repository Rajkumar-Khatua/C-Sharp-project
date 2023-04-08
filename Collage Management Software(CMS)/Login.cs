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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        // Let's Create The SqlDatabase Connection
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Raj\Documents\CollegeBD.mdf;Integrated Security=True;Connect Timeout=30");
        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            /* MasterForm Mform = new MasterForm();
             Mform.Show();
             this.Hide();*/
            MasterForm HOME = new MasterForm();
            Con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select count(*) from UserTbl where UserName='" + UserNameTb.Text + "' and password='" + UserPassTb.Text + "'", Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                HOME.Show();
                this.Hide();
                Con.Close();
            }
            else
            {
                MessageBox.Show("Wrong UserName or Password!!");
            }
            Con.Close();

            /*  DashBoard Db= new DashBoard();
              Db.Show();
              this.Hide();*/
        }

        private void guna2ControlBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void guna2GradientButton2_Click(object sender, EventArgs e)
        {
            // Go To register Page
            NewUserRegister NUR = new NewUserRegister();
            NUR.Show();
            this.Hide();
        }
    }
}
