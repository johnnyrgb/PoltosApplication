using BusinessLogicLayer.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IAccountService
    {
        List<AccountDTO> GetAccounts();
        AccountDTO GetAccount(int id);
        void CreateAccount(AccountDTO accountDTO);
        void UpdateAccount(AccountDTO accountDTO);
        void DeleteAccount(int id);

        // custom
        public List<AccountDTO> GetAccountsByUserId(int userId);
        public void UpdateLimitAccountsByUserId(int userId);

    }
}
