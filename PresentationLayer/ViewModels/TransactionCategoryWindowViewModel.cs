using BusinessLogicLayer.DataTransferObjects;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using DataAccessLayer.Entities;
using Notification.Wpf;
using PresentationLayer.Util.Navigation;
using PresentationLayer.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PresentationLayer.ViewModels
{
    public class TransactionCategoryWindowViewModel : ViewModel
    {
        #region Services
        private ITransactionCategoryService _transactionCategoryService;
        private NotificationManager _notificationManager = new NotificationManager();
        #endregion

        #region Fields
        private TransactionCategoryWindow _transactionCategoryWindow;
        private string _name;
        private int _userId;


        public int UserId
        {
            get { return _userId; }
            set { _userId = value; OnPropertyChanged(); }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }

        public ObservableCollection<TransactionCategoryDTO> TransactionCategories { get; set; }
        #endregion
        #region Commands
        private ICommand _submitCommand;
        public ICommand SubmitCommand
        {
            get
            {
                return _submitCommand ??= new RelayCommand(obj =>
                {
                    try
                    {
                        TransactionCategoryDTO transaction = new TransactionCategoryDTO();
                        if (Name != null && Name != "")
                        {
                            transaction.Name = Name;
                            transaction.UserId = UserId;
                            _transactionCategoryWindow.DialogResult = true;
                            _transactionCategoryService.CreateTransactionCategory(transaction);
                        }
                        else
                        {
                            _notificationManager.Show("Некорректные данные!", NotificationType.Warning, "WindowArea");
                        }
                    }
                    catch (Exception ex)
                    {
                        _transactionCategoryWindow.DialogResult = false;
                    }
                });
            }
        }
        private ICommand _cancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                return _cancelCommand ??= new RelayCommand(obj =>
                {
                    _transactionCategoryWindow.DialogResult = false;
                });
            }
        }
        #endregion
        public TransactionCategoryWindowViewModel(TransactionCategoryWindow categoryWindow, ITransactionCategoryService transactionCategoryService, int loggedUserId)
        {
            UserId = loggedUserId;
            _transactionCategoryService = transactionCategoryService;
            _transactionCategoryWindow = categoryWindow;
        }
    }
}
