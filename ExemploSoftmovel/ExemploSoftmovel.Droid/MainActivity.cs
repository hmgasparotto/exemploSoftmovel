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
using System.Threading;
using System.Threading.Tasks;
using Android.Hardware.Usb;
using Java.Nio;
using Android.Locations;
using Android.Bluetooth;
using Java.Util;

namespace ExemploSoftmovel.Droid
{
	[Activity (Label = "ExemploSoftmovel.Droid", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity, ILocationListener
	{
        LocationManager locManager;
        Location location;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

            locManager = (LocationManager)GetSystemService(LocationService);
            Criteria criteria = new Criteria
            {
                Accuracy = Accuracy.Fine
            };
            locManager.RequestLocationUpdates(1000, 1, criteria, this, null);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button> (Resource.Id.btnLogar);
            EditText txtEmail = FindViewById<EditText>(Resource.Id.txtEmail);
            EditText txtPassword = FindViewById<EditText>(Resource.Id.txtPassword);
            Button btnCarregar = FindViewById<Button>(Resource.Id.btnCarregar);
            btnCarregar.Click += async delegate
            {
                //Carregamento dos dados da API
                var dialogAPI = ProgressDialog.Show(this, "Aguarde...", "Carregando usuários...");
                await Task.Run(() => ExemploSoftmovelApp.Current.repository.InsertAllFromApi());
                dialogAPI.Hide();
            };
			
			button.Click += async delegate
            {
                var dialog = ProgressDialog.Show(this, "Aguarde...", "Buscando usuários...");

                User user = null;
                await Task.Run(() => user = ExemploSoftmovelApp.Current.repository.ValidateUser(txtEmail.Text, txtPassword.Text));
                dialog.Hide();

                if (user != null)
                {
                    var intent = new Intent(this, typeof(InitPageActivitycs));
                    intent.PutExtra("Name", user.Name);
                    StartActivity(intent);
                }
                else
                {
                    Toast.MakeText(this,
                        Resources.GetText(Resource.String.ErrorMessage),
                        ToastLength.Short).Show();
                }
            };

            Button btnPeriferico = FindViewById<Button>(Resource.Id.btnPeriferico);
            btnPeriferico.Click += delegate {
                var usbManager = (UsbManager)this.ApplicationContext.GetSystemService(Context.UsbService);

                var usbDevice = usbManager.DeviceList.FirstOrDefault();
                if (usbDevice.Value == null)
                { 
                    return;
                }

                var usbDeviceKey = usbDevice.Key;
                var usbDeviceValue = usbDevice.Value; //UsbDevice

                if (!usbManager.HasPermission(usbDeviceValue))
                {
                    usbManager.RequestPermission(usbDeviceValue, null);
                }

                var usbDeviceConnection = usbManager.OpenDevice(usbDeviceValue);

                using (var usbInterface = usbDeviceValue.GetInterface(0))
                {
                    using (var usbEndpoint = usbInterface.GetEndpoint(0))
                    {
                        UsbRequest usbRequest = new UsbRequest();
                        usbRequest.Queue((ByteBuffer)new byte[] { 1, 2, 3, 4 }, 4);
                        //usbDeviceConnection.BulkTransfer(usbEndpoint, new byte[] { 1, 2, 3, 4, 5 }, 5, 60000);
                    }
                }
            };

            Button btnLocation = FindViewById<Button>(Resource.Id.btnLocation);
            btnLocation.Click += async delegate
            {
                Geocoder geocoder = new Geocoder(this);

                if (location != null)
                {
                    var addressList = await geocoder.GetFromLocationAsync(location.Latitude, location.Longitude, 1);
                    //geocoder.GetFromLocationNameAsync();
                    var address = addressList.FirstOrDefault();
                    Toast.MakeText(this, address.GetAddressLine(0), ToastLength.Long).Show();
                }
                else
                {
                    Toast.MakeText(this, "Location nulo!", ToastLength.Short).Show();
                }
            };

            Button btnBluetooth = FindViewById<Button>(Resource.Id.btnBluetooth);
            btnBluetooth.Click += async delegate
            {
                BluetoothAdapter adapter = BluetoothAdapter.DefaultAdapter;
                if (adapter != null)
                {
                    Toast.MakeText(this, "Adaptador Bluetooth não disponível!", ToastLength.Long);
                }
                if (!adapter.IsEnabled)
                {
                    Toast.MakeText(this, "Adaptador Bluetooth não habilitado!", ToastLength.Long);
                }

                BluetoothDevice device = adapter.BondedDevices.Where(m => m.Name == "MPT-III").FirstOrDefault();

                var socket = device.CreateRfcommSocketToServiceRecord(UUID.FromString("00001101-0000-1000-8000-00805f9b34fb"));

                await socket.ConnectAsync();

                await socket.InputStream.ReadAsync(new byte[8], 0, 8);
                await socket.OutputStream.WriteAsync(new byte[8], 0, 8);
            };
		}

        public void OnLocationChanged(Location location)
        {
            this.location = location;
        }

        public void OnProviderDisabled(string provider)
        {
            throw new NotImplementedException();
        }

        public void OnProviderEnabled(string provider)
        {
            throw new NotImplementedException();
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
            throw new NotImplementedException();
        }
    }
}