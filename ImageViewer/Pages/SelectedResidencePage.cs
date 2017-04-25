using Xamarin.Forms;
using System.Collections.Generic;
using System;

namespace ImageViewer.Pages {
	public partial class SelectedResidencePage : ContentPage {
		public SelectedResidencePage ( Residence residence ) {
			InitializeComponent ( );

			Title = residence.Address;
			Padding = 20;

			stack.Children.Add ( new ImageSelector ( residence.ImageUrls ) );

			stack.Children.Add ( new MapView ( residence.Address ) );

		}

	}
}
