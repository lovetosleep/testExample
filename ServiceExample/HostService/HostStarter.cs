﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using ServiceExample;

namespace HostService
{
    class HostStarter
    {
        static void Main(string[] args)
        {
            using (var host = new ServiceHost(typeof(ServiceExample.RepositoryService)))
            {
                host.Description.Behaviors.Add(new ErrorHandlerBehavior());
                host.Open();
                Console.WriteLine("Host started ...");
                Console.ReadKey();
            }
        }
    }
}
