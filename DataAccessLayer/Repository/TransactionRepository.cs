using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DataAccessLayer.Repository
{
    public class TransactionRepository : IRepository<Entities.Transaction>
    {
        private DatabaseContext database;

        public TransactionRepository(DatabaseContext database)
        {
            this.database = database;
        }

        public void Create(Entities.Transaction transaction)
        {
            database.Transactions.Add(transaction);
        }

        public void Update(Entities.Transaction transaction)
        {
            database.Entry(transaction).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public void Delete(int id)
        {
            Entities.Transaction transaction = database.Transactions.Find(id);
            database.Transactions.Remove(transaction);
        }

        public IEnumerable<Entities.Transaction> GetAll()
        {
            return database.Transactions;
        }

        public Entities.Transaction GetItem(int id)
        {
            return database.Transactions.Find(id);
        }
    }
}
