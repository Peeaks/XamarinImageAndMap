using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ImageViewer.Pages {
	public partial class ResidenceListPage : ContentPage {

		private List<Residence> _residences;
		private ListView _residenceList;

		public ResidenceListPage ( ) {
			InitializeComponent ( );
			Title = "Boliger";


			// DATA
			_residences = new List<Residence> ( );
			var imageUrls = new List<string> ( );
			imageUrls.Add ( "https://upload.wikimedia.org/wikipedia/commons/9/95/1St_Leonards%2C_New_South_wales.jpg" );
			imageUrls.Add ( "http://www.apartmentbarcelona.com/images/home/apartment-02.jpg" );
			imageUrls.Add ( "http://franchisee.questapartments.com.au/static/uploads/apartment_types/101/Studio%20apartment.jpg" );
			imageUrls.Add ( "http://www.chestnuthillrealty.com/sites/default/files/norwest_woods_apartments_granite_kitchen_740_x_354.jpg" );
			imageUrls.Add ( "https://www.adinahotels.com/wp-content/uploads/sites/4/2016/07/adina-melbourne-flinders-street-apartment-hotel-one-and-two-bedroom-apartment-2-2013.jpg" );
			imageUrls.Add ( "https://res.cloudinary.com/apartmentlist/image/upload/t_fullsize/r0ymlbhl0u9chmnzcula.jpg" );
			_residences.Add ( new Residence { Address = "Nygårdsvej 135, Esbjerg", ImageUrls = imageUrls } );
			_residences.Add ( new Residence { Address = "Frodesgade 23, Esbjerg", ImageUrls = imageUrls } );
			_residences.Add ( new Residence { Address = "Englandsgade 57, Esbjerg", ImageUrls = imageUrls } );
			_residences.Add ( new Residence { Address = "Skolegade 112, Esbjerg", ImageUrls = imageUrls } );


			_residenceList = new ResidenceListView ( ListViewCachingStrategy.RetainElement, _residences );
			_residenceList.ItemsSource = _residences;

			_residenceList.SeparatorVisibility = SeparatorVisibility.None;
			_residenceList.BackgroundColor = Color.Silver;

			_residenceList.ItemSelected += SelectedResidence;

			Content = _residenceList;
		}

		protected override void OnDisappearing ( ) {
			base.OnDisappearing ( );

			_residenceList.SelectedItem = null;
		}

		public async void SelectedResidence ( object sender, SelectedItemChangedEventArgs e ) {
			if ( _residenceList.SelectedItem != null ) {
				var selectedIndex = _residences.IndexOf ( ( Residence ) _residenceList.SelectedItem );
				var selectedResidence = _residences[ selectedIndex ];

				await Navigation.PushAsync ( new SelectedResidencePage ( selectedResidence ) );
			}
		}
	}

	public class ResidenceListView : ListView {
		private List<Residence> _residences;

		public ResidenceListView ( ListViewCachingStrategy strategy, List<Residence> residences ) : base ( strategy ) {
			ItemTemplate = new DataTemplate ( typeof ( ResidenceCell ) );
			RowHeight = 125;
			this._residences = residences;
		}

		protected override void SetupContent ( Cell content, int index ) {
			base.SetupContent ( content, index );

			var currentViewCell = content as ResidenceCell;

			if ( currentViewCell != null ) {
				currentViewCell.setupCell ( _residences[ index ] );
			}
		}
	}

	public class ResidenceCell : ViewCell {
		public ResidenceCell ( ) {

		}

		public void setupCell ( Residence residence ) {
			var masterLayout = new StackLayout ( );
			masterLayout.Padding = new Thickness ( 10, 10, 10, 10 );


			var horizontalLayout = new StackLayout {
				Orientation = StackOrientation.Horizontal
			};

			horizontalLayout.BackgroundColor = Color.White;
			horizontalLayout.Padding = 10;

			var image = new Image ( );
			image.Source = residence.ImageUrls[ 0 ];

			image.Aspect = Aspect.AspectFill;
			image.WidthRequest = 175;
			image.HeightRequest = 100;

			//	RelativeLayout layout = new RelativeLayout();

			//layout.Children.Add(image, Constraint.Constant(0), Constraint.Constant(0), Constraint.RelativeToParent((parent) => { return parent.Width; }), Constraint.RelativeToParent((parent) => { return parent.Height; }));
			//layout.Children.Add(new Label { Text = "HEJ" }, Constraint.Constant(0), Constraint.Constant(0), Constraint.RelativeToParent((parent) => { return parent.Width; }), Constraint.RelativeToParent((parent) => { return parent.Height; }));

			//layout.WidthRequest = 100;
			//layout.HeightRequest = 100;

			//Label stacklayout in right side
			var verticalLayout = new StackLayout {
				Orientation = StackOrientation.Vertical

			};

			var lblAddress = new Label ( );



			verticalLayout.Children.Add ( lblAddress );

			lblAddress.Text = residence.Address;


			horizontalLayout.Children.Add ( image );

			//horizontalLayout.Children.Add(image);
			horizontalLayout.Children.Add ( verticalLayout );

			masterLayout.Children.Add ( horizontalLayout );

			View = masterLayout;
		}
	}
}
