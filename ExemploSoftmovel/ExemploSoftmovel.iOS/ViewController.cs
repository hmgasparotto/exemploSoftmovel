using System;
using Foundation;
using UIKit;
using System.Collections.Generic;
using ExemploSoftmovel.Models;
using System.Linq;

namespace ExemploSoftmovel.iOS
{
	public partial class ViewController : UIViewController
	{
		public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
            // Perform any additional setup after loading the view, typically from a nib.
            btnLogar.TouchUpInside += delegate {
                string email = txtEmail.Text;
                string password = txtSenha.Text;

                IEnumerable<User> users = AppDelegate.Current.
                    repository.GetAllUsers()
                    .Where(u => u.Email == email && u.Password == password);
                if (users.Count() > 0)
                {
                    NavigationController.ShowViewController(
                        new Page2ViewController(this.Handle), this.btnLogar);
                }
                //ActivateController(new Page2ViewController());
            };
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            base.PrepareForSegue(segue, sender);

            var page2Controller = segue.DestinationViewController 
                as Page2ViewController;
            page2Controller.Name = "Parâmetro";
        }
    }
}

