using System;
using System.Threading;
namespace _3
{
    public class AlarmEventArgs : EventArgs
    {
        public DateTime CurrentTime { get; set; }
        public string Message { get; set; }
    }
    public class AlarmClock
    {
        // 定义事件（使用标准 EventHandler 委托）
        public event EventHandler<AlarmEventArgs> Tick;
        public event EventHandler<AlarmEventArgs> Alarm;

        private DateTime _alarmTime;

        public AlarmClock(DateTime alarmTime)
        {
            _alarmTime = alarmTime;
        }

        public void Start()
        {
            while (true)
            {
                DateTime now = DateTime.Now;

                // 触发 Tick 事件
                OnTick(now);

                // 检查是否到达响铃时间
                if (now.Hour == _alarmTime.Hour &&
                    now.Minute == _alarmTime.Minute &&
                    now.Second == _alarmTime.Second)
                {
                    OnAlarm(now);
                }

                Thread.Sleep(1000); // 模拟每秒走时
            }
        }

        // 触发事件的受保护方法（规范做法）
        protected virtual void OnTick(DateTime time)
        {
            Tick?.Invoke(this, new AlarmEventArgs { CurrentTime = time });
        }

        protected virtual void OnAlarm(DateTime time)
        {
            Alarm?.Invoke(this, new AlarmEventArgs { CurrentTime = time, Message = "起床啦！⏰" });
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("设置闹钟为 10 秒后...");
            DateTime target = DateTime.Now.AddSeconds(10);

            AlarmClock clock = new AlarmClock(target);

            // 订阅事件
            clock.Tick += (sender, e) => {
                Console.WriteLine($"[嘀嗒]: {e.CurrentTime.ToLongTimeString()}");
            };

            clock.Alarm += (sender, e) => {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n*** {e.Message} 当前时间: {e.CurrentTime} ***\n");
                Console.ResetColor();
            };

            // 启动闹钟
            clock.Start();
        }
    }
}
