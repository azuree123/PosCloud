using System;
using System.Collections.Generic;
using System.Configuration.Install;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WeeklyMail
{
    static class Program
    {

        static void Main()
        {

ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new MailService()
            };
            ServiceBase.Run(ServicesToRun);


        }
    }
}
