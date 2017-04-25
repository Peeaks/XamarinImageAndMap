using System;
using Xamarin.Forms.Maps;
using System.Collections.Generic;
namespace ImageViewer {
	public class CustomMap : Map {

		public List<CustomPin> CustomPins { get; set; }

		public CustomMap ( ) {
			CustomPins = new List<CustomPin> ( );
		}
	}
}
