using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace ExemploSoftmovel.iOS
{
	partial class Page2ViewController : UIViewController
	{
        public string Name { get; set; }

        public Page2ViewController (IntPtr handle) : base (handle)
		{
            
		}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            lblMessage.Text = "Bem-vindo, " + Name + "!"; 
        }
    }
}
