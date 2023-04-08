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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Start The Progress Bar
        int StartProgressBar = 0;
        int CircleStartProgressBar = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            // Start The Progress Bar With 1
            StartProgressBar += 1;
            CircleStartProgressBar += 1;
            // Assign The Value With The Variable StartProgressBar
            MyProgeessBar.Value = StartProgressBar; 
            CirclePro.Value= CircleStartProgressBar;
            if(MyProgeessBar.Value == 100)
            {
                MyProgeessBar.Value = 0;
                //Stop The Timer
                timer1.Stop();

                // This is For Loading Login Page..
                Login login= new Login();   // Create Object For Loading page
                login.Show();   // Show the login form
                this.Hide();    // Hide this Splash Form or This Form..
            }
            if (CirclePro.Value == 100)
            {
                CirclePro.Value = 0;
                //Stop The Timer
                timer1.Stop();

                // This is For Loading Login Page..
                Login login = new Login();   // Create Object For Loading page
                login.Show();   // Show the login form
                this.Hide();    // Hide this Splash Form or This Form..
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Start The Timer
            timer1.Start();
        }

        //int StartCirclePro=0;
        private void guna2CircleProgressBar1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
