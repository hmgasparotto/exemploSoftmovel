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
        static object locker = new object();
        private Services.SoftmovelServices service;

        public Database(SQLiteConnection conn)
        {
            database = conn;

            database.CreateTable<User>();

            service = new Services.SoftmovelServices();
        }

        public async void InsertAllFromApi()
        {
            IEnumerable<User> users = await service.GetAllUsersFromApi();
            foreach (User u in users)
            {
                u.Password = "";
                database.Insert(u);
            }
        }

        public IEnumerable<User> GetAllUsers()
        {
            lock(locker)
            {
                return database.Table<User>().ToList(); //(from u in database.Table<User>() select u).ToList();
            }
        }

        public User GetUserById(int id)
        {
            lock (locker)
            {
                return (from u in database.Table<User>() where u.Id == id select u).FirstOrDefault();
            }
        }
    }
}
