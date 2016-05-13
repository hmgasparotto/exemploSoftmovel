using ExemploSoftmovel.Repository;
using System;
using System.IO;

namespace ExemploSoftmovel.Shared
{
	public static class DatabaseConnection
	{
        public static UserRepository StartDatabase()
        {
            string filePath = System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.Personal);
#if __IOS__
            filePath = Path.Combine(filePath, "..", "Library");
#endif
            string path = Path.Combine(filePath, "TesteDB.db3");
            SQLite.SQLiteConnection conn = new
                SQLite.SQLiteConnection(path);

            return new UserRepository(conn);
        }
	}
}

