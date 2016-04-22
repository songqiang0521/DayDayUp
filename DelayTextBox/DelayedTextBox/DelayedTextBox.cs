using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DelayedTextBoxControl
{
    public class DelayedTextBox : TextBox
    {
        private int delayWhenTextChaned;
        [Description("触发文本变化前等待的时间,单位是毫秒"),Category("UI")]
        public int DelayWhenTextChaned
        {
            get { return delayWhenTextChaned; }
            set { delayWhenTextChaned = value; }
        }

        private System.Timers.Timer _timer;
        private bool _timeElapsed;

        public DelayedTextBox()
        {
            if (delayWhenTextChaned==0)
            {
                delayWhenTextChaned = 500;
            }
            _timer = new System.Timers.Timer(delayWhenTextChaned);
            _timer.Elapsed += _timer_Elapsed;
        }

        private void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _timeElapsed = true;
            this.Invoke(new Action(() => { this.OnTextChanged(new EventArgs()); }));
            _timer.Enabled = false;
        }

        protected override void OnTextChanged(EventArgs e)
        {
            if (_timeElapsed)
            {
                _timeElapsed = false;
                base.OnTextChanged(e);
            }
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (!_timer.Enabled)
            {
                _timer.Enabled = true;
                _timer.Interval = DelayWhenTextChaned;
            }
            else
            {
                _timer.Enabled = false;
                _timer.Enabled = true;
                _timer.Interval = DelayWhenTextChaned;
            }
            base.OnKeyPress(e);
        }
    }
}
