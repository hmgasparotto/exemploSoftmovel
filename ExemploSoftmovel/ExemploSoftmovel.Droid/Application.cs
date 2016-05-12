using System;

using Android.App;
using Android.Runtime;
using ExemploSoftmovel.Repository;
using ExemploSoftmovel.Shared;

namespace ExemploSoftmovel.Droid
{
    [Application]
    public class ExemploSoftmovelApp : Application
    {
        public static ExemploSoftmovelApp Current { get; set; }
        public UserRepository repository { get; set; }

        public ExemploSoftmovelApp(IntPtr handle, JniHandleOwnership owner) : base(handle, owner)
        {
            Current = this;
        }

        public override void OnCreate()
        {
            base.OnCreate();

            repository = DatabaseConnection.StartDatabase();
        }
    }
}