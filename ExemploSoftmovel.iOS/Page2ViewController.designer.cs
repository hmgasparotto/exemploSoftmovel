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
	[Register ("Page2ViewController")]
	partial class Page2ViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel lblMessage { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (lblMessage != null) {
				lblMessage.Dispose ();
				lblMessage = null;
			}
		}
	}
}
