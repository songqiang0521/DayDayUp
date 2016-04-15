using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace winform_combobox
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < 10; i++)
            {
                comboBox1.Items.Add(i.ToString());
            }


        }

        private void comboBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            var g = e.Graphics;
            e.DrawBackground();
            g.DrawString(comboBox1.Items[e.Index].ToString(), new Font("Arial",20,FontStyle.Bold), Brushes.Black,e.Bounds);
            e.DrawFocusRectangle();
        }

        private void comboBox1_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = 50;

            comboBox1.DropDownHeight = comboBox1.Items.Count * e.ItemHeight + 10;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.Items.Count > 0)
            {
                comboBox1.Items.RemoveAt(0);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.Items.Count > 0)
            {
                comboBox1.Items.RemoveAt(comboBox1.Items.Count - 1);
            }
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;

            if (e.KeyCode == Keys.Delete)
            {
                int index = cb.SelectedIndex;
                if (index < 0 || index >= cb.Items.Count)
                {
                    return;
                }
                cb.Items.RemoveAt(index);
            }
        }
    }
}
