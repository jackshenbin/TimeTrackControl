using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace demo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timeTrackControl1.Init(DateTime.Now.AddDays(-5), DateTime.Now.AddDays(5));
            List<TimeTrackControl.TimeTrackObject> objs = new List<TimeTrackControl.TimeTrackObject>();
            objs.Add(new TimeTrackControl.TimeTrackObject(){ UserData = 1, AppearTime = DateTime.Now.AddHours(1.544), DisppearTime = DateTime.Now.AddHours(2.541)});
            timeTrackControl1.SetTimetrackObject(objs);
            timeTrackControl1.CurrentTime = DateTime.Now.AddHours(-5);
            timeTrackControl1.OnDoubleClick += timeTrackControl1_OnDoubleClick;
        }

        void timeTrackControl1_OnDoubleClick(TimeTrackControl.TimeTrackControl.TrackObjectDoubleClickEventArgs obj)
        {
            
        }
    }
}
