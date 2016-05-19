using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Webkit;
using System.Web;
using ExemploSoftmovel.Models;

namespace ExemploSoftmovel.Hybrid.Droid
{
    public class SoftmovelWebClient : WebViewClient
    {
        public override bool ShouldOverrideUrlLoading(WebView view, string url)
        {
            var schema = "softmovel:";

            var resources = url.Substring(schema.Count()).Split('?');
            var method = resources[0];
            var parameters = HttpUtility.ParseQueryString(resources[1]);
            //var parameters2 = resources[1].Split('&');

            if (method == "OpenPage2")
            {
                var parent = view.Context as MainActivity;
                User user = parent.repository.ValidateUser(parameters["Email"], parameters["Password"]);

                if (user != null)
                {
                    var template = new Page2View() { Model = user };
                    var page = template.GenerateString();
                    view.LoadDataWithBaseURL("file:///android_assets", page, "text/html", "UTF-8", null);
                    return true;
                }
                else
                    return false;
            }

            return base.ShouldOverrideUrlLoading(view, url);
        }
    }
}