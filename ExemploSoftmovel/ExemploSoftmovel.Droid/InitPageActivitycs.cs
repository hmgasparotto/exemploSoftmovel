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
using Android.Telephony;

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

            Button btnEnviar = FindViewById<Button>(Resource.Id.btnEnviar);
            btnEnviar.Click += delegate
            {
                TextView txtTextoEmail = FindViewById<TextView>(Resource.Id.txtTextoEmail);

                var intent = new Intent(Intent.ActionSend);

                intent.PutExtra(Intent.ExtraEmail, new string[] { "fernando@softmovel.com.br", "teste@teste.com" });
                intent.PutExtra(Intent.ExtraCc, "hmgasparotto@hotmail.com");
                intent.PutExtra(Intent.ExtraText, txtTextoEmail.Text);
                intent.PutExtra(Intent.ExtraSubject, "Teste");
                //intent.PutExtra(Intent.ExtraStream, "file:///mnt/sdcard/algum_arquivo.pdf");

                intent.SetType("message/rfc822");

                StartActivity(Intent.CreateChooser(intent, "Enviar email..."));
                
            };

            Button btnEnviarSMS = FindViewById<Button>(Resource.Id.btnEnviarSMS);
            btnEnviarSMS.Click += delegate
            {
                TextView txtTexto = FindViewById<TextView>(Resource.Id.txtTextoEmail);

                SmsManager.Default.SendTextMessage("5554", null, txtTexto.Text, null, null);
            };
        }
    }
}