using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace TimeTrackControl
{
    public partial class TimeTrackControl : UserControl
    {
        public TimeTrackControl()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
        }

        #region 私有变量
        const int BaseLinePos = 24;

        private int m_offSet = 0;
        private DateTime m_currentTime = new DateTime();
        private int m_editMode = 0;
        private Brush m_backgroundColor = SystemBrushes.ControlDarkDark;
        private DateTime m_startTime = new DateTime();
        private DateTime m_endTime = new DateTime();
        private List<TimeTrackObject> m_timeObjects = new List<TimeTrackObject>();
        private TimeTrackObject m_selectedObject = null;
        private Point m_startMovePoint = new Point();
        private bool m_isMoveFlag = false;
        private DateTime m_dampStartTime = new DateTime();
        private Point m_dampStartPoint = new Point();
        private int BaseLineHeight { get { return this.Height; } }
        private Font TimeFont = new Font("宋体", 9);
        #endregion

        #region 共有变量
        public class TrackObjectDoubleClickEventArgs : EventArgs
        {
            public TimeTrackObject SelectedObject;
            public DateTime CurrentTime;
        }
        public event Action<TrackObjectDoubleClickEventArgs> OnDoubleClick;

        public DateTime CurrentTime
        {
            get { return m_currentTime; }
            set { m_currentTime = value; }
        }
        public int EditMode
        {
            get { return m_editMode; }
            set { m_editMode = value; }
        }
        public Brush BackgroundColor
        {
            get { return m_backgroundColor; }
            set { m_backgroundColor = value; }
        }
        public DateTime StartTime
        {
            get { return m_startTime; }
            set { m_startTime = value; }
        }
        public DateTime EndTime
        {
            get { return m_endTime; }
            set { m_endTime = value; }
        }
        public int TimeTrackWidth
        {
            get
            {
                return (int)m_endTime.Subtract(m_startTime).TotalMinutes;
            }
        }
        #endregion

        #region 共有方法
        public void Init(DateTime startTime, DateTime endTime)
        {
            m_startTime = startTime;
            m_endTime = endTime;
            m_currentTime = startTime;
            BackgroundColor = SystemBrushes.ControlDarkDark;
            m_selectedObject = null;

            Invalidate();
        }

        public void SetTimetrackObject(List<TimeTrackObject> objs)
        {
            m_timeObjects = objs;
            Invalidate();
        }

        public void MoveTime(int minute = -60)
        {
            if (minute > 0)
                MoveRight(Math.Abs(minute));
            else if (minute < 0)
                MoveLeft(Math.Abs(minute));
            else
                return;
        }
        public void MoveTime(DateTime date)
        {
            m_offSet = (int)m_startTime.Subtract(date).TotalMinutes;
            Invalidate();
        }
        #endregion

        #region 私有方法
        private void MoveRight(int minute = 60)
        {
            if (m_offSet + minute > 0)
            {
                m_offSet = 0;
                Invalidate();
                return;
            }
            m_offSet = m_offSet + minute;
            Invalidate();
        }

        private void MoveLeft(int minute = 60)
        {
            if (Math.Abs(m_offSet - minute) > TimeTrackWidth - this.Width)
            {
                m_offSet = this.Width - TimeTrackWidth;
                Invalidate();
                return;
            }
            m_offSet = m_offSet - minute;
            Invalidate();
        }

        private void DrawBG(Graphics tempgraphics)
        {
            tempgraphics.FillRectangle(BackgroundColor, 0, 0, TimeTrackWidth, BaseLineHeight);
        }

        private void DrawLine(Graphics tempgraphics)
        {
            if (m_startTime == m_endTime)
                return;

            int w = 0;
            bool breakFlag = false;
            do
            {
                DateTime t = m_startTime.AddMinutes(w);
                if (t.Minute % 6 == 0)
                    tempgraphics.DrawLine(SystemPens.ControlLight, w, BaseLinePos, w, BaseLinePos + 10);
                if (t.Minute == 30)
                    tempgraphics.DrawLine(SystemPens.ControlLight, w, BaseLinePos, w, BaseLinePos + 13);
                if (t.Minute == 0)
                {
                    tempgraphics.DrawLine(SystemPens.ControlLight, w, BaseLinePos, w, BaseLinePos + 16);
                    DrawTimeString(tempgraphics, m_startTime.AddMinutes(w).ToString("HH:mm:ss"), new Point(w, BaseLinePos));
                }
                if (t.Minute == 0 && t.Hour == 0)
                    DrawTimeString(tempgraphics, m_startTime.AddMinutes(w).ToString("yyyy-MM-dd"), new Point(w, BaseLinePos / 2));


                w++;
                if (m_startTime.AddMinutes(w).Ticks > m_endTime.Ticks)
                    breakFlag = true;
            } while (!breakFlag);
            tempgraphics.DrawLine(SystemPens.ControlLight, 0, BaseLinePos, --w, BaseLinePos);

        }

        private void DrawTimeString(Graphics tempgraphics, string str, Point p)
        {
            SizeF s = tempgraphics.MeasureString(str, TimeFont);
            tempgraphics.DrawString(str, TimeFont, SystemBrushes.ControlLight, new PointF(p.X - s.Width / 2, p.Y - s.Height));
        }

        private void DrawObjects(Graphics tempgraphics)
        {
            if (m_timeObjects.Count <= 0)
                return;
            RectangleF[] rects = new RectangleF[m_timeObjects.Count];
            for (int i = 0; i < m_timeObjects.Count; i++)
            {
                float x = (float)m_timeObjects[i].AppearTime.Subtract(m_startTime).TotalMinutes;
                float y = 0f;
                float w = (float)m_timeObjects[i].DisppearTime.Subtract(m_timeObjects[i].AppearTime).TotalMinutes;
                float h = BaseLineHeight;
                rects[i] = new RectangleF(x, y, w, h);
            }
            tempgraphics.FillRectangles(Brushes.SkyBlue, rects);
        }

        private void DrawCurrTimeLine(Graphics tempgraphics)
        {
            if (m_startTime == m_endTime)
                return;

            float x = (float)m_currentTime.Subtract(m_startTime).TotalMinutes;
            tempgraphics.DrawLine(Pens.LightGoldenrodYellow, x, 0, x, BaseLineHeight);
        }

        private void DrawSelectedObject(Graphics tempgraphics)
        {
            if (m_selectedObject == null)
                return;

            float x = (float)m_selectedObject.AppearTime.Subtract(m_startTime).TotalMinutes;
            float y = 0f;
            float w = (float)m_selectedObject.DisppearTime.Subtract(m_selectedObject.AppearTime).TotalMinutes;
            float h = BaseLineHeight;
            RectangleF rect = new RectangleF(x, y, w, h);

            tempgraphics.FillRectangle(Brushes.Blue, rect);
        }

        private void StartDamp(object dis)
        {
            System.Diagnostics.Trace.WriteLine(((double)dis).ToString("0.0000"));
            int temp = (int)((double)dis / 10);

            while (true)
            {
                if (Math.Abs(temp) < 10)
                    break;
                MoveTime(temp);
                temp = (int)(0.9 * temp);
                System.Threading.Thread.Sleep(40);
            }
        }


        #endregion

        #region 事件
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            MoveTime(e.Delta / 10);
            Invalidate();
            base.OnMouseWheel(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (TimeTrackWidth <= 0)
                return;
            Image img = new Bitmap(TimeTrackWidth, BaseLineHeight);
            Graphics tempgraphics = Graphics.FromImage(img);
            DrawBG(tempgraphics);
            DrawObjects(tempgraphics);
            DrawSelectedObject(tempgraphics);
            DrawLine(tempgraphics);
            DrawCurrTimeLine(tempgraphics);

            e.Graphics.DrawImageUnscaled(img, m_offSet, 0, this.Width, BaseLineHeight);
            img.Dispose();
            base.OnPaint(e);
        }

        private void TimeTrackControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (m_isMoveFlag)
            {
                MoveTime(e.Location.X - m_startMovePoint.X);
                m_startMovePoint = e.Location;
            }
            else
            {
                m_currentTime = m_startTime.AddMinutes(e.Location.X - m_offSet);
            }
            Invalidate();
        }

        private void TimeTrackControl_MouseClick(object sender, MouseEventArgs e)
        {
            m_selectedObject = null;
            for (int i = 0; i < m_timeObjects.Count; i++)
            {
                float x = (float)m_timeObjects[i].AppearTime.Subtract(m_startTime).TotalMinutes;
                float y = 0f;
                float w = (float)m_timeObjects[i].DisppearTime.Subtract(m_timeObjects[i].AppearTime).TotalMinutes;
                float h = BaseLineHeight;
                RectangleF rect = new RectangleF(x, y, w, h);
                if (rect.Contains(new PointF(e.Location.X - m_offSet, e.Location.Y)))
                {
                    m_selectedObject = m_timeObjects[i];
                    break;
                }
            }
            Invalidate();
        }

        private void TimeTrackControl_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (OnDoubleClick != null)
                OnDoubleClick(new TrackObjectDoubleClickEventArgs() { SelectedObject = m_selectedObject, CurrentTime = m_currentTime });
        }

        private void TimeTrackControl_MouseDown(object sender, MouseEventArgs e)
        {
            m_isMoveFlag = true;
            m_startMovePoint = e.Location;
            m_dampStartTime = DateTime.Now;
            m_dampStartPoint = e.Location;
        }

        private void TimeTrackControl_MouseUp(object sender, MouseEventArgs e)
        {
            m_isMoveFlag = false;
            m_startMovePoint = new Point();
            double dis = (e.Location.X - m_dampStartPoint.X) * Math.Abs(e.Location.X - m_dampStartPoint.X) * 0.6 / DateTime.Now.Subtract(m_dampStartTime).TotalMilliseconds;
            new System.Threading.Thread(StartDamp).Start(dis);
            m_dampStartTime = new DateTime();
            m_dampStartPoint = new Point();

        }

        private void TimeTrackControl_MouseLeave(object sender, EventArgs e)
        {
            m_isMoveFlag = false; m_startMovePoint = new Point();
            //double dis =  (Math.Pow(e.Location.X - m_dampStartPoint.X, 2) + Math.Pow(e.Location.Y - m_dampStartPoint.Y,2))
            //    / DateTime.Now.Subtract(m_dampStartTime).TotalMilliseconds ;
            //new System.Threading.Thread(StartDamp).Start(dis);
            m_dampStartTime = new DateTime();
            m_dampStartPoint = new Point();

        }

        #endregion
    }

    public class TimeTrackObject
    {
        //<MotionalObject>
        // <Item>
        // <ObjectTime>目标时间</ObjectTime>
        // <AppearTime>目标出现时间</AppearTime>
        // <DisppearTime>目标消失时间</DisppearTime>
        // </Item>
        // <Item>
        // </Item>
        //</MotionalObject>
        public object UserData { get; set; }
        public DateTime ObjectTime { get; set; }
        public DateTime AppearTime { get; set; }
        public DateTime DisppearTime { get; set; }
    }
}
