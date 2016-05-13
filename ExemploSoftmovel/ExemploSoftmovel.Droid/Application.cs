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
using ExemploSoftmovel.Repository;
using System.IO;

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

            string path = Path.Combine(
                System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.Personal),
                "TesteDB.db3");
            SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(path);

            repository = new UserRepository(conn);
        }
    }
}