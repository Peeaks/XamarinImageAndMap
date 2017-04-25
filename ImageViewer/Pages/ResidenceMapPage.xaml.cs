using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Linq;
using System.Diagnostics;
using Plugin.Geolocator;

namespace ImageViewer.Pages {
	public partial class ResidenceMapPage : ContentPage {
		CustomMap map;
		public ResidenceMapPage ( ) {
			InitializeComponent ( );
			Title = "Kort";

			// DATA
			var _residences = new List<Residence> ( );
			var imageUrls = new List<string> ( );
			imageUrls.Add ( "https://upload.wikimedia.org/wikipedia/commons/9/95/1St_Leonards%2C_New_South_wales.jpg" );
			imageUrls.Add ( "http://www.apartmentbarcelona.com/images/home/apartment-02.jpg" );
			imageUrls.Add ( "http://franchisee.questapartments.com.au/static/uploads/apartment_types/101/Studio%20apartment.jpg" );
			imageUrls.Add ( "http://www.chestnuthillrealty.com/sites/default/files/norwest_woods_apartments_granite_kitchen_740_x_354.jpg" );
			imageUrls.Add ( "https://www.adinahotels.com/wp-content/uploads/sites/4/2016/07/adina-melbourne-flinders-street-apartment-hotel-one-and-two-bedroom-apartment-2-2013.jpg" );
			imageUrls.Add ( "https://res.cloudinary.com/apartmentlist/image/upload/t_fullsize/r0ymlbhl0u9chmnzcula.jpg" );
			_residences.Add ( new Residence { Address = "Nygårdsvej 135, Esbjerg", ImageUrls = imageUrls.GetRange ( 0, 5 ) } );
			_residences.Add ( new Residence { Address = "Frodesgade 23, Esbjerg", ImageUrls = imageUrls.GetRange ( 2, 3 ) } );
			_residences.Add ( new Residence { Address = "Englandsgade 57, Esbjerg", ImageUrls = imageUrls.GetRange ( 1, 4 ) } );
			_residences.Add ( new Residence { Address = "Skolegade 112, Esbjerg", ImageUrls = imageUrls.GetRange ( 4, 1 ) } );


			map = new CustomMap {
				IsShowingUser = true,
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				MapType = MapType.Street
			};

			stack.Children.Add ( map );

			GetLocations ( _residences );
		}

		private async void GetLocations ( List<Residence> residences ) {
			var geoCoder = new Geocoder ( );

			foreach ( var residence in residences ) {
				var position = ( await geoCoder.GetPositionsForAddressAsync ( residence.Address ) ).ToList ( )[ 0 ];

				var pin = new CustomPin {
					Pin = new Pin {
						Type = PinType.Place,
						Position = position,
						Label = residence.Address
					},
					Residence = residence
				};
				//pin.Clicked += delegate {
				//	Navigation.PushAsync ( new SelectedResidencePage ( residence ), true );
				//};
				map.CustomPins.Add ( pin );
				map.Pins.Add ( pin.Pin );

				Debug.WriteLine ( "Latitude: {0} Longitude: {1}", position.Latitude, position.Longitude );
			}

			var locator = CrossGeolocator.Current;
			locator.DesiredAccuracy = 50;

			var devicePosition = await locator.GetPositionAsync ( 2000 );

			map.MoveToRegion ( MapSpan.FromCenterAndRadius ( new Position ( devicePosition.Latitude, devicePosition.Longitude ), Distance.FromKilometers ( 2.0 ) ) );
		}

	}
}
