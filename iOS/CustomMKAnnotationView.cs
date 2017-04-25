using System;
using MapKit;
namespace ImageViewer.iOS {
	public class CustomMKAnnotationView : MKAnnotationView {
		public Residence Residence { get; set; }

		public CustomMKAnnotationView ( IMKAnnotation annotation, string id )
					: base ( annotation, id ) {
		}
	}
}
