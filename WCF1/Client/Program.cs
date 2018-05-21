using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            //using (ChannelFactory<ICalculator> channelFactory = new ChannelFactory<ICalculator>("calculatorservice")) {

            //    ICalculator proxy = channelFactory.CreateChannel();
            //    using (proxy as IDisposable) {
            //        Console.WriteLine("x + y = {2} when x = {0} and y = {1}", 1, 2, proxy.Add(1, 2));
            //        Console.WriteLine("x - y = {2} when x = {0} and y = {1}", 1, 2, proxy.Subtract(1, 2));
            //        Console.WriteLine("x * y = {2} when x = {0} and y = {1}", 1, 2, proxy.Multiply(1, 2));
            //        Console.WriteLine("x / y = {2} when x = {0} and y = {1}", 1, 2, proxy.Divide(1, 2));
            //    }
            //}
            try {
                InvocateCalclatorServiceViaConfiguration();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.Read();
        }

        static void InvocateCalclatorServiceViaConfiguration(){
            Binding httpBinding = new BasicHttpBinding();
            Binding tcpBinding = new NetTcpBinding();

            EndpointAddress httpAddress = new EndpointAddress("http://localhost:8888/generalCalculator");
            EndpointAddress tcpAddress = new EndpointAddress("net.tcp://localhost:9999/generalCalculator");
            EndpointAddress httpAddress_iisHost = new EndpointAddress("http://localhost/wcfservice/GeneralCalculatorService.svc");

            Console.WriteLine("Invocate self-host calculator service ");

            using (CalculatorClient calculator_http =new CalculatorClient(httpBinding,httpAddress)) {
                using (CalculatorClient calculator_tcp = new CalculatorClient(tcpBinding,tcpAddress)) {
                    try {
                        Console.WriteLine("Begin to invocate calculator service via http transport ");
                        Console.WriteLine("x + y = {2} where x = {0} and y = {1}", 1, 2, calculator_http.Add(1, 2));

                        Console.WriteLine("Begin to invocate calculator service via tcp transport ");
                        Console.WriteLine("x + y = {2} where x = {0} and y = {1}", 1, 2, calculator_tcp.Add(1, 2));
                    }
                    catch (Exception ex) {
                        Console.WriteLine(ex.Message);
                    }

                }
            }

            Console.WriteLine("\n\nInvocate IIS-host calculator service ");

            using (CalculatorClient calculator = new CalculatorClient(httpBinding, httpAddress_iisHost))
            {
                try
                {
                    Console.WriteLine("Begin to invocate calculator service via http transport ");
                    Console.WriteLine("x + y = {2} where x = {0} and y = {1}", 1, 2, calculator.Add(1, 2));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }


        static void AInvocateCalclatorServiceViaConfiguration()
        {
            Console.WriteLine("Invocate self-host calculator service ");

            #region Invocate Self-host service
            using (CalculatorClient calculator_http = new CalculatorClient("selfHostEndpoint_http"))
            {
                using (CalculatorClient calculator_tcp = new CalculatorClient("selfHostEndpoint_tcp"))
                {
                    try
                    {
                        Console.WriteLine("Begin to invocate calculator service via http transport ");
                        Console.WriteLine("x + y = {2} where x = {0} and y = {1}", 1, 2, calculator_http.Add(1, 2));

                        Console.WriteLine("Begin to invocate calculator service via tcp transport ");
                        Console.WriteLine("x + y = {2} where x = {0} and y = {1}", 1, 2, calculator_tcp.Add(1, 2));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            #endregion

            Console.WriteLine("\n\nInvocate IIS-host calculator service ");

            #region Invocate IIS-host service
            using (CalculatorClient calculator = new CalculatorClient("iisHostEndpoint"))
            {
                try
                {
                    Console.WriteLine("Begin to invocate calculator service via http transport ");
                    Console.WriteLine("x + y = {2} where x = {0} and y = {1}", 1, 2, calculator.Add(1, 2));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            #endregion
        }


    }
}
