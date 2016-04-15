using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace delete
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private bool FirstTime = true;

        private void Form1_SizeChanged(object sender, EventArgs e)
        {

            if (this.WindowState == FormWindowState.Normal)
            {
                Font normalLabelFont = new Font("微软雅黑", 9);
                label1.Font = normalLabelFont;
                comboBoxPowerModel.Font = normalLabelFont;
                label3.Font = normalLabelFont;
                textBoxSN.Font = normalLabelFont;
                label7.Font = normalLabelFont;
                label4.Font = normalLabelFont;
                textBox2.Font = normalLabelFont;

                labelFullVoltageStandard.Font = normalLabelFont;
                tbFullVoltage.Font = normalLabelFont;
                tbFullVoltageResult.Font = normalLabelFont;
                labelRippleVoltageStandard.Font = normalLabelFont;
                tbRippleVoltage.Font = normalLabelFont;
                tbRippleVoltageResult.Font = normalLabelFont;
                labelLoadRateStandard.Font = normalLabelFont;
                tbLoadRate.Font = normalLabelFont;
                tbLoadRateResult.Font = normalLabelFont;
                tbFailedAlarm.Font = normalLabelFont;
                labelPowerTestRunState.Font = normalLabelFont;
                buttonPowerTest.Font = normalLabelFont;
                buttonStopPowerTest.Font = normalLabelFont;
                buttonReturnToMainFrom.Font = normalLabelFont;


                Font testItemBoldFont = new Font("微软雅黑", 14.25f, FontStyle.Bold);
                label14.Font = testItemBoldFont;
                label5.Font = testItemBoldFont;
                label15.Font = testItemBoldFont;
                label16.Font = testItemBoldFont;


                Font testItemFont = new Font("微软雅黑", 14.25f);
                label6.Font = testItemFont;
                label20.Font = testItemFont;
                label10.Font = testItemFont;
                label21.Font = testItemFont;
            }
            else if (this.WindowState == FormWindowState.Maximized)
            {
                Font normalLabelFont = new Font("微软雅黑", 15);
                label1.Font = normalLabelFont;
                comboBoxPowerModel.Font = normalLabelFont;
                label3.Font = normalLabelFont;
                textBoxSN.Font = normalLabelFont;
                label7.Font = normalLabelFont;
                label4.Font = normalLabelFont;
                textBox2.Font = normalLabelFont;

                labelFullVoltageStandard.Font = normalLabelFont;
                tbFullVoltage.Font = normalLabelFont;
                tbFullVoltageResult.Font = normalLabelFont;
                labelRippleVoltageStandard.Font = normalLabelFont;
                tbRippleVoltage.Font = normalLabelFont;
                tbRippleVoltageResult.Font = normalLabelFont;
                labelLoadRateStandard.Font = normalLabelFont;
                tbLoadRate.Font = normalLabelFont;
                tbLoadRateResult.Font = normalLabelFont;
                tbFailedAlarm.Font = normalLabelFont;
                labelPowerTestRunState.Font = normalLabelFont;
                buttonPowerTest.Font = normalLabelFont;
                buttonStopPowerTest.Font = normalLabelFont;
                buttonReturnToMainFrom.Font = normalLabelFont;


                Font testItemBoldFont = new Font("微软雅黑", 22f, FontStyle.Bold);
                label14.Font = testItemBoldFont;
                label5.Font = testItemBoldFont;
                label15.Font = testItemBoldFont;
                label16.Font = testItemBoldFont;


                Font testItemFont = new Font("微软雅黑", 22);
                label6.Font = testItemFont;
                label20.Font = testItemFont;
                label10.Font = testItemFont;
                label21.Font = testItemFont;
            }


        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
