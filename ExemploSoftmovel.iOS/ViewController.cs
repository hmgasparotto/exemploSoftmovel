﻿using System;
using Foundation;
using UIKit;
using ExemploSoftmovel.Models;
using MultiThreading.Controls;
using CoreGraphics;
using System.Threading.Tasks;
using MessageUI;
using MapKit;
using CoreLocation;
using CoreBluetooth;
using CoreFoundation;

namespace ExemploSoftmovel.iOS
{
    public partial class ViewController : UIViewController
    {
        public string Name { get; set; }

        private CBPeripheral peripheral;

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            CBCentralManager bluetoothManager = new CBCentralManager(new SoftmovelCentralManagerDelegate(), DispatchQueue.CurrentQueue);

            bluetoothManager.ConnectedPeripheral += (s, e) => {
                peripheral = e.Peripheral;
                peripheral.Delegate = new SoftmovelPeripheralDelegate();

                foreach(CBService service in peripheral.Services)
                {
                    //service.
                }
            };

            bluetoothManager.ConnectPeripheral(peripheral);

            CLLocationManager manager = new CLLocationManager();
            manager.RequestAlwaysAuthorization();

            CLCircularRegion region = new CLCircularRegion(new CLLocationCoordinate2D(-30.0, -51.0), 1000, "Região circular");
            manager.RegionEntered += delegate 
            {
                new UIAlertView("Entrou na região!", "", null, "OK", null).Show();
            };
            manager.RegionLeft += delegate 
            {
                new UIAlertView("Saiu da região!", "", null, "OK", null).Show();
            };
            if (CLLocationManager.IsMonitoringAvailable(typeof(CLCircularRegion)))
                manager.StartMonitoring(region);

            map.ShowsUserLocation = true;

            UIButton btnEnviar = new UIButton(new CGRect(0, 200, 100, 40));
            btnLogar.TouchUpInside += delegate {
                var emailSender = new MFMailComposeViewController();
                emailSender.SetToRecipients(new string[] { "fernando@softmovel.com.br" });
                emailSender.SetCcRecipients(new string[] { "hmgasparotto@hotmail.com" });
                emailSender.SetBccRecipients(new string[] { "teste@teste.com" });
                emailSender.SetSubject("Teste");
                emailSender.SetMessageBody("TESTE", false);

                emailSender.Finished += (object s, MFComposeResultEventArgs args) => {
                    args.Controller.DismissViewController(false, null);
                };

                this.PresentViewController(emailSender, false, null);
            };

            this.Add(btnEnviar);
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

            var dialog = new LoadingOverlay(new CGRect(0, 0, 100, 200), NSBundle.MainBundle.LocalizedString("ValidandoUsuario", "Validando usuário..."));
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
                new UIAlertView("Erro", NSBundle.MainBundle.LocalizedString("ErrorMessage", "Mensagem de erro de autenticação"), null, "OK", null).Show();
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