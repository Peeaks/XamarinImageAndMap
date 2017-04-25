using System;

using Xamarin.Forms;

namespace ImageViewer.Pages {
	public class ResidenceTabbedPage : TabbedPage {
		public ResidenceTabbedPage ( ) {
			var listNavigation = new NavigationPage ( new ResidenceListPage ( ) );
			listNavigation.Title = "Boliger";

			var mapNavigation = new NavigationPage ( new ResidenceMapPage ( ) );
			mapNavigation.Title = "Kort";

			Children.Add ( listNavigation );
			Children.Add ( mapNavigation );
		}
	}
}

