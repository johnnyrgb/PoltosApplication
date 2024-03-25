using BusinessLogicLayer.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface ITransactionCategoryService
    {
        List<TransactionCategoryDTO> GetTransactionCategories();
        TransactionCategoryDTO GetTransactionCategory(int id);
        void CreateTransactionCategory(TransactionCategoryDTO transactionCategoryDTO);
        void UpdateTransactionCategory(TransactionCategoryDTO transactionCategoryDTO);
        void DeleteTransactionCategory(int id);
        public List<TransactionCategoryDTO> GetTransactionCategoriesByUserId(int userId);

        public List<TransactionCategoryDTO> GetOnlyUserCategories(int userId);

        //public List<TransactionCategoryDTO> GetTransactionCategoriesInDateRange(DateTime startDate, DateTime endDate);
        //public List<TransactionCategoryDTO> GetSliceOfCategories(List<int> ids);

    }
}
