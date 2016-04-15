using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            splash splash = new splash();
            var newThread = new Thread(frmNewFormThread);
            newThread.Start(splash);

            Thread.Sleep(8 * 1000);
            splash.Invoke(new Action(() =>
            {
                splash.Close();
            }));

            return;
        }

        private void frmNewFormThread(object splash)
        {
            Application.Run((splash)splash);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
