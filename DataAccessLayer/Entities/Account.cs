using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Account
    {
        public Account()
        {
            Transactions = new HashSet<Transaction>();
            TransferTransactions = new HashSet<Transaction>();
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public double Balance { get; set; }
        public double? Limit { get; set; }
        public string? Number { get; set; }
        public DateTime? LimitRenewalDate { get; set; }
        public int? LimitRenewalFrequency { get; set; }
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        [InverseProperty("Accounts")]
        public User User { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
        public ICollection<Transaction> TransferTransactions { get; set; }
    }
}
