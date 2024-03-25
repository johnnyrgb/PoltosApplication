using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DataTransferObjects
{
    public class AccountDTO
    { 
        public AccountDTO() { }
        public AccountDTO(Account account)
        {
            Id = account.Id;
            Name = account.Name;
            Balance = account.Balance;
            Limit = account.Limit;
            Number = account.Number;
            LimitRenewalDate = account.LimitRenewalDate;
            LimitRenewalFrequency = account.LimitRenewalFrequency;
            UserId = account.UserId;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public double Balance { get; set; }
        public int UserId { get; set; } 
        public double? Limit { get; set; }
        public string? Number { get; set; }
        public DateTime? LimitRenewalDate { get; set; }
        public int? LimitRenewalFrequency { get; set; }

        
    }
}
