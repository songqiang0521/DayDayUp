using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Net;
using System.Threading;

namespace WindowsFormsApplication1
{
    public partial class FormFindIP : Form
    {
        IPAddress currentIPAddress;
        List<string> g_ips = new List<string>(4);

        public FormFindIP()
        {
            InitializeComponent();
            textBox1.Text = "192.168.0.1";
        }

        /// <summary>
        /// sync
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {

            string ips = "";

            Ping ping = new Ping();
            var index = textBox1.Text.LastIndexOf('.');
            string ip_front = textBox1.Text.Substring(0, index + 1);

            for (int i = 0; i < 254; i++)
            {
                string ip = ip_front + i.ToString();
                PingReply reply = ping.Send(ip, 100);
                var status = reply.Status;
                if (status == IPStatus.Success)
                {
                    ips += ip + "\n";

                }
            }

            if (ips != "")
            {
                MessageBox.Show("it is \n" + ips);

            }


            //IPAddress addr = IPAddress.Parse();


        }

        /// <summary>
        /// async
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {



        }


    }
}
