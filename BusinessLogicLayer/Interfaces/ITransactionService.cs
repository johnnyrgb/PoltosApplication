using BusinessLogicLayer.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface ITransactionService
    {
        List<TransactionDTO> GetTransactions();
        TransactionDTO GetTransaction(int id);
        void CreateTransaction(TransactionDTO transactionDTO);
        void UpdateTransaction(TransactionDTO transactionDTO);
        void DeleteTransaction(int id);

        public List<TransactionDTO> GetTransactionsByUserId(int userId);
        public List<TransactionDTO> GetTransactionsInDateRangeByUserId(int userId, DateTime startDate, DateTime endDate);

    }
}
