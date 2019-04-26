using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace WeeklyMail
{
    public partial class MailService : ServiceBase
    {
        //        string timeString;
        //        private System.Timers.Timer timer1;
        //        public MailService()
        //        {
        //            int strTime = Convert.ToInt32(ConfigurationManager.AppSettings["callDuration"]);
        //            var getCallType = Convert.ToInt32(ConfigurationManager.AppSettings["CallType"]);
        //            if (getCallType == 1)
        //            {
        //                timer1 = new System.Timers.Timer();
        //                double inter = (double)GetNextInterval();
        //                timer1.Interval = inter;
        //                timer1.Elapsed += new ElapsedEventHandler(ServiceTimer_Tick);
        //            }
        //            else
        //            {
        //                timer1 = new System.Timers.Timer();
        //                timer1.Interval = strTime * 1000;
        //                timer1.Elapsed += new ElapsedEventHandler(ServiceTimer_Tick);
        //            }

        //            InitializeComponent();

        //        }



        //        private double GetNextInterval()
        //        {
        //            timeString = ConfigurationManager.AppSettings["StartTime"];
        //            DateTime t = DateTime.Parse(timeString);

        //            TimeSpan ts = new TimeSpan();
        //            int x;
        //            ts = t - System.DateTime.Now;
        //            if (ts.TotalMilliseconds < 0)
        //            {
        //                ts = t.AddMinutes(5) - System.DateTime.Now;//Here you can increase the timer interval based on your requirments.   
        //            }
        //            return ts.TotalMilliseconds;
        //        }
        //        private void SetTimer()
        //        {
        //            try
        //            {
        //                double inter = (double)GetNextInterval();
        //                timer1.Interval = inter;
        //                timer1.Start();
        //            }
        //            catch (Exception ex)
        //            {
        //            }
        //        }

        //        protected override void OnStart(string[] args)
        //        {
        //            DebugMode();

        //            timer1.AutoReset = true;
        //            timer1.Enabled = true;

        //            ServiceLog.WriteErrorLog("Daily Reporting service started");
        //        }

        //        protected override void OnStop()
        //        {
        //            timer1.AutoReset = false;
        //            timer1.Enabled = false;
        //            ServiceLog.WriteErrorLog("Daily Reporting service stopped");
        //        }
        //        private void ServiceTimer_Tick(object sender, System.Timers.ElapsedEventArgs e)
        //        {
        //            string Msg = "Hi ! This is DailyMailSchedulerService mail.";//whatever msg u want to send write here.  
        //            // Here you can write the   
        //            ServiceLog.SendEmail("waqarchaudhary1928@gmail.com",
        //                "Daily Report of DailyMailSchedulerService on " + DateTime.Now.ToString("dd-MMM-yyyy"), Msg);
        //            var getCallType = Convert.ToInt32(ConfigurationManager.AppSettings["CallType"]);
        //            if (getCallType == 1)
        //            {
        //                timer1.Stop();
        //                System.Threading.Thread.Sleep(1000000);
        //                SetTimer();
        //            }
        //        }
        //        [Conditional("DEBUG_SERVICE")]
        //        private static void DebugMode()
        //        {
        //            Debugger.Break();
        //        }


        //    }
        //}
        private System.Timers.Timer timer;

        public MailService()
        {
            InitializeComponent();
        }
        public void onDebug()
        {
            OnStart(null);
        }
        protected override void OnStart(string[] args)
        {
            DebugMode();
            //instantiate timer
            Thread t = new Thread(new ThreadStart(this.InitTimer));
           
            t.Start();
        }

        protected override void OnStop()
        {
            timer.Enabled = false;
        }

        private void InitTimer()
        {
            timer = new System.Timers.Timer();
            //wire up the timer event 
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            //set timer interval   
            //var timeInSeconds = Convert.ToInt32(ConfigurationManager.AppSettings["TimerIntervalInSeconds"]); 
            double timeInSeconds = 3.0;
            timer.Interval = (timeInSeconds * 1000);
            // timer.Interval is in milliseconds, so times above by 1000 
            timer.Enabled = true;
        }

        protected void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            int timer_fired = 0;
        }
        [Conditional("DEBUG_SERVICE")]
        private static void DebugMode()
        {
            Debugger.Break();
        }
    }
}