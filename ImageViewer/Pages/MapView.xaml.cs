using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Linq;
using System.Diagnostics;

namespace ImageViewer.Pages {
	public partial class MapView : ContentView {
		Map map;
		public MapView ( String address ) {
			InitializeComponent ( );

			GetLocation ( address );

			map = new Map {
				IsShowingUser = true,
				HeightRequest = 300,
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				MapType = MapType.Street
			};

			stack.Children.Add ( map );
		}

		private async void GetLocation ( String address ) {
			var geoCoder = new Geocoder ( );
			var positions = ( await geoCoder.GetPositionsForAddressAsync ( address ) ).ToList ( );

			map.MoveToRegion ( MapSpan.FromCenterAndRadius ( positions[ 0 ], Distance.FromKilometers ( 0.3 ) ) );
			var pin = new Pin {
				Type = PinType.Place,
				Position = positions[ 0 ],
				Label = address
			};
			map.Pins.Add ( pin );
		}
	}
}