using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class TransactionCategoryRepository : IRepository<TransactionCategory>
    {
        private DatabaseContext database;

        public TransactionCategoryRepository(DatabaseContext database)
        {
            this.database = database;
        }
        public void Create(TransactionCategory transactionCategory)
        {
            database.TransactionCategories.Add(transactionCategory);
        }

        public void Update(TransactionCategory transactionCategory)
        {
            database.Entry(transactionCategory).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public void Delete(int id)
        {
            TransactionCategory transactionCategory = database.TransactionCategories.Find(id);
            database.TransactionCategories.Remove(transactionCategory);
        }

        public IEnumerable<TransactionCategory> GetAll()
        {
            return database.TransactionCategories;
        }

        public TransactionCategory GetItem(int id)
        {
            return database.TransactionCategories.Find(id);
        }
    }
}
