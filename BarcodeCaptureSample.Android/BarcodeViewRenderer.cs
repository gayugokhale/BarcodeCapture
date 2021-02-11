using System;
using System.IO;
using Android.Content;
using Android.Graphics;
using BarcodeCaptureSample;
using BarcodeCaptureSample.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(BarcodeView), typeof(BarcodeViewRenderer))]
namespace BarcodeCaptureSample.Droid
{
    public class BarcodeViewRenderer : ViewRenderer<BarcodeView, Android.Views.View>
    {
        public BarcodeViewRenderer(Context c) : base(c)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<BarcodeView> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                //register the event
                e.NewElement.OnDrawBitmap += NewElement_OnDrawBitmap;
            }
        }

        private void NewElement_OnDrawBitmap(object sender, EventArgs e)
        {
            try
            {
                if (this.ViewGroup != null)
                {
                    var element = sender as BarcodeView;

                    //get the subview
                    Android.Views.View subView = ViewGroup.GetChildAt(0);
                    int width = subView.Width;
                    int height = subView.Height;
                    
                    //create and draw the bitmap
                    Bitmap b = Bitmap.CreateBitmap(width, height, Bitmap.Config.Argb8888);
                    Canvas c = new Canvas(b);
                    ViewGroup.Draw(c);

                    byte[] bitmapData = null;
                    using (var stream = new MemoryStream())
                    {
                        b.Compress(Bitmap.CompressFormat.Png, 0, stream);
                        bitmapData = stream.ToArray();
                    }

                    element.BarcodeUrl = Convert.ToBase64String(bitmapData);
                }
            }
            catch (Exception ex)
            {
            }
        }

    }
}
