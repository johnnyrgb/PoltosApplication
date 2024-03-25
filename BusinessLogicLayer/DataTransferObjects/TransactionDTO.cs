using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DataTransferObjects
{
    public class TransactionDTO
    {
        public TransactionDTO() { }
        public TransactionDTO(Transaction transaction) 
        {
            Id = transaction.Id;
            Date = transaction.Date;
            Amount = transaction.Amount;
            TransactionCategoryId = transaction.TransactionCategoryId;
            AccountId = transaction.AccountId;
            TransferAccountId = transaction.TransferAccountId;
            UserId = transaction.UserId;
            TransactionType = transaction.TransactionType;
        }
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public int TransactionCategoryId { get; set; }
        public int AccountId { get; set; }
        public int? TransferAccountId { get; set; }
        public int UserId { get; set; }
        public int TransactionType { get; set; }
    }
}
