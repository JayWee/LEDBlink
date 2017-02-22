using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Devices.Gpio;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LEDBlink
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private bool isledon;
        private DispatcherTimer timer;

        public MainPage()
        {
            this.InitializeComponent();

            InitGpio();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += Timer_Tick;

            if (led != null)
            {
                timer.Start();
            }

        }

        private void Timer_Tick(object sender, object e)
        {
            if (isledon == false)
            {
                led.Write(GpioPinValue.High);
                isledon = true;
            }
            else if (isledon)
            {
                led.Write(GpioPinValue.Low);
                isledon = false;
            }
        }

        private GpioPin led;

        private void InitGpio()
        {
            var gpio = GpioController.GetDefault();

            if (gpio == null)
            {
                led = null;
                return;
            }
            
            led = gpio.OpenPin(17);
            led.SetDriveMode(GpioPinDriveMode.Output);
            led.Write(GpioPinValue.Low);

            

        }
    }
}
