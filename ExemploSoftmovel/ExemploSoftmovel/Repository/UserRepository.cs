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

        public IEnumerable<User> GetAllUsers()
        {
            return database.GetAllUsers();
        }

        public User GetUserById (int id)
        {
            return database.GetUserById(id);
        }
    }
}
