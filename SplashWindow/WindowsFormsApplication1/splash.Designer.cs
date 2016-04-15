namespace WindowsFormsApplication1
{
    partial class splash
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.progressbar1 = new MetroProgressBar.Progressbar();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(202, 208);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(190, 32);
            this.label1.TabIndex = 1;
            this.label1.Text = "正在初始化设备";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("微软雅黑 Light", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(160, 32);
            this.label2.TabIndex = 2;
            this.label2.Text = "FMSM点检仪";
            // 
            // progressbar1
            // 
            this.progressbar1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.progressbar1.BackColor = System.Drawing.Color.Transparent;
            this.progressbar1.Location = new System.Drawing.Point(143, 165);
            this.progressbar1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.progressbar1.Name = "progressbar1";
            this.progressbar1.Size = new System.Drawing.Size(323, 16);
            this.progressbar1.TabIndex = 0;
            // 
            // splash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::WindowsFormsApplication1.Properties.Resources.Splash;
            this.ClientSize = new System.Drawing.Size(598, 299);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressbar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "splash";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "splash";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroProgressBar.Progressbar progressbar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}