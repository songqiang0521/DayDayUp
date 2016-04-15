namespace delete
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBoxPowerModel = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBoxPowerModel = new System.Windows.Forms.PictureBox();
            this.labelLoadRateStandard = new System.Windows.Forms.Label();
            this.buttonPowerTest = new System.Windows.Forms.Button();
            this.progressBarPowerTest = new System.Windows.Forms.ProgressBar();
            this.labelRippleVoltageStandard = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.labelFullVoltageStandard = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxSN = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.tbFullVoltageResult = new System.Windows.Forms.TextBox();
            this.tbRippleVoltageResult = new System.Windows.Forms.TextBox();
            this.tbLoadRateResult = new System.Windows.Forms.TextBox();
            this.tbFailedAlarm = new System.Windows.Forms.TextBox();
            this.tbFullVoltage = new System.Windows.Forms.TextBox();
            this.tbRippleVoltage = new System.Windows.Forms.TextBox();
            this.tbLoadRate = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.labelPowerTestRunState = new System.Windows.Forms.Label();
            this.buttonReturnToMainFrom = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.buttonStopPowerTest = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPowerModel)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxPowerModel
            // 
            this.comboBoxPowerModel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.comboBoxPowerModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPowerModel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBoxPowerModel.FormattingEnabled = true;
            this.comboBoxPowerModel.Items.AddRange(new object[] {
            "SM910",
            "FM910"});
            this.comboBoxPowerModel.Location = new System.Drawing.Point(195, 17);
            this.comboBoxPowerModel.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxPowerModel.Name = "comboBoxPowerModel";
            this.comboBoxPowerModel.Size = new System.Drawing.Size(183, 28);
            this.comboBoxPowerModel.TabIndex = 115;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(88, 21);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 20);
            this.label1.TabIndex = 117;
            this.label1.Text = "待测模块型号";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBoxPowerModel
            // 
            this.pictureBoxPowerModel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.pictureBoxPowerModel, 2);
            this.pictureBoxPowerModel.Location = new System.Drawing.Point(4, 67);
            this.pictureBoxPowerModel.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBoxPowerModel.Name = "pictureBoxPowerModel";
            this.tableLayoutPanel1.SetRowSpan(this.pictureBoxPowerModel, 5);
            this.pictureBoxPowerModel.Size = new System.Drawing.Size(374, 431);
            this.pictureBoxPowerModel.TabIndex = 134;
            this.pictureBoxPowerModel.TabStop = false;
            // 
            // labelLoadRateStandard
            // 
            this.labelLoadRateStandard.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelLoadRateStandard.AutoSize = true;
            this.labelLoadRateStandard.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelLoadRateStandard.Location = new System.Drawing.Point(653, 351);
            this.labelLoadRateStandard.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelLoadRateStandard.Name = "labelLoadRateStandard";
            this.labelLoadRateStandard.Size = new System.Drawing.Size(31, 20);
            this.labelLoadRateStandard.TabIndex = 146;
            this.labelLoadRateStandard.Text = "5%";
            // 
            // buttonPowerTest
            // 
            this.buttonPowerTest.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonPowerTest.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonPowerTest.Location = new System.Drawing.Point(393, 569);
            this.buttonPowerTest.Margin = new System.Windows.Forms.Padding(4);
            this.buttonPowerTest.Name = "buttonPowerTest";
            this.buttonPowerTest.Size = new System.Drawing.Size(169, 55);
            this.buttonPowerTest.TabIndex = 125;
            this.buttonPowerTest.Text = "开始测试";
            this.buttonPowerTest.UseVisualStyleBackColor = true;
            // 
            // progressBarPowerTest
            // 
            this.progressBarPowerTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.progressBarPowerTest, 4);
            this.progressBarPowerTest.Location = new System.Drawing.Point(386, 519);
            this.progressBarPowerTest.Margin = new System.Windows.Forms.Padding(4);
            this.progressBarPowerTest.Name = "progressBarPowerTest";
            this.progressBarPowerTest.Size = new System.Drawing.Size(589, 29);
            this.progressBarPowerTest.TabIndex = 131;
            // 
            // labelRippleVoltageStandard
            // 
            this.labelRippleVoltageStandard.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelRippleVoltageStandard.AutoSize = true;
            this.labelRippleVoltageStandard.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelRippleVoltageStandard.Location = new System.Drawing.Point(638, 257);
            this.labelRippleVoltageStandard.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelRippleVoltageStandard.Name = "labelRippleVoltageStandard";
            this.labelRippleVoltageStandard.Size = new System.Drawing.Size(60, 20);
            this.labelRippleVoltageStandard.TabIndex = 145;
            this.labelRippleVoltageStandard.Text = "250mV";
            // 
            // label21
            // 
            this.label21.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label21.Location = new System.Drawing.Point(422, 439);
            this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(110, 31);
            this.label21.TabIndex = 127;
            this.label21.Text = "故障报警";
            // 
            // labelFullVoltageStandard
            // 
            this.labelFullVoltageStandard.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelFullVoltageStandard.AutoSize = true;
            this.labelFullVoltageStandard.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelFullVoltageStandard.Location = new System.Drawing.Point(617, 163);
            this.labelFullVoltageStandard.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelFullVoltageStandard.Name = "labelFullVoltageStandard";
            this.labelFullVoltageStandard.Size = new System.Drawing.Size(102, 20);
            this.labelFullVoltageStandard.TabIndex = 144;
            this.labelFullVoltageStandard.Text = "22.5V~26.5V";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(625, 79);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 31);
            this.label5.TabIndex = 143;
            this.label5.Text = "标准值";
            // 
            // label14
            // 
            this.label14.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.Location = new System.Drawing.Point(434, 79);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(86, 31);
            this.label14.TabIndex = 128;
            this.label14.Text = "测试项";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(446, 157);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 31);
            this.label6.TabIndex = 123;
            this.label6.Text = "电压";
            // 
            // label20
            // 
            this.label20.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label20.Location = new System.Drawing.Point(422, 251);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(110, 31);
            this.label20.TabIndex = 126;
            this.label20.Text = "纹波电压";
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(410, 345);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(134, 31);
            this.label10.TabIndex = 124;
            this.label10.Text = "负载调整率";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(500, 21);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 20);
            this.label3.TabIndex = 119;
            this.label3.Text = "模块条码";
            // 
            // textBoxSN
            // 
            this.textBoxSN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSN.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxSN.Location = new System.Drawing.Point(577, 18);
            this.textBoxSN.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxSN.Name = "textBoxSN";
            this.textBoxSN.Size = new System.Drawing.Size(183, 27);
            this.textBoxSN.TabIndex = 121;
            this.textBoxSN.Text = "请输入模块条码";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(768, 24);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(15, 15);
            this.label7.TabIndex = 147;
            this.label7.Text = "*";
            // 
            // label16
            // 
            this.label16.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label16.Location = new System.Drawing.Point(1043, 79);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(62, 31);
            this.label16.TabIndex = 130;
            this.label16.Text = "结果";
            // 
            // tbFullVoltageResult
            // 
            this.tbFullVoltageResult.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbFullVoltageResult.Enabled = false;
            this.tbFullVoltageResult.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbFullVoltageResult.Location = new System.Drawing.Point(1016, 159);
            this.tbFullVoltageResult.Margin = new System.Windows.Forms.Padding(4);
            this.tbFullVoltageResult.Name = "tbFullVoltageResult";
            this.tbFullVoltageResult.Size = new System.Drawing.Size(117, 27);
            this.tbFullVoltageResult.TabIndex = 139;
            this.tbFullVoltageResult.Text = "合格";
            this.tbFullVoltageResult.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbRippleVoltageResult
            // 
            this.tbRippleVoltageResult.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbRippleVoltageResult.Enabled = false;
            this.tbRippleVoltageResult.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbRippleVoltageResult.Location = new System.Drawing.Point(1016, 253);
            this.tbRippleVoltageResult.Margin = new System.Windows.Forms.Padding(4);
            this.tbRippleVoltageResult.Name = "tbRippleVoltageResult";
            this.tbRippleVoltageResult.Size = new System.Drawing.Size(117, 27);
            this.tbRippleVoltageResult.TabIndex = 140;
            this.tbRippleVoltageResult.Text = "合格";
            this.tbRippleVoltageResult.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbLoadRateResult
            // 
            this.tbLoadRateResult.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbLoadRateResult.Enabled = false;
            this.tbLoadRateResult.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbLoadRateResult.Location = new System.Drawing.Point(1016, 347);
            this.tbLoadRateResult.Margin = new System.Windows.Forms.Padding(4);
            this.tbLoadRateResult.Name = "tbLoadRateResult";
            this.tbLoadRateResult.Size = new System.Drawing.Size(117, 27);
            this.tbLoadRateResult.TabIndex = 141;
            this.tbLoadRateResult.Text = "合格";
            this.tbLoadRateResult.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbFailedAlarm
            // 
            this.tbFailedAlarm.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbFailedAlarm.Enabled = false;
            this.tbFailedAlarm.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbFailedAlarm.Location = new System.Drawing.Point(1016, 441);
            this.tbFailedAlarm.Margin = new System.Windows.Forms.Padding(4);
            this.tbFailedAlarm.Name = "tbFailedAlarm";
            this.tbFailedAlarm.Size = new System.Drawing.Size(117, 27);
            this.tbFailedAlarm.TabIndex = 142;
            this.tbFailedAlarm.Text = "合格";
            this.tbFailedAlarm.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbFullVoltage
            // 
            this.tbFullVoltage.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbFullVoltage.Enabled = false;
            this.tbFullVoltage.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbFullVoltage.Location = new System.Drawing.Point(817, 159);
            this.tbFullVoltage.Margin = new System.Windows.Forms.Padding(4);
            this.tbFullVoltage.Name = "tbFullVoltage";
            this.tbFullVoltage.Size = new System.Drawing.Size(132, 27);
            this.tbFullVoltage.TabIndex = 136;
            this.tbFullVoltage.Text = "24.1V";
            // 
            // tbRippleVoltage
            // 
            this.tbRippleVoltage.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbRippleVoltage.Enabled = false;
            this.tbRippleVoltage.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbRippleVoltage.Location = new System.Drawing.Point(817, 253);
            this.tbRippleVoltage.Margin = new System.Windows.Forms.Padding(4);
            this.tbRippleVoltage.Name = "tbRippleVoltage";
            this.tbRippleVoltage.Size = new System.Drawing.Size(132, 27);
            this.tbRippleVoltage.TabIndex = 137;
            this.tbRippleVoltage.Text = "200mV";
            // 
            // tbLoadRate
            // 
            this.tbLoadRate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbLoadRate.Enabled = false;
            this.tbLoadRate.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbLoadRate.Location = new System.Drawing.Point(817, 347);
            this.tbLoadRate.Margin = new System.Windows.Forms.Padding(4);
            this.tbLoadRate.Name = "tbLoadRate";
            this.tbLoadRate.Size = new System.Drawing.Size(132, 27);
            this.tbLoadRate.TabIndex = 138;
            this.tbLoadRate.Text = "5%";
            // 
            // label15
            // 
            this.label15.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label15.Location = new System.Drawing.Point(840, 79);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(86, 31);
            this.label15.TabIndex = 129;
            this.label15.Text = "测量值";
            // 
            // labelPowerTestRunState
            // 
            this.labelPowerTestRunState.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelPowerTestRunState.AutoSize = true;
            this.labelPowerTestRunState.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelPowerTestRunState.Location = new System.Drawing.Point(983, 523);
            this.labelPowerTestRunState.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelPowerTestRunState.Name = "labelPowerTestRunState";
            this.labelPowerTestRunState.Size = new System.Drawing.Size(39, 20);
            this.labelPowerTestRunState.TabIndex = 132;
            this.labelPowerTestRunState.Text = "停止";
            // 
            // buttonReturnToMainFrom
            // 
            this.buttonReturnToMainFrom.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonReturnToMainFrom.Enabled = false;
            this.buttonReturnToMainFrom.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonReturnToMainFrom.Location = new System.Drawing.Point(799, 569);
            this.buttonReturnToMainFrom.Margin = new System.Windows.Forms.Padding(4);
            this.buttonReturnToMainFrom.Name = "buttonReturnToMainFrom";
            this.buttonReturnToMainFrom.Size = new System.Drawing.Size(169, 55);
            this.buttonReturnToMainFrom.TabIndex = 135;
            this.buttonReturnToMainFrom.Text = "返回主界面";
            this.buttonReturnToMainFrom.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(906, 21);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 20);
            this.label4.TabIndex = 79;
            this.label4.Text = "模块位置";
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox2.Location = new System.Drawing.Point(983, 18);
            this.textBox2.Margin = new System.Windows.Forms.Padding(4);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(183, 27);
            this.textBox2.TabIndex = 81;
            // 
            // buttonStopPowerTest
            // 
            this.buttonStopPowerTest.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanel1.SetColumnSpan(this.buttonStopPowerTest, 2);
            this.buttonStopPowerTest.Enabled = false;
            this.buttonStopPowerTest.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonStopPowerTest.Location = new System.Drawing.Point(596, 569);
            this.buttonStopPowerTest.Margin = new System.Windows.Forms.Padding(4);
            this.buttonStopPowerTest.Name = "buttonStopPowerTest";
            this.buttonStopPowerTest.Size = new System.Drawing.Size(169, 55);
            this.buttonStopPowerTest.TabIndex = 133;
            this.buttonStopPowerTest.Text = "停止测试";
            this.buttonStopPowerTest.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 8;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.98465F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.98466F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.98466F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.98466F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.046036F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.98466F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.98466F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.046036F));
            this.tableLayoutPanel1.Controls.Add(this.comboBoxPowerModel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pictureBoxPowerModel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelLoadRateStandard, 3, 4);
            this.tableLayoutPanel1.Controls.Add(this.buttonPowerTest, 2, 7);
            this.tableLayoutPanel1.Controls.Add(this.progressBarPowerTest, 2, 6);
            this.tableLayoutPanel1.Controls.Add(this.labelRippleVoltageStandard, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.label21, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.labelFullVoltageStandard, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.label5, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.label14, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label6, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.label20, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.label10, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.label3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label7, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.label16, 6, 1);
            this.tableLayoutPanel1.Controls.Add(this.tbFullVoltageResult, 6, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbRippleVoltageResult, 6, 3);
            this.tableLayoutPanel1.Controls.Add(this.tbLoadRateResult, 6, 4);
            this.tableLayoutPanel1.Controls.Add(this.tbFailedAlarm, 6, 5);
            this.tableLayoutPanel1.Controls.Add(this.tbFullVoltage, 5, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbRippleVoltage, 5, 3);
            this.tableLayoutPanel1.Controls.Add(this.tbLoadRate, 5, 4);
            this.tableLayoutPanel1.Controls.Add(this.label15, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelPowerTestRunState, 6, 6);
            this.tableLayoutPanel1.Controls.Add(this.label4, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBox2, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.label8, 7, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonStopPowerTest, 3, 7);
            this.tableLayoutPanel1.Controls.Add(this.buttonReturnToMainFrom, 5, 7);
            this.tableLayoutPanel1.Controls.Add(this.textBoxSN, 3, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 9;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.478672F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.478672F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.21801F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.21801F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.21801F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.21801F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.478672F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.478672F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.21327F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1195, 668);
            this.tableLayoutPanel1.TabIndex = 151;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(1174, 24);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(15, 15);
            this.label8.TabIndex = 114;
            this.label8.Text = "*";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1195, 668);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPowerModel)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxPowerModel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBoxPowerModel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label labelLoadRateStandard;
        private System.Windows.Forms.Button buttonPowerTest;
        private System.Windows.Forms.ProgressBar progressBarPowerTest;
        private System.Windows.Forms.Label labelRippleVoltageStandard;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label labelFullVoltageStandard;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxSN;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox tbFullVoltageResult;
        private System.Windows.Forms.TextBox tbRippleVoltageResult;
        private System.Windows.Forms.TextBox tbLoadRateResult;
        private System.Windows.Forms.TextBox tbFailedAlarm;
        private System.Windows.Forms.TextBox tbFullVoltage;
        private System.Windows.Forms.TextBox tbRippleVoltage;
        private System.Windows.Forms.TextBox tbLoadRate;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label labelPowerTestRunState;
        private System.Windows.Forms.Button buttonReturnToMainFrom;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button buttonStopPowerTest;
        private System.Windows.Forms.Label label8;
    }
}

