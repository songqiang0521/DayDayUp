using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mem_leaker
{
    public partial class Form1 : Form
    {

        List<byte[]> mems = new List<byte[]>(100);

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int count = 0;
            try
            {
                count = Convert.ToInt32(textBox1.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (count <= 0 || count >= 1024)
            {
                MessageBox.Show("请输入0-1024之间的数字");
            }

            byte[] bytes = new byte[count * 1024 * 1024];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = (byte)i;
            }
            mems.Add(bytes);
        }
    }
}
