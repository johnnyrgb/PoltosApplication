using BusinessLogicLayer.DataTransferObjects;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BusinessLogicLayer.Services
{
    public class TransactionService : ITransactionService
    {
        private IDbRepository dbRepository;

        public TransactionService(IDbRepository dbRepository)
        {
            this.dbRepository = dbRepository;
        }
        public TransactionDTO GetTransaction(int id)
        {
            return new TransactionDTO(dbRepository.Transactions.GetItem(id));
        }
        public List<TransactionDTO> GetTransactions()
        {
            return dbRepository.Transactions.GetAll().Select(item => new TransactionDTO(item)).ToList();
        }
        public void CreateTransaction(TransactionDTO transactionDTO)
        {
            dbRepository.Transactions.Create(new Transaction()
            {
                Date = transactionDTO.Date,
                Amount = transactionDTO.Amount,
                TransactionCategoryId = transactionDTO.TransactionCategoryId,
                AccountId = transactionDTO.AccountId,
                UserId = transactionDTO.UserId,
                TransactionType = transactionDTO.TransactionType,
            });
            Save();
        }
        public void UpdateTransaction(TransactionDTO transactionDTO)
        {
            Transaction transaction = dbRepository.Transactions.GetItem(transactionDTO.Id);
            transaction.Date = transactionDTO.Date;
            transaction.Amount = transactionDTO.Amount;
            transaction.TransactionCategoryId = transactionDTO.TransactionCategoryId;
        }
        public void DeleteTransaction(int id)
        {
            dbRepository.Transactions.Delete(id);
        }

        public bool Save()
        {
            return dbRepository.Save();
        }

        // custom

        public List<TransactionDTO> GetTransactionsByUserId(int userId)
        {
            return GetTransactions().Where(trans => trans.UserId == userId).ToList();
        }

        public List<TransactionDTO> GetTransactionsInDateRangeByUserId(int userId, DateTime startDate, DateTime endDate)
        {
            return GetTransactionsByUserId(userId).Where(trans => trans.Date.Date >= startDate.Date && trans.Date.Date <= endDate.Date).ToList();
        }
    }
}
