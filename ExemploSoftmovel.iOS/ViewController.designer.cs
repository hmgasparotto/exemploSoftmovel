// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace ExemploSoftmovel.iOS
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton btnLogar { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblEmail { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblSenha { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		MapKit.MKMapView map { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField txtEmail { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField txtSenha { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (btnLogar != null) {
				btnLogar.Dispose ();
				btnLogar = null;
			}
			if (lblEmail != null) {
				lblEmail.Dispose ();
				lblEmail = null;
			}
			if (lblSenha != null) {
				lblSenha.Dispose ();
				lblSenha = null;
			}
			if (map != null) {
				map.Dispose ();
				map = null;
			}
			if (txtEmail != null) {
				txtEmail.Dispose ();
				txtEmail = null;
			}
			if (txtSenha != null) {
				txtSenha.Dispose ();
				txtSenha = null;
			}
		}
	}
}
