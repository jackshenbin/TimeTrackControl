namespace demo
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
            this.timeTrackControl1 = new TimeTrackControl.TimeTrackControl();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // timeTrackControl1
            // 
            this.timeTrackControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.timeTrackControl1.CurrentTime = new System.DateTime(((long)(0)));
            this.timeTrackControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.timeTrackControl1.EditMode = 0;
            this.timeTrackControl1.EndTime = new System.DateTime(((long)(0)));
            this.timeTrackControl1.Location = new System.Drawing.Point(0, 0);
            this.timeTrackControl1.MinimumSize = new System.Drawing.Size(2, 47);
            this.timeTrackControl1.Name = "timeTrackControl1";
            this.timeTrackControl1.Size = new System.Drawing.Size(967, 60);
            this.timeTrackControl1.StartTime = new System.DateTime(((long)(0)));
            this.timeTrackControl1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(69, 141);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(967, 236);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.timeTrackControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private TimeTrackControl.TimeTrackControl timeTrackControl1;
        private System.Windows.Forms.Button button1;
    }
}

