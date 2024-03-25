using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DataTransferObjects
{
    public class TransactionCategoryDTO
    {
        public TransactionCategoryDTO() { }
        public TransactionCategoryDTO(TransactionCategory transactionCategory) 
        {
            Id = transactionCategory.Id;
            Name = transactionCategory.Name;
            UserId = transactionCategory.UserId;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? UserId { get; set; }
    }
}
