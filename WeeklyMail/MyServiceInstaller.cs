using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WeeklyMail
{
    [RunInstaller(true)]
    public class MyServiceInstaller : System.Configuration.Install.Installer
    {
        public MyServiceInstaller()
        {
            ServiceProcessInstaller process = new ServiceProcessInstaller();

            process.Account = ServiceAccount.LocalSystem;

            ServiceInstaller serviceAdmin = new ServiceInstaller();

            serviceAdmin.StartType = ServiceStartMode.Manual;
            serviceAdmin.ServiceName = "MailService";
            serviceAdmin.DisplayName = "MailService";
            Installers.Add(process);
            Installers.Add(serviceAdmin);
        }
    }
}
