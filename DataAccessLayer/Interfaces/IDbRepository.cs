using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IDbRepository
    {
        IRepository<User> Users { get; }
        IRepository<Goal> Goals { get; }
        IRepository<Transaction> Transactions { get; }
        IRepository<Account> Accounts { get; }
        IRepository<TransactionCategory> TransactionCategories { get; }
        IReportRepository Reports { get; }
        bool Save();
    }
}
