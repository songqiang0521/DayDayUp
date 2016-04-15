using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 日志查看
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();




        }

        private void eventLog1_EntryWritten(object sender, System.Diagnostics.EntryWrittenEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0} {1} {2}",
                e.Entry.TimeGenerated.ToShortTimeString(),
                e.Entry.Source,
                e.Entry.Message);
            listBox1.Items.Add(sb.ToString());
        }
    }
}
