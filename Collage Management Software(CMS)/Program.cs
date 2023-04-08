using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Collage_Management_Software_CMS_
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
           // Application.Run(new UserForm());
          //  Application.Run(new Form1());
          //  Application.Run(new UserForm());
            /*Application.Run(new Form1());
            Application.Run(new Login());
            Application.Run(new DashBoard());*/


        }
    }
}
