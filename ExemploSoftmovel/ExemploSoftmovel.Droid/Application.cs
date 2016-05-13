using System;

using Android.App;
using Android.Runtime;
using ExemploSoftmovel.Repository;
using ExemploSoftmovel.Shared;
using ExemploSoftmovel.Services;

namespace ExemploSoftmovel.Droid
{
    [Application]
    public class ExemploSoftmovelApp : Application
    {
        public static ExemploSoftmovelApp Current { get; set; }
        public UserRepository repository { get; set; }
        public SoftmovelServices services { get; set; }

        public ExemploSoftmovelApp(IntPtr handle, JniHandleOwnership owner) : base(handle, owner)
        {
            Current = this;
        }

        public override void OnCreate()
        {
            base.OnCreate();

            repository = DatabaseConnection.StartDatabase();
            services = new SoftmovelServices();
        }
    }
}