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

            DiscDriveModel = new FloppyDriveModel(1, null);

        }

        public static FloppyDriveModel DiscDriveModel { get; }
    }
}
