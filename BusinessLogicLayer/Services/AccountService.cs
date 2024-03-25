using BusinessLogicLayer.DataTransferObjects;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class AccountService : IAccountService
    {
        // base
        private IDbRepository dbRepository;

        public AccountService(IDbRepository dbRepository)
        {
            this.dbRepository = dbRepository;
        }
        public AccountDTO GetAccount(int id)
        {
            return new AccountDTO(dbRepository.Accounts.GetItem(id));
        }
        public List<AccountDTO> GetAccounts()
        {
            return dbRepository.Accounts.GetAll().Select(item => new AccountDTO(item)).ToList();
        }
        public void CreateAccount(AccountDTO accountDTO)
        {
            dbRepository.Accounts.Create(new Account()
            {
                Name = accountDTO.Name,
                Balance = accountDTO.Balance,
                Limit = accountDTO.Limit,
                Number = accountDTO.Number,
                LimitRenewalDate = accountDTO.LimitRenewalDate,
                LimitRenewalFrequency = accountDTO.LimitRenewalFrequency,
                UserId = accountDTO.UserId,
            }); ;
            Save();
        }
        public void UpdateAccount(AccountDTO accountDTO)
        {
            Account account = dbRepository.Accounts.GetItem(accountDTO.Id);
            account.Name = accountDTO.Name;
            account.Balance = accountDTO.Balance;
            account.Limit = accountDTO.Limit;
            account.Number = accountDTO.Number;
            account.LimitRenewalDate = accountDTO.LimitRenewalDate;
            account.LimitRenewalFrequency = accountDTO.LimitRenewalFrequency;
            account.UserId = accountDTO.UserId;     
            Save();
        }
        public void DeleteAccount(int id)
        {
            dbRepository.Accounts.Delete(id);
        }

        public bool Save()
        {
            return dbRepository.Save();
        }

        //custom
        public List<AccountDTO> GetAccountsByUserId(int userId)
        {
            return GetAccounts().Where(acc => acc.UserId == userId).ToList();
        }

        public void UpdateLimitAccountsByUserId(int userId)
        {
            var accounts = GetAccountsByUserId(userId).Where(acc => acc.Limit != null).ToList();
            foreach (AccountDTO account in accounts) 
            {
                if (account.LimitRenewalDate != null && account.LimitRenewalFrequency != null)
                {
                    DateTime date = (DateTime)account.LimitRenewalDate;
                    if (date == DateTime.Now)
                    {
                        account.LimitRenewalDate = date.AddDays((double)account.LimitRenewalFrequency);
                        account.Balance += 100000;
                        UpdateAccount(account);
                    }
                    if (date < DateTime.Now)
                    {
                        TimeSpan difference = DateTime.Now.Date.Subtract(date.Date);
                        int times = (int)difference.Days / (int)account.LimitRenewalFrequency;
                        account.LimitRenewalDate = date.AddDays((times + 1) * (double)account.LimitRenewalFrequency);
                        account.Balance += 100000;
                        UpdateAccount(account);
                    }
                }
                
            }
        }
    }
}
