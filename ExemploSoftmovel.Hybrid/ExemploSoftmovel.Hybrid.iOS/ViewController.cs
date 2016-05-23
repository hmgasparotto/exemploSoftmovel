using ExemploSoftmovel.Models;
using ExemploSoftmovel.Repository;
using ExemploSoftmovel.Shared;
using Foundation;
using System;
using System.Web;
using UIKit;

namespace ExemploSoftmovel.Hybrid.iOS
{
	public partial class ViewController : UIViewController
	{
        public UserRepository repository { get; set; }

		public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

            repository = DatabaseConnection.StartDatabase();
            repository.InsertAllFromApi();

            // Perform any additional setup after loading the view, typically from a nib.
            //var bounds = UIScreen.MainScreen.Bounds;
            //UIWebView webView = new UIWebView(bounds);

            var templateMain = new MainView() { Model = "Hello world!" };
            var pageMain = templateMain.GenerateString();
            wvwPage.LoadHtmlString(pageMain, new NSUrl(""));

            wvwPage.ShouldStartLoad += (UIWebView view, NSUrlRequest request, UIWebViewNavigationType type) =>
            {
                var schema = "softmovel:";

                if (!request.Url.ToString().Contains(schema))
                    return true;

                //var resources = wvwPage.Request.Url.ToString().Substring(schema.Length).Split('?');
                var resources = request.Url.ToString().Substring(schema.Length).Split('?');
                var method = resources[0];
                var parameters = HttpUtility.ParseQueryString(resources[1]);
                //var parameters2 = resources[1].Split('&');

                if (method == "OpenPage2")
                {
                    User user = this.repository.ValidateUser(parameters["Email"], parameters["Password"]);

                    if (user != null)
                    {
                        var template = new Page2View() { Model = user };
                        var page = template.GenerateString();
                        view.LoadHtmlString(page, new NSUrl(""));
                        return true;
                    }
                    else
                        return false;
                }
                else if (method.Contains("javascript:"))
                {
                    string aux = "javascript:";
                    view.EvaluateJavascript(method.Substring(aux.Length));
                    return true;
                }
                return true;
            };

            //View.Add(wvwPage);
        }

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}

