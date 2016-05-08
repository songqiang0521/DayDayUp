using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 性能计数器
{
    public partial class Form1 : Form
    {

        private int moveCount ;
        private int countPerSecond;
        public Form1()
        {
            InitializeComponent();
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            perfMoveCount.RawValue = moveCount;
            perfMouseMove.RawValue = countPerSecond/100;
            countPerSecond = 0;
            moveCount = 0;

           

        }

        private void button1_Click(object sender, EventArgs e)
        {
            perfMoveCount.Increment();
            moveCount++;

        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            perfMouseMove.Increment();
            countPerSecond++;
        }
    }
}
