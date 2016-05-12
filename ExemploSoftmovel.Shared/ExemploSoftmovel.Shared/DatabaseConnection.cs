using ExemploSoftmovel.Repository;
using System;
using System.IO;

namespace ExemploSoftmovel.Shared
{
	public static class DatabaseConnection
    {
		public static UserRepository StartDatabase()
		{
            string folderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string fileName = "TesteDB.db3";
#if __ANDROID__
            string path = Path.Combine(folderPath, fileName);
#else
#if __IOS__
            string path = Path.Combine(folderPath, "..", "Library", fileName);
#else
            string path = Path.Combine(ApplicationData.Current.LocalFolder.Path, sqliteFilename);
#endif
#endif
            SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(path);
            var repository = new UserRepository(conn);
            return repository;
        }
	}
}

