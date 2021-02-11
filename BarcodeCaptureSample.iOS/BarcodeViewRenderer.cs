using System;
using BarcodeCaptureSample;
using BarcodeCaptureSample.iOS;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BarcodeView), typeof(BarcodeViewRenderer))]
namespace BarcodeCaptureSample.iOS
{
    public class BarcodeViewRenderer : ViewRenderer<BarcodeView, UIView>
    {
        public BarcodeViewRenderer()
        {
        }

        BarcodeView view;
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
                var formsView = Element;
                var rect = new CGRect(formsView.Bounds.X, formsView.Bounds.Y, formsView.Bounds.Width, formsView.Bounds.Height);
                var iOSView = ConvertFormsToNative(formsView, rect);
                UIImage image = ConvertViewToImage(iOSView);

                //var renderer = Xamarin.Forms.Platform.iOS.Platform.GetRenderer(formsView);
                //Xamarin.Forms.Platform.iOS.Platform.SetRenderer(formsView, renderer);
                //var viewGroup = renderer.NativeView;
                //UIImage image;
                //// UIGraphics.BeginImageContext(viewGroup.Bounds.Size);
                //viewGroup.DrawViewHierarchy(viewGroup.Bounds, true);
                // image = UIGraphics.GetImageFromCurrentImageContext();

                //var view = sender as UIView;
                //UIGraphics.BeginImageContextWithOptions(view.Bounds.Size, opaque: true, scale: 0);
                //UIImage image;

                //    view.DrawViewHierarchy(view.Bounds, afterScreenUpdates: true);
                //    image = UIGraphics.GetImageFromCurrentImageContext();

                //UIGraphics.EndImageContext();


                if (image != null)
                    formsView.BarcodeUrl = image.AsPNG().GetBase64EncodedString(Foundation.NSDataBase64EncodingOptions.EndLineWithLineFeed);

            }
            catch (Exception ex)
            {
            }
        }

        private UIImage ConvertViewToImage(UIView iOSView)
        {
            try
            {
                UIGraphics.BeginImageContextWithOptions(iOSView.Bounds.Size, true, 0);
                iOSView.DrawViewHierarchy(iOSView.Bounds, true);
                var context = UIGraphics.GetCurrentContext();
                iOSView.Layer.RenderInContext(context);
                var img = UIGraphics.GetImageFromCurrentImageContext();
                UIGraphics.EndImageContext();
                
                return img;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static UIView ConvertFormsToNative(Xamarin.Forms.View view, CGRect size)
        {
            var renderer = Platform.CreateRenderer(view);

            renderer.NativeView.Frame = size;

            renderer.NativeView.AutoresizingMask = UIViewAutoresizing.All;
            renderer.NativeView.ContentMode = UIViewContentMode.ScaleToFill;

            renderer.Element.Layout(size.ToRectangle());

            var nativeView = renderer.NativeView;

            nativeView.SetNeedsLayout();

            return nativeView;
        }

    }
}
