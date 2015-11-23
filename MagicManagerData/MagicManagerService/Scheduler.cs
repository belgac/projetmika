using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using Quartz;
using Quartz.Impl;

namespace MagicManagerService
{
    public class Scheduler
    {
            private static void Main(string[] args)
            {

            try
                {
                Common.Logging.LogManager.Adapter = new Common.Logging.Simple.ConsoleOutLoggerFactoryAdapter { Level = Common.Logging.LogLevel.Info };

                // Grab the Scheduler instance from the Factory 
                IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();

                    // and start it off
                    scheduler.Start();

                    // define the job and tie it to our HelloJob class
                    IJobDetail job = JobBuilder.Create<HelloJob>()
                        .WithIdentity("job1", "group1")
                        .Build();

                    // Trigger the job to run now, and then repeat every 10 seconds
                    ITrigger trigger = TriggerBuilder.Create()
                        .WithIdentity("trigger1", "group1")
                        .StartNow()
                        .WithSimpleSchedule(x => x
                            .WithIntervalInSeconds(10)
                            .RepeatForever())
                        .Build();

                    // Tell quartz to schedule the job using our trigger
                    scheduler.ScheduleJob(job, trigger);

                    // some sleep to show what's happening
                    Thread.Sleep(TimeSpan.FromSeconds(60));

                    // and last shut down the scheduler when you are ready to close your program
                    scheduler.Shutdown();
                }
                catch (SchedulerException se)
                {
                    Console.WriteLine(se);
                }

            Console.WriteLine("Press any key to close the application");
            Console.ReadKey();

        }
    }
}


//// construct a scheduler factory
//ISchedulerFactory schedFact = new StdSchedulerFactory();

//// get a scheduler
//IScheduler sched = schedFact.GetScheduler();
//sched.Start();

//    // define the job and tie it to our HelloJob class
//    IJobDetail job = JobBuilder.Create<HelloJob>()
//        .WithIdentity("myJob", "group1")
//        .Build();

//// Trigger the job to run now, and then every 40 seconds
//ITrigger trigger = TriggerBuilder.Create()
//  .WithIdentity("myTrigger", "group1")
//  .StartNow()
//  .WithSimpleSchedule(x => x
//      .WithIntervalInSeconds(40)
//      .RepeatForever())
//  .Build();

//sched.ScheduleJob(job, trigger);


//// define the job and tie it to our HelloJob class
//IJobDetail job = JobBuilder.Create<HelloJob>()
//    .WithIdentity("myJob", "group1") // name "myJob", group "group1"
//    .Build();

//// Trigger the job to run now, and then every 40 seconds
//ITrigger trigger = TriggerBuilder.Create()
//    .WithIdentity("myTrigger", "group1")
//    .StartNow()
//    .WithSimpleSchedule(x => x
//        .WithIntervalInSeconds(40)
//        .RepeatForever())
//    .Build();

//// Tell quartz to schedule the job using our trigger
//sched.scheduleJob(job, trigger);