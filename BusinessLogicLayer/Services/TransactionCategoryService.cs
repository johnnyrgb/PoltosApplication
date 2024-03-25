using BusinessLogicLayer.DataTransferObjects;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class TransactionCategoryService : ITransactionCategoryService
    {
        private IDbRepository dbRepository;

        public TransactionCategoryService(IDbRepository dbRepository)
        {
            this.dbRepository = dbRepository;
        }
        public TransactionCategoryDTO GetTransactionCategory(int id)
        {
            return new TransactionCategoryDTO(dbRepository.TransactionCategories.GetItem(id));
        }
        public List<TransactionCategoryDTO> GetTransactionCategories()
        {
            return dbRepository.TransactionCategories.GetAll().Select(item => new TransactionCategoryDTO(item)).ToList();
        }
        public void CreateTransactionCategory(TransactionCategoryDTO transactionCategoryDTO)
        {
            dbRepository.TransactionCategories.Create(new TransactionCategory()
            {
                Name = transactionCategoryDTO.Name,
                UserId = transactionCategoryDTO.UserId,
            });
            Save();
        }
        public void UpdateTransactionCategory(TransactionCategoryDTO transactionCategoryDTO)
        {
            TransactionCategory transactionCategory = dbRepository.TransactionCategories.GetItem(transactionCategoryDTO.Id);
            transactionCategory.Name = transactionCategoryDTO.Name;
            transactionCategory.UserId = transactionCategoryDTO.UserId;
            Save();
        }
        public void DeleteTransactionCategory(int id)
        {
            dbRepository.TransactionCategories.Delete(id);
        }
        public bool Save()
        {
            return dbRepository.Save();
        }

        // custom
        public List<TransactionCategoryDTO> GetTransactionCategoriesByUserId(int userId)
        {
            var categories = GetTransactionCategories();
            var userCategories = categories.Where(cat => cat.UserId == userId).ToList();
            var baseCategories = categories.Where(cat => cat.UserId == null).ToList();

            baseCategories.AddRange(userCategories);
            return baseCategories;
        }

        public List<TransactionCategoryDTO> GetOnlyUserCategories(int userId)
        {
            return GetTransactionCategoriesByUserId(userId).Where(cat => cat.UserId == userId).ToList();
        }

        //public List<TransactionCategoryDTO> GetSliceOfCategories(List<int> ids)
        //{
        //    var categories = GetTransactionCategories();
        //    var filteredCategories = categories.Where(x => ids.Contains(x.Id)).ToList();
        //}
    }
}
