using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class User
    {
        public User()
        {
            Goals = new HashSet<Goal>();
            Accounts = new HashSet<Account>();
            Transactions = new HashSet<Transaction>();
        }
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public ICollection<Goal> Goals { get; set; }
        public ICollection<Account> Accounts { get; set; }
        public ICollection<Transaction> Transactions { get; set; }

    }
}
