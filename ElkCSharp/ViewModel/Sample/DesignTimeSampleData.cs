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
        public static BiLEDModel BiLED { get; }

        static DesignTimeSampleData()
        {

            LED = new LEDModel() { Name = "Test LED" };

            BiLED = new BiLEDModel() { Name = "BiLed", Green = 100, Red = 200 };

            Task.Run(async delegate
            {
                await Task.Delay(1000);
                LED.Lit = !LED.Lit;

            });
        }


    }
}
