using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElkCSharp.ViewModel.Sample
{
    public static class DesignTimeSampleData
    {
        public static LEDModel LED { get; }

        static DesignTimeSampleData()
        {

            LED = new LEDModel() { Name = "Test LED" };

            Task.Run(async delegate
            {
                await Task.Delay(1000);
                LED.Lit = !LED.Lit;

            });
        }


    }
}
