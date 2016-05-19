using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Webkit;
using ExemploSoftmovel.Shared;
using ExemploSoftmovel.Repository;

namespace ExemploSoftmovel.Hybrid.Droid
{
	[Activity (Label = "ExemploSoftmovel.Hybrid.Droid", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
        public UserRepository repository { get; set; }

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            WebView webView = FindViewById<WebView>(Resource.Id.wvwPage);
            webView.SetWebViewClient(new SoftmovelWebClient());

            repository = DatabaseConnection.StartDatabase();
            repository.InsertAllFromApi();

            var template = new MainView() { Model = "Hello world!" };
            var page = template.GenerateString();
            webView.LoadDataWithBaseURL("file:///android_assets/", page, "text/html", "UTF-8", null);
		}
	}
}


