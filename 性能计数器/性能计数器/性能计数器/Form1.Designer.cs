namespace 性能计数器
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();

            this.timer1 = new System.Windows.Forms.Timer(this.components);

            this.perfMouseMove = new System.Diagnostics.PerformanceCounter();
            this.perfMoveCount = new System.Diagnostics.PerformanceCounter();

            ((System.ComponentModel.ISupportInitialize)(this.perfMouseMove)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.perfMoveCount)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(174, 104);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // perfMouseMove
            // 
            this.perfMouseMove.CategoryName = "SQCount";
            this.perfMouseMove.CounterName = "mouseMove";
            this.perfMouseMove.MachineName = "SQ-PC";
            this.perfMouseMove.ReadOnly = false;
            // 
            // perfMoveCount
            // 
            this.perfMoveCount.CategoryName = "SQCount";
            this.perfMoveCount.CounterName = "mouseMoveCount";
            this.perfMoveCount.MachineName = "SQ-PC";
            this.perfMoveCount.ReadOnly = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.perfMouseMove)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.perfMoveCount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer timer1;
        private System.Diagnostics.PerformanceCounter perfMouseMove;
        private System.Diagnostics.PerformanceCounter perfMoveCount;
    }
}

