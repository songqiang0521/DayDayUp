using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security.Principal;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            listBox1.Items.Add("sq1");
            listBox1.Items.Add("sq2");
            listBox1.Items.Add("sq3");
            listBox1.Items.Add("sq4");
            listBox1.Items.Add("sq5");
            listBox1.Items.Add("sq6");
            listBox1.Items.Add("sq7");
        }

        private void textBox1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Link;
        }

        private void textBox1_DragDrop(object sender, DragEventArgs e)
        {
            textBox1.Text = ((Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            if (File.Exists(textBox1.Text))
            {
                using (StreamReader reader = new StreamReader(textBox1.Text, Encoding.Default))
                {
                    textBox2.Text = reader.ReadToEnd();
                }
            }
        }

        private void listBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                listBox1.DoDragDrop(listBox1.SelectedItem, DragDropEffects.Move);
            }

        }

        private void listBox2_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void listBox2_DragDrop(object sender, DragEventArgs e)
        {
            listBox1.Items.Remove(e.Data.GetData(DataFormats.Text));
            listBox2.Items.Add(e.Data.GetData(DataFormats.Text));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal pricopal = new WindowsPrincipal(identity);
            bool isAdmin = pricopal.IsInRole(WindowsBuiltInRole.Administrator);
            listBox1.Items.Add(isAdmin?"是管理员权限":"不是管理员权限");
            bool isUser = pricopal.IsInRole(WindowsBuiltInRole.User);
            listBox1.Items.Add(isUser ? "是用户权限" : "不是用户权限");
        }
    }
}
