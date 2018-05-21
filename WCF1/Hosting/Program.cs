using Contracts;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;


namespace Hosting
{
    class Program
    {
        static void Main(string[] args)
        {
            //////////////////////////////////////////////////////////////////////////////////////////////////////
            //using (ServiceHost  host =new ServiceHost(typeof(CalculatorService))) {

            //    //host.AddServiceEndpoint(typeof(ICalculator),new WSHttpBinding(),"http://127.0.0.1:9999/calculatorservice");

            //    //if (host.Description.Behaviors.Find<ServiceMetadataBehavior>()==null) {

            //    //    ServiceMetadataBehavior behavior = new ServiceMetadataBehavior();

            //    //    behavior.HttpGetEnabled = true;

            //    //    behavior.HttpGetUrl = new Uri("http://127.0.0.1:9999/calculatorservice/metadata");

            //    //    host.Description.Behaviors.Add(behavior);

            //    //}
            /////////////////////////////////////////////////////////////////////////////////////////////采用配置后
            //    host.Opened += delegate
            //    {
            //        Console.WriteLine("CalculaorService已经启动，按任意键终止服务！");
            //    };

            //    host.Open();
            //    Console.Read();
            //////////////////////////////////////////////////////////////////////////////////////////////////
            //}
            HostCalculatorSerivceViaConfiguration();
        }

        static void HostCalculatorSerivceViaConfiguration() {

            Uri httpBaseAddress = new Uri("http://localhost:8888/generalCalculator");

            Uri tcpBaseAddress  = new Uri("net.tcp://localhost:9999/generalCalculator");

            using (ServiceHost calculatorServiceHost=new ServiceHost(typeof(CalculatorService),httpBaseAddress,tcpBaseAddress)) {
                BasicHttpBinding httpBinding = new BasicHttpBinding();
                NetTcpBinding tcpBinding = new NetTcpBinding();

                calculatorServiceHost.AddServiceEndpoint(typeof(ICalculator),httpBinding,string.Empty);
                calculatorServiceHost.AddServiceEndpoint(typeof(ICalculator),tcpBinding,string.Empty);

                ServiceMetadataBehavior behavior = calculatorServiceHost.Description.Behaviors.Find<ServiceMetadataBehavior>();
                {
                    if (behavior == null)
                    {
                        behavior = new ServiceMetadataBehavior();
                        behavior.HttpGetEnabled = true;
                        calculatorServiceHost.Description.Behaviors.Add(behavior);
                    }
                    else {

                        behavior.HttpGetEnabled = true;
                    }

                }

                calculatorServiceHost.Opened += delegate
                {
                    Console.WriteLine("Calculator Service has begun to listen  ");

                };

                calculatorServiceHost.Open();
                Console.Read();
            }
        }


        static void AHostCalculatorSerivceViaConfiguration() {
            using (ServiceHost calculatorServiceHost = new ServiceHost(typeof(CalculatorService))) {
                calculatorServiceHost.Opened += delegate
                {
                    Console.WriteLine("Calculator Service has begun to listen  ");

                };
                calculatorServiceHost.Open();
                Console.Read();
            }
        }



    }
}
