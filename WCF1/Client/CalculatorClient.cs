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
    class CalculatorClient : ClientBase<ICalculator>, ICalculator
    {
        public CalculatorClient() : base() { }

        public CalculatorClient(string endpointConfigurationName) : base(endpointConfigurationName) { }

        public CalculatorClient(Binding binding, EndpointAddress address) : base(binding,address) { }


        public double Add(double x, double y)
        {
            return this.Channel.Add(x,y);
        }

        public double Divide(double x, double y)
        {
            return this.Channel.Divide(x, y);
        }

        public double Multiply(double x, double y)
        {
            return this.Channel.Multiply(x, y);
        }

        public double Subtract(double x, double y)
        {
            return this.Channel.Subtract(x, y);
        }
    }
}
