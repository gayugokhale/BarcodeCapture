using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace BarcodeCaptureSample
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private string _Barcode;
        public string Barcode
        {
            get { return _Barcode; }
            set
            {
                _Barcode = value;
                OnPropertyChanged();
            }
        }

        private ImageSource barcodesource1;
        public ImageSource BarcodeSource1
        {
            get { return barcodesource1; }
            set
            {
                barcodesource1 = value;
                OnPropertyChanged();
            }
        }
        private string _BarcodeURL;


        public string BarcodeURL
        {
            get { return _BarcodeURL; }
            set
            {
                _BarcodeURL = value;
                BarcodeSource1 = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(BarcodeURL)));
            }
        }

        private string _RandomBarcodeNumber;
        public string RandomBarcodeNumber
        {
            get { return _RandomBarcodeNumber; }
            set
            {
                _RandomBarcodeNumber = value;
                OnPropertyChanged();
            }
        }
        private bool _IsEditBarcodeImgVisible = false;
        public bool IsEditBarcodeImgVisible
        {
            get { return _IsEditBarcodeImgVisible; }
            set
            {
                _IsEditBarcodeImgVisible = value;
                OnPropertyChanged();
            }
        }
        
        public MainPageViewModel()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
