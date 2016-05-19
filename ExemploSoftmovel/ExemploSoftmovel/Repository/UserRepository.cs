using ExemploSoftmovel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExemploSoftmovel.Repository
{
    public class UserRepository
    {
        private Database.Database database;

        public UserRepository(SQLite.SQLiteConnection conn)
        {
            database = new Database.Database(conn);
        }

        public void InsertAllFromApi()
        {
            database.InsertAllFromApi();
        }

        public IEnumerable<User> GetAllUsers()
        {
            return database.GetAllUsers();
        }

        public User GetUserById (int id)
        {
            return database.GetUserById(id);
        }

        public User ValidateUser(string email, string senha)
        {
            var users = GetAllUsers();
            users = users.Where(m => m.Email == email && m.Password == senha);

            if (users.Count() > 0)
            {
                return users.FirstOrDefault();
            }
            return null;
        }
    }
}
