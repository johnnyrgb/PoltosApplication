using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class UserRepository : IRepository<User>
    {
        private DatabaseContext database;

        public UserRepository(DatabaseContext database)
        {
            this.database = database;
        }

        public void Create(User user)
        {
            database.Users.Add(user);
        }

        public void Update(User user)
        {
            database.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public void Delete(int id) 
        {
            User user = database.Users.Find(id);
            database.Users.Remove(user);
        }

        public IEnumerable<User> GetAll() 
        {
            return database.Users;
        }

        public User GetItem(int id)
        {
            return database.Users.Find(id);
        }

    }
}
