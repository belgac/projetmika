using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MagicManagerService
{
    public class HelloJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine("Greetings from HelloJob!");
        }
    }
}