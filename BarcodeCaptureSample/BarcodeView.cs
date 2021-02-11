using System;
using Xamarin.Forms;

namespace BarcodeCaptureSample
{
    public class BarcodeView : StackLayout
    {
        public EventHandler OnDrawBitmap;

        public static readonly BindableProperty BarcodeUrlProperty = BindableProperty.Create(nameof(BarcodeUrl),
           typeof(string),
           typeof(string),
           default(string));//, propertyChanged: OnItemsSourcePropertyChangedInternal);

        public string BarcodeUrl
        {
            get { return (string)GetValue(BarcodeUrlProperty); }
            set { SetValue(BarcodeUrlProperty, value); }
        }

    }
}
