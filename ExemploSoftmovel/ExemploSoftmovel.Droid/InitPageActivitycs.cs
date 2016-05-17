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

namespace ExemploSoftmovel.Droid
{
    [Activity(Label = "InitPageActivitycs")]
    public class InitPageActivitycs : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.InitPage);

            string nome = Intent.GetStringExtra("Name");

            TextView lblMessage = FindViewById<TextView>(Resource.Id.lblMessage);
            lblMessage.Text = Resources.GetText(Resource.String.WelcomeMessage)
                + ", " + nome + "!";
        }
    }
}