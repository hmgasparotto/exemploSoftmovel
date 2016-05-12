using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using ExemploSoftmovel.Models;

namespace ExemploSoftmovel.Database
{
    public class Database
    {
        private SQLiteConnection database;

        public Database(SQLiteConnection conn)
        {
            database = conn;

            database.CreateTable<User>();
            database.DeleteAll<User>();

            database.Insert(new User()
            {
                Name = "Henrique",
                Email = "hmgasparotto@hotmail.com",
                Password = "teste"
            });
        }

        public IEnumerable<User> GetAllUsers()
        {
            return database.Table<User>().ToList(); //(from u in database.Table<User>() select u).ToList();
        }

        public User GetUserById(int id)
        {
            return (from u in database.Table<User>() where u.Id == id select u).FirstOrDefault();
        }
    }
}
