using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;

namespace BarcodeCaptureSample
{
    public partial class MainPage : ContentPage
    {
        MainPageViewModel viewmodel;
        public MainPage()
        {
            InitializeComponent();
            viewmodel = new MainPageViewModel();
            BindingContext = viewmodel;
        }

        private async void txtBarcodeFocused(object sender, EventArgs e)
        {
            try
            {
                var options = new MobileBarcodeScanningOptions
                {
                    AutoRotate = false,
                    UseFrontCameraIfAvailable = false,
                    TryHarder = true,

                };

                var overlay = new ZXingDefaultOverlay
                {
                    TopText = "Please scan QR code",
                    BottomText = "Align the QR code within the frame"
                };

                var QRScanner = new ZXingScannerPage(options, overlay);

                await Navigation.PushAsync(QRScanner);

                QRScanner.OnScanResult += (result) =>
                {
                    // Stop scanning
                    QRScanner.IsScanning = false;

                    // Pop the page and show the result

                    Navigation.PopAsync(true);
                    txtBarcode.Text = result.Text.ToUpper().Trim();
                };
            }
            catch (Exception ex)
            {
            }
        }

        private void GenerateBarcode(object sender, EventArgs e)
        {
            try
            {
                barcodeview.OnDrawBitmap.Invoke(barcodeview, new EventArgs());
                barcodeview.Opacity = 1;
                start_btn.Opacity = 0;
                viewmodel.BarcodeURL = barcodeview.BarcodeUrl;
                viewmodel.IsEditBarcodeImgVisible = true;
            }
            catch (Exception ex)
            {
            }
        }
    }
}
