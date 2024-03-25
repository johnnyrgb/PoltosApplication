using BusinessLogicLayer.DataTransferObjects;
using BusinessLogicLayer.Interfaces;
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
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Windows.Input;
using System.Xml.Linq;
using MaterialDesignThemes.Wpf;

namespace PresentationLayer.ViewModels
{
    public class TransactionWindowViewModel : ViewModel
    {
        #region Services
        private IAccountService _accountService;
        private ITransactionService _transactionService;
        private ITransactionCategoryService _transactionCategoryService;
        private IGoalService _goalService;
        private NotificationManager _notificationManager = new NotificationManager();
        #endregion

        #region Fields
        private TransactionWindow _transactionWindow;
        private int _accountId;
        private int? _transferAccountId;
        private int _transactionCategoryId;
        private DateTime _date = DateTime.Now;
        private double _amount;
        private int _userId;
        private int _transactionType;
        private int? _transferGoalId;


        public int UserId
        {
            get { return _userId; }
            set { _userId = value; OnPropertyChanged(); }
        }
        public int AccountId
        {
            get { return _accountId; }
            set { _accountId = value; OnPropertyChanged(); }
        }

        public int? TransferAccountId
        {
            get { return _transferAccountId; }
            set { _transferAccountId = value; OnPropertyChanged(); }
        }
        public int? TransferGoalId
        {
            get { return _transferGoalId; }
            set { _transferGoalId = value; OnPropertyChanged(); }
        }

        public int TransactionCategoryId
        {
            get { return _transactionCategoryId; }
            set { _transactionCategoryId = value; OnPropertyChanged(); }
        }

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; OnPropertyChanged(); }
        }
        public double Amount
        {
            get { return _amount; }
            set { _amount = value; OnPropertyChanged(); }
        }

        public int TransactionType
        {
            get { return _transactionType; }
            set { _transactionType = value; OnPropertyChanged(); }
        }

        public ObservableCollection<AccountDTO> Accounts { get; set; }
        public ObservableCollection<TransactionCategoryDTO> TransactionCategories { get; set; }

        public ObservableCollection<GoalDTO> Goals { get; set; }
        // public ObservableCollection<GoalDTO> Goals { get; set; }   
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
                        TransactionDTO transaction = new TransactionDTO();
                        if (Amount != null && Amount != 0 && Date >= new DateTime(1970,1,1) && AccountId != null && AccountId != 0 && TransactionCategoryId != null && TransactionCategoryId != 0)
                        {
                            
                            
                            transaction.Amount = Amount;
                            transaction.Date = Date;
                            transaction.TransactionCategoryId = TransactionCategoryId;
                            transaction.AccountId = AccountId;
                            transaction.UserId = UserId;
                            if (TransactionCategoryId == 2) // перевод
                            {
                                if (TransferAccountId != null && TransferAccountId != 0)
                                {
                                    TransactionType = 3;
                                    transaction.TransactionType = TransactionType;
                                    transaction.Amount = -Math.Abs(Amount);
                                    transaction.TransferAccountId = TransferAccountId;
                                    TransactionDTO transferTransaction = new TransactionDTO();
                                    transferTransaction.Amount = Math.Abs(Amount);
                                    transferTransaction.Date = Date;
                                    transferTransaction.TransactionCategoryId = TransactionCategoryId;
                                    transferTransaction.AccountId = (int)TransferAccountId;
                                    transferTransaction.TransferAccountId = AccountId;
                                    transferTransaction.UserId = UserId;
                                    transferTransaction.TransactionType = TransactionType;
                                    _transactionService.CreateTransaction(transaction);
                                    _transactionService.CreateTransaction(transferTransaction);

                                    AccountDTO account = Accounts[AccountId - 1];
                                    AccountDTO transferAccount = Accounts[(int)TransferAccountId - 1];
                                    account.Balance += -Amount;
                                    transferAccount.Balance += Amount;
                                    _accountService.UpdateAccount(account);
                                    _accountService.UpdateAccount(transferAccount);
                                    _transactionWindow.DialogResult = true;
                                }
                                else
                                    _notificationManager.Show("Некорректные данные!", NotificationType.Warning, "WindowArea");

                            }
                            else if (TransactionCategoryId == 1) // сбережения на цель
                            {
                                if (TransferGoalId != null ) {
                                    TransactionType = 2;
                                    transaction.TransactionType = TransactionType;
                                    transaction.Amount = -Math.Abs(Amount);
                                    transaction.TransactionCategoryId = TransactionCategoryId;

                                    GoalDTO goal = Goals[(int)TransferGoalId - 1];
                                    goal.Balance += Math.Abs(Amount);
                                    _transactionService.CreateTransaction(transaction);
                                    _goalService.UpdateGoal(goal);
                                    _transactionWindow.DialogResult = true;
                                }
                                else
                                    _notificationManager.Show("Некорректные данные!", NotificationType.Warning, "WindowArea");
                            }
                            else
                            {
                                TransactionType = Amount > 0 ? 1 : 2;
                                transaction.TransactionType = TransactionType;
                                transaction.TransferAccountId = null;
                                _transactionService.CreateTransaction(transaction);
                                AccountDTO account = Accounts[AccountId - 1];
                                account.Balance += Amount;
                                _accountService.UpdateAccount(account);
                                _transactionWindow.DialogResult = true;
                            }
                        }
                        else
                        {
                            _notificationManager.Show("Некорректные данные!", NotificationType.Warning, "WindowArea");
                        }
                    }
                    catch (Exception ex)
                    {
                        _transactionWindow.DialogResult = false;
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
                    _transactionWindow.DialogResult = false;
                });
            }
        }
        #endregion

        public TransactionWindowViewModel(TransactionWindow transactionWindow, 
                                          IAccountService accountService,
                                          ITransactionService transactionService,
                                          ITransactionCategoryService transactionCategoryService, 
                                          IGoalService goalService,
                                          int loggedUserId)
        {
            UserId = loggedUserId;
            _transactionWindow = transactionWindow;
            _accountService = accountService;
            _transactionCategoryService = transactionCategoryService;
            _transactionService = transactionService;
            _goalService = goalService;
            Goals = new ObservableCollection<GoalDTO>(_goalService.GetGoalsByUserId(UserId));

            Accounts = new ObservableCollection<AccountDTO>(_accountService.GetAccountsByUserId(UserId));
            TransactionCategories = new ObservableCollection<TransactionCategoryDTO>(_transactionCategoryService.GetTransactionCategoriesByUserId(UserId));
           
        }
    }
}
