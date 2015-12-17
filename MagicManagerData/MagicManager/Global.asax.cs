using MagicManager.MkmRequests;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MagicManager
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //// Grab the Scheduler instance from the Factory 
            //IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();

            //// define the job and tie it to our HelloJob class
            //IJobDetail job = JobBuilder.Create<DailyPriceUpdate>()
            //    .WithIdentity("job1", "group1")
            //    .Build();

            //// Trigger the job to run now, and then repeat every 10 seconds
            //ITrigger trigger = TriggerBuilder.Create()
            //    .WithIdentity("trigger1", "group1")
            //    .StartNow()
            //    .WithSimpleSchedule(x => x
            //        .WithIntervalInHours(24)
            //        .RepeatForever())
            //    .Build();

            //// Tell quartz to schedule the job using our trigger
            //scheduler.ScheduleJob(job, trigger);

            //// and start it off
            //scheduler.Start();

            
        }
    }
}
