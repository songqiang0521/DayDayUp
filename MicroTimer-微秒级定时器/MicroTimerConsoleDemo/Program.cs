using System;
using System.Collections.Generic;

namespace MicroTimerConsoleDemo
{
    class Program
    {
        static List<string> lists;

        static void Main(string[] args)
        {


            lists = new List<string>(1024*5);
            Program program = new Program();
            program.MicroTimerTest();
        }

        private void MicroTimerTest()
        {
            // Instantiate new MicroTimer and add event handler
            MicroLibrary.MicroTimer microTimer = new MicroLibrary.MicroTimer();
            microTimer.MicroTimerElapsed +=
                new MicroLibrary.MicroTimer.MicroTimerElapsedEventHandler(OnTimedEvent);

            microTimer.Interval = 1000*50; // Call micro timer every 1000µs (1ms)

            // Can choose to ignore event if late by Xµs (by default will try to catch up)
            // microTimer.IgnoreEventIfLateBy = 500; // 500µs (0.5ms)

            microTimer.Enabled = true; // Start timer

            // Do something whilst events happening, for demo sleep 2000ms (2sec)
            System.Threading.Thread.Sleep(50000);

            microTimer.Enabled = false; // Stop timer (executes asynchronously)

            // Alternatively can choose stop here until current timer event has finished
            // microTimer.StopAndWait(); // Stop timer (waits for timer thread to terminate)

            // Wait for user input
            Console.ReadLine();
        }


        private void OnTimedEvent(object sender,
                                  MicroLibrary.MicroTimerEventArgs timerEventArgs)
        {
            // Do something small that takes significantly less time than Interval
            //Console.WriteLine(string.Format(
            //    "Count = {0:#,0}  Timer = {1:#,0} µs, " + 
            //    "LateBy = {2:#,0} µs, ExecutionTime = {3:#,0} µs",
            //    timerEventArgs.TimerCount, timerEventArgs.ElapsedMicroseconds,
            //    timerEventArgs.TimerLateBy, timerEventArgs.CallbackFunctionExecutionTime));


            string item = string.Format(
                "Count = {0:#,0}  Timer = {1:#,0} µs, " + 
                "LateBy = {2:#,0} µs, ExecutionTime = {3:#,0} µs",
                timerEventArgs.TimerCount, timerEventArgs.ElapsedMicroseconds,
                timerEventArgs.TimerLateBy, timerEventArgs.CallbackFunctionExecutionTime);
            lists.Add(item);
        }
    }
}
