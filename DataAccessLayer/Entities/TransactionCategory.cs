using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class TransactionCategory
    {
        public TransactionCategory()
        {
            Transactions = new HashSet<Transaction>();
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("UserId")]
        public int? UserId { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
