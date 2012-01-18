using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using Microsoft.Phone.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone;
using Microsoft.Devices;
using System.Windows.Media.Imaging;

namespace FlashLightTest
{
    public partial class MainPage : PhoneApplicationPage
    {
        private VideoCamera cam;
        CameraVisualizer visualer;
        // Constructor
        public MainPage()
        {
            System.Diagnostics.Debug.WriteLine("init");
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("OnNavigatedTo ");
            cam = new VideoCamera( CameraSource.PrimaryCamera );
            cam.Initialized += new EventHandler(cam_Initialized);
            visualer = new CameraVisualizer();
            visualer.SetSource(cam);
        }

        protected override void OnNavigatingFrom(System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("OnNavigatedFrom ");
            if (cam != null)
            {
                //cam.StopRecording();
                // Dispose camera to minimize power consumption and to expedite shutdown.
                cam.Dispose();
                cam.Initialized -= cam_Initialized;
                // Release memory, ensure garbage collection.
            }
            
        }

        // Update the UI if initialization succeeds.
        void cam_Initialized(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("camera init method: "+e.ToString());
            // The camera is not supported on the device.
            cam.LampEnabled = true;
        }

        private void torchImage_Tap(object sender, GestureEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("click:" + cam.LampEnabled);
            //cam.LampEnabled = !cam.LampEnabled;
            if (cam.LampEnabled == true)
            {

                if (cam.IsRecording == false)
                {
                    // 打开.
                    cam.StartRecording();
                    BitmapImage ontorchimage = new BitmapImage(new Uri("/FlashLightTest;component/torch_pressed.png", UriKind.Relative));
                    torchImage.Source = ontorchimage;
                    lightImage.Visibility = Visibility.Visible;
                }
                else
                {
                    // 关闭
                    cam.StopRecording();
                    BitmapImage offtorchimage = new BitmapImage(new Uri("/FlashLightTest;component/torch_normal.png", UriKind.Relative));
                    torchImage.Source = offtorchimage;
                    lightImage.Visibility = Visibility.Collapsed;
                }
            }
        }

    }
}