using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        [ForeignKey("TransactionCategoryId")]
        public int TransactionCategoryId { get; set; }
        public virtual TransactionCategory TransactionCategory { get; set; }
        [ForeignKey("AccountId")]
        public int AccountId { get; set; }
        [InverseProperty("Transactions")]
        public virtual Account Account { get; set; }
        [ForeignKey("TransferAccountId")]
        
        public int? TransferAccountId { get; set; }
        [InverseProperty("TransferTransactions")]
        public virtual Account? TransferAccount { get; set; }
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int TransactionType { get; set; } // 1 - доход, 2 - расход, 3 - перевод
    }
}
