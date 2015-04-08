namespace TimeTrackControl
{
    partial class TimeTrackControl
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // TimeTrackControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MinimumSize = new System.Drawing.Size(0, 47);
            this.Name = "TimeTrackControl";
            this.Size = new System.Drawing.Size(903, 60);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TimeTrackControl_MouseClick);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TimeTrackControl_MouseDoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TimeTrackControl_MouseDown);
            this.MouseLeave += new System.EventHandler(this.TimeTrackControl_MouseLeave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TimeTrackControl_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TimeTrackControl_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
