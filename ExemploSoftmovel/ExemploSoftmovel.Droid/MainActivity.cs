using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ExemploSoftmovel.Repository;
using System.Collections.Generic;
using System.IO;
using ExemploSoftmovel.Models;
using System.Linq;

namespace ExemploSoftmovel.Droid
{
	[Activity (Label = "ExemploSoftmovel.Droid", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button> (Resource.Id.btnLogar);
            EditText txtEmail = FindViewById<EditText>(Resource.Id.txtEmail);
            EditText txtPassword = FindViewById<EditText>(Resource.Id.txtPassword);
			
			button.Click += async delegate {
                /*IEnumerable<User> users = ExemploSoftmovelApp.Current.repository.GetAllUsers()
                    .Where(u => u.Email == txtEmail.Text && u.Password == txtPassword.Text);*/
                //Abre loader
                IEnumerable<User> users = await ExemploSoftmovelApp.Current.services.GetAllUsersFromApi();
                users = users.Where(u => u.Email == txtEmail.Text);
                if (users.Count() > 0)
                {
                    var intent = new Intent(this, typeof(InitPageActivitycs));
                    intent.PutExtra("Name", users.FirstOrDefault().Name);
                    StartActivity(intent);
                }
                else
                {
                    Toast.MakeText(this, 
                        "Email ou senha incorretos!", 
                        ToastLength.Short).Show();
                }
			};
		}
	}
}


