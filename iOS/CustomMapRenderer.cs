using System;
using System.Collections.Generic;
using CoreGraphics;
using Foundation;
using ImageViewer;
using ImageViewer.iOS;
using MapKit;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.iOS;
using Xamarin.Forms.Platform.iOS;
using System.Diagnostics;

[assembly: ExportRenderer ( typeof ( CustomMap ), typeof ( CustomMapRenderer ) )]
namespace ImageViewer.iOS {
	public class CustomMapRenderer : MapRenderer {
		UIView customPinView;
		List<CustomPin> customPins;

		protected override void OnElementChanged ( ElementChangedEventArgs<View> e ) {
			base.OnElementChanged ( e );

			if ( e.OldElement != null ) {
				var nativeMap = Control as MKMapView;
				nativeMap.GetViewForAnnotation = null;
				nativeMap.CalloutAccessoryControlTapped -= OnCalloutAccessoryControlTapped;
				nativeMap.DidSelectAnnotationView -= OnDidSelectAnnotationView;
				nativeMap.DidDeselectAnnotationView -= OnDidDeselectAnnotationView;
			}

			if ( e.NewElement != null ) {
				var formsMap = ( CustomMap ) e.NewElement;
				var nativeMap = Control as MKMapView;
				customPins = formsMap.CustomPins;

				nativeMap.GetViewForAnnotation += GetViewForAnnotation;
				nativeMap.CalloutAccessoryControlTapped += OnCalloutAccessoryControlTapped;
				nativeMap.DidSelectAnnotationView += OnDidSelectAnnotationView;
				nativeMap.DidDeselectAnnotationView += OnDidDeselectAnnotationView;
			}
		}

		MKAnnotationView GetViewForAnnotation ( MKMapView mapView, IMKAnnotation annotation ) {
			MKAnnotationView annotationView = null;

			if ( annotation is MKUserLocation )
				return null;

			var anno = annotation as MKPointAnnotation;
			if ( anno == null ) {
				return null;
			}

			var customPin = GetCustomPin ( anno );
			if ( customPin == null ) {
				throw new Exception ( "Custom pin not found" );
			}

			annotationView = mapView.DequeueReusableAnnotation ( customPin.Pin.Label );
			if ( annotationView == null ) {
				annotationView = new CustomMKAnnotationView ( annotation, customPin.Pin.Label );
				annotationView.Image = UIImage.FromFile ( "pin.png" );
				annotationView.CalloutOffset = new CGPoint ( 0, 0 );
				//annotationView.LeftCalloutAccessoryView = new UIImageView ( UIImage.FromFile ( "monkey.png" ) );
				annotationView.RightCalloutAccessoryView = UIButton.FromType ( UIButtonType.DetailDisclosure );
				( ( CustomMKAnnotationView ) annotationView ).Residence = customPin.Residence;
			}
			annotationView.CanShowCallout = true;

			return annotationView;
		}

		void OnCalloutAccessoryControlTapped ( object sender, MKMapViewAccessoryTappedEventArgs e ) {
			//var customView = e.View as CustomMKAnnotationView;
			//if ( !string.IsNullOrWhiteSpace ( customView.Url ) ) {
			//	UIApplication.SharedApplication.OpenUrl ( new Foundation.NSUrl ( customView.Url ) );
			//}
		}

		void OnDidSelectAnnotationView ( object sender, MKAnnotationViewEventArgs e ) {
			var customView = e.View as CustomMKAnnotationView;
			customPinView = new UIView ( );

			customPinView.Frame = new CGRect ( 0, 0, 300, 200 );
			var image = new UIImageView ( new CGRect ( 0, 0, 300, 200 ) );
			image.Image = ImageFromUrl ( customView.Residence.ImageUrls[ 0 ] );
			image.ContentMode = UIViewContentMode.ScaleAspectFit;
			customPinView.AddSubview ( image );
			customPinView.Center = new CGPoint ( 0, -( e.View.Frame.Height + 145 ) );
			e.View.AddSubview ( customPinView );
		}

		void OnDidDeselectAnnotationView ( object sender, MKAnnotationViewEventArgs e ) {
			if ( !e.View.Selected ) {
				customPinView.RemoveFromSuperview ( );
				customPinView.Dispose ( );
				customPinView = null;
			}
		}

		CustomPin GetCustomPin ( MKPointAnnotation annotation ) {
			var position = new Position ( annotation.Coordinate.Latitude, annotation.Coordinate.Longitude );

			foreach ( var pin in customPins ) {
				if ( pin.Pin.Position == position ) {
					return pin;
				}
			}
			return null;
		}

		static UIImage ImageFromUrl ( string uri ) {
			using ( var url = new NSUrl ( uri ) )
			using ( var data = NSData.FromUrl ( url ) )
				return UIImage.LoadFromData ( data );
		}
	}
}
