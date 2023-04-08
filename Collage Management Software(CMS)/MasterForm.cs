using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Collage_Management_Software_CMS_
{
    public partial class MasterForm : Form
    {
        public MasterForm()
        {
            InitializeComponent();
        }

        // Go to Student Page
        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            Students std = new Students();
            std.Show();
            this.Hide();
        }

        // Go to Fees or Account Page
        private void guna2CircleButton5_Click(object sender, EventArgs e)
        {
            Account Acc = new Account();
            Acc.Show();
            this.Hide();
        }

        // Go to Teachar Page
        private void guna2CircleButton2_Click(object sender, EventArgs e)
        {
            Teachar Tech = new Teachar();
            Tech.Show();
            this.Hide();
        }

        // Go to Department Page
        private void guna2CircleButton4_Click(object sender, EventArgs e)
        {
            Department dep = new Department();
            dep.Show();
            this.Hide();
        }

        // Go to User page
        private void guna2CircleButton3_Click(object sender, EventArgs e)
        {
            UserForm user = new UserForm();
            user.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*Login login= new Login();
            login.Show();
            this.Hide();*/
        }

        private void MasterForm_Load(object sender, EventArgs e)
        {

        }
        // Go to Dash board
        private void guna2CircleButton6_Click(object sender, EventArgs e)
        {
            DashBoard Db = new DashBoard();
            Db.Show();
            this.Hide();
        }

        private void guna2ControlBox2_Click(object sender, EventArgs e)
        {
            // Shut Down The Application
            Application.Exit();
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            
        }

        // Go to Log-In Page
        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }
    }
}
