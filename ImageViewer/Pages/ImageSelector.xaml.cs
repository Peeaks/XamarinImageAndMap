using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ImageViewer.Pages {
	public partial class ImageSelector : ContentView {
		public ImageSelector ( List<String> imageStrings ) {
			InitializeComponent ( );

			if ( imageStrings == null || imageStrings.Count < 1 ) {
				imageStrings = new List<String> ( );
				imageStrings.Add ( "Placeholder.jpg" );
			}

			bigIMage.Source = ImageSource.FromUri ( new Uri ( imageStrings[ 0 ] ) );

			var imageTap = new TapGestureRecognizer ( );
			imageTap.Tapped += ImageTap_Tapped;
			for ( int i = 0; i < imageStrings.Count; i++ ) {
				grid.ColumnDefinitions.Add ( new ColumnDefinition { Width = new GridLength ( 1, GridUnitType.Star ) } );
				Image image = new Image { Source = ImageSource.FromUri ( new Uri ( imageStrings[ i ] ) ), Aspect = Aspect.Fill };
				grid.Children.Add ( image, i, 0 );
				image.GestureRecognizers.Add ( imageTap );
			}

		}

		void ImageTap_Tapped ( Object sender, EventArgs e ) {
			var senderImage = ( Image ) sender;
			bigIMage.Source = senderImage.Source;
		}
	}
}
