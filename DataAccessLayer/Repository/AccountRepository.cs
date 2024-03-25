using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    // base
    public class AccountRepository : IRepository<Account>
    {
        private DatabaseContext database;

        public AccountRepository(DatabaseContext database)
        {
            this.database = database;
        }

        public void Create(Account account)
        {
            database.Accounts.Add(account);
        }

        public void Update(Account account)
        {
            database.Entry(account).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public void Delete(int id)
        {
            Account account = database.Accounts.Find(id);
            database.Accounts.Remove(account);
        }

        public IEnumerable<Account> GetAll()
        {
            return database.Accounts;
        }

        public Account GetItem(int id)
        {
            return database.Accounts.Find(id);
        }
    }
}
