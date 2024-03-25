using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class DbRepository : IDbRepository
    {
        private DatabaseContext database;
        private UserRepository userRepository;
        private GoalRepository goalRepository;
        private AccountRepository accountRepository;
        private TransactionRepository transactionRepository;
        private TransactionCategoryRepository transactionCategoryRepository;
        private ReportRepository reportRepository;

        public DbRepository(string connectionString)
        {
            database = new DatabaseContext(connectionString);
        }
        public IRepository<User> Users 
        { 
            get 
            {
                if (userRepository == null)
                    userRepository = new UserRepository(this.database);
                return userRepository;
            }

        }

        public IRepository<Goal> Goals 
        {
            get
            {
                if (goalRepository == null)
                    goalRepository = new GoalRepository(this.database);
                return goalRepository;
            }

        }

        public IRepository<Transaction> Transactions
        {
            get
            {
                if (transactionRepository == null)
                    transactionRepository = new TransactionRepository(this.database);
                return transactionRepository;
            }

}
        public IRepository<Account> Accounts
        {
            get
            {
                if (accountRepository == null)
                    accountRepository = new AccountRepository(this.database);
                return accountRepository;
            }

        }


        public IRepository<TransactionCategory> TransactionCategories
        {
            get
            {
                if (transactionCategoryRepository == null)
                    transactionCategoryRepository = new TransactionCategoryRepository(this.database);
                return transactionCategoryRepository;
            }

        }

        public IReportRepository Reports
        {
            get
            {
                if (reportRepository == null)
                    reportRepository = new ReportRepository(this.database);
                return reportRepository;
            }

        }

        public bool Save()
        {
            return database.SaveChanges() > 0;
        }
    }
}
