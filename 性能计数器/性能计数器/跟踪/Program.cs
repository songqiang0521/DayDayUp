using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Diagnostics;
using System.Configuration;

namespace 跟踪
{
    class Program
    {
        static void Main(string[] args)
        {
            #region tracesource
            ////TraceSource source1 = new TraceSource("APP-SQ", SourceLevels.Error|SourceLevels.Warning);
            ////TraceSource source1 = new TraceSource(ConfigurationManager.AppSettings["MyTraceSource"]);

            ////跟踪源名称在配置文件中描述
            //TraceSource source1 = new TraceSource("MyTraceSource");
            //source1.Switch = new SourceSwitch("MySourceSwitch", "Warning");



            //XmlWriterTraceListener listener = new XmlWriterTraceListener("xmlDemo.xml");
            ////listener.TraceOutputOptions = TraceOptions.DateTime | TraceOptions.LogicalOperationStack | TraceOptions.ProcessId | TraceOptions.Timestamp;


            //source1.Listeners.Add(listener);


            //source1.TraceInformation("普通信息");
            //source1.TraceEvent(TraceEventType.Error, 33,"发生错误");
            //source1.TraceData(TraceEventType.Information, 22, "ddw");
            //source1.Close();
            #endregion

            #region log
            //EventLog.Delete("SQ日志文件");
            //EventLog.DeleteEventSource("SQ应用程序源");


            //if (!EventLog.SourceExists("SQ应用程序源"))
            //{
            //    //EventSourceCreationData eventData = new EventSourceCreationData("SQ应用程序源", "SQ日志文件");
            //    EventSourceCreationData eventData = new EventSourceCreationData("SQ应用程序源", "SQ日志文件");
            //    //eventData.LogName = "SQ日志文件";
            //    EventLog.CreateEventSource(eventData);


            //}
            //EventLog.WriteEntry("SQ应用程序源", "错误", EventLogEntryType.Error, 33);




            //using (EventLog log = new EventLog("SQ日志文件",".","SQ应用程序源"))
            //{
                
            //    log.WriteEntry("消息1");
            //    log.WriteEntry("消息2:警告", EventLogEntryType.Warning);
            //    log.WriteEntry("消息3：错误", EventLogEntryType.Error, 33334);


            //}
           
            
                //if (EventLog.Exists("SQ"))
                //{
                //    EventLog.Delete("SQ");
                //      //EventLog.DeleteEventSource("SQ");
                //}
            //EventLog.DeleteEventSource("EventAppLog", ".");
            #endregion 


            //if (EventLog.Exists("SQ日志文件"))
            //{
            //    EventLog.Delete("SQ日志文件");
            //}

            //if (EventLog.SourceExists("SQ应用程序源"))
            //{
            //    EventLog.DeleteEventSource("SQ应用程序源");
            //}

            if (!EventLog.Exists("SQ日志文件"))
            {
                EventSourceCreationData sourceData = new EventSourceCreationData("SQ应用程序源", "SQ日志文件");

                EventLog.CreateEventSource(sourceData);
            }

            using (EventLog log = new EventLog("SQ日志文件", ".", "SQ应用程序源"))
            {
                //log.WriteEvent(new EventInstance(10,2,
                log.WriteEntry("消息1");
                log.WriteEntry("消息2:警告", EventLogEntryType.Warning);
                log.WriteEntry("消息3：错误", EventLogEntryType.Error, 33334);

            }



            Console.WriteLine("Over!");
            Console.ReadLine();
        }
    }
}
