using System;
using Foundation;
using UIKit;
using ExemploSoftmovel.Models;
using MultiThreading.Controls;
using CoreGraphics;
using System.Threading.Tasks;

namespace ExemploSoftmovel.iOS
{
    public partial class ViewController : UIViewController
    {
        public string Name { get; set; }

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        public override bool ShouldPerformSegue(string segueIdentifier, NSObject sender)
        {
            if (segueIdentifier != "mainTo2")
                return base.ShouldPerformSegue(segueIdentifier, sender);

            var email = this.txtEmail.Text;
            var senha = this.txtSenha.Text;

            var dialog = new LoadingOverlay(new CGRect(0, 0, 100, 200), "Validando usuário...");
            View.Add(dialog);
            User user = null;
            Task.Run(() => user = AppDelegate.Current.repository.ValidateUser(email, senha));
            dialog.Hide();

            if (user != null)
            {
                Name = user.Name;
                return true;
            }
            else
            {
                new UIAlertView("Erro", "Usuário ou senha incorretos!", null, "OK", null).Show();
                return false;
            }           
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            base.PrepareForSegue(segue, sender);

            var page2Controller = segue.DestinationViewController as Page2ViewController;
            page2Controller.Name = Name;
        }
    }
}