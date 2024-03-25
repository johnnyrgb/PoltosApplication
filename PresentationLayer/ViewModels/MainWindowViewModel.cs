using Autofac;
using BusinessLogicLayer.DataTransferObjects;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using DataAccessLayer.Entities;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Extensions;
using Notification.Wpf;
using PresentationLayer.Util;
using PresentationLayer.Util.Navigation;
using PresentationLayer.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using System.Xml.Linq;

namespace PresentationLayer.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        #region Services
        private IReportService _reportService;
        private IUserService _userService;
        private IGoalService _goalService;
        private IAccountService _accountService;
        private ITransactionService _transactionService;
        private ITransactionCategoryService _transactionCategoryService;
        private NotificationManager _notificationManager = new NotificationManager();
        #endregion

        #region Windows
        private MainWindow _mainWindow;
        private AccountWindow _accountWindow;
        private EditAccountWindow _editAccountWindow;
        private TransactionWindow _transactionWindow;
        private TransactionCategoryWindow _transactionCategoryWindow;
        private GoalWindow _goalWindow;
        private EditGoalWindow _editGoalWindow;
        #endregion

        #region Users
        private int _loggedUserId;
        public int LoggedUserId
        {
            get { return _loggedUserId; }
            private set { _loggedUserId = value; OnPropertyChanged(); }
        }
        #endregion

        #region Goals
        private ObservableCollection<GoalDTO> _goals;
        public ObservableCollection<GoalDTO> Goals
        { 
            get { return _goals; } 
            set { _goals = value; OnPropertyChanged(); }
        }
        private ICommand _addNewGoalCommand;
        public ICommand AddNewGoalCommand
        {
            get
            {
                return _addNewGoalCommand ??= new RelayCommand(obj =>
                {
                    _goalWindow = new GoalWindow(_goalService, LoggedUserId);
                    var result = _goalWindow.ShowDialog();
                    if (result == true)
                    {
                        Goals = new ObservableCollection<GoalDTO>(_goalService.GetGoalsByUserId(LoggedUserId));
                        _notificationManager.Show("Успешно!", "Новый счет создан!", NotificationType.Success, "WindowArea");
                    }
                });
            }
        }
        private ICommand _editGoalCommand;

        public ICommand EditGoalCommand
        {
            get
            {
                return _editGoalCommand ??= new RelayCommand(goal =>
                {
                    _editGoalWindow = new EditGoalWindow(_goalService, LoggedUserId, (GoalDTO)goal);
                    var result = _editGoalWindow.ShowDialog();
                    if (result == true)
                    {
                        Goals = new ObservableCollection<GoalDTO>(_goalService.GetGoalsByUserId(LoggedUserId));
                        _notificationManager.Show("Успешно!", "Цель успешно отредактирована", NotificationType.Success, "WindowArea");
                    }
                });
            }
        }

        private ICommand _deleteGoalCommand;
        public ICommand DeleteGoalCommand
        {
            get
            {
                return _deleteGoalCommand ??= new RelayCommand(goal =>
                {
                    if (goal != null)
                    {
                        GoalDTO goalDTO = goal as GoalDTO;
                        _goalService.DeleteGoal(goalDTO.Id);
                        Goals = new ObservableCollection<GoalDTO>(_goalService.GetGoalsByUserId(LoggedUserId));
                    }

                });
            }
        }
        #endregion

        #region Accounts
        private ObservableCollection<AccountDTO> _accounts;
        public ObservableCollection<AccountDTO> Accounts
        {
            get { return _accounts; }
            set
            {
                _accounts = value;
                OnPropertyChanged();
            }
        }
        public int AccountsCount
        {
            get { return Accounts.Count; }
        }

        private ICommand _addNewAccountCommand;
        public ICommand AddNewAccountCommand
        {
            get
            {
                return _addNewAccountCommand ??= new RelayCommand(obj =>
                    {
                        _accountWindow = new AccountWindow(_accountService, LoggedUserId);
                        var result = _accountWindow.ShowDialog();
                        if (result == true)
                        {
                            Accounts = new ObservableCollection<AccountDTO>(_accountService.GetAccountsByUserId(LoggedUserId));
                            Transactions = new ObservableCollection<TransactionDTO>(_transactionService.GetTransactionsInDateRangeByUserId(LoggedUserId, StartDate, EndDate));
                            _notificationManager.Show("Успешно!", "Новый счет создан!", NotificationType.Success, "WindowArea");
                        }
                    });
            }
        }

        private ICommand _editAccountCommand;

        public ICommand EditAccountCommand
        {
            get
            {
                return _editAccountCommand ??= new RelayCommand(account =>
                {
                    _editAccountWindow = new EditAccountWindow(_accountService, LoggedUserId, (AccountDTO)account);
                    var result = _editAccountWindow.ShowDialog();
                    if (result == true)
                    {
                        Accounts = new ObservableCollection<AccountDTO>(_accountService.GetAccountsByUserId(LoggedUserId));
                        Transactions = new ObservableCollection<TransactionDTO>(_transactionService.GetTransactionsInDateRangeByUserId(LoggedUserId, StartDate, EndDate));
                        _notificationManager.Show("Успешно!", "Счет успешно отредактирован!", NotificationType.Success, "WindowArea");
                    }
                });
            }
        }
        #endregion

        #region Transactions
        public ObservableCollection<string> SortingMethods { get; set; } = 
            new ObservableCollection<string>{ "По убыванию", "По возрастанию", "Сначала новые", "Сначала старые" };
        private int _selectedSortingMethod;

        private TransactionDTO _selectedTransaction;

        public TransactionDTO SelectedTransaction
        {
            get { return _selectedTransaction; }
            set { _selectedTransaction = value; OnPropertyChanged(); }
        }
        
        private ObservableCollection<TransactionDTO> _transactions;

        public ObservableCollection<TransactionDTO> Transactions
        {
            get { return _transactions; }
            set { _transactions = value; OnPropertyChanged(); }
        }

        private DateTime _startDate = DateTime.Now;
        public DateTime StartDate
        {
            get { return _startDate; }
            set { _startDate = value; OnPropertyChanged(); }
        }

        private DateTime _endDate = DateTime.Now;
        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; OnPropertyChanged(); }
        }

        private double _totalIncomes;

        public double TotalIncomes
        {
            get { return _totalIncomes; }
            set { _totalIncomes = value; OnPropertyChanged(); }
        }
        private double _totalExpenses;

        public double TotalExpenses
        {
            get { return _totalExpenses; }
            set { _totalExpenses = value; OnPropertyChanged(); }
        }

        private ObservableCollection<ISeries> _incomesSeries;
        public ObservableCollection<ISeries> IncomesSeries 
        { 
            get { return _incomesSeries; }
            set { _incomesSeries = value; OnPropertyChanged(); }
        }

        //private ObservableCollection<TransactionCategoryDTO> _incomeCategories;
        //public ObservableCollection<TransactionCategoryDTO> IncomeCategories
        //{
        //    get { return _incomeCategories; }
        //    set { _incomeCategories = value; OnPropertyChanged(); }
        //}

        
        private ObservableCollection<ISeries> _expensesSeries;
        public ObservableCollection<ISeries> ExpensesSeries
        {
            get { return _expensesSeries; }
            set { _expensesSeries = value; OnPropertyChanged(); }
        }

        //private ObservableCollection<TransactionCategoryDTO> _expenseCategories;
        //public ObservableCollection<TransactionCategoryDTO> ExpenseCategories
        //{
        //    get { return _expenseCategories; }
        //    set { _expenseCategories = value; OnPropertyChanged(); }
        //}

        private ICommand _addNewTransactionCommand;
        public ICommand AddNewTransactionCommand
        {
            get
            {
                return _addNewTransactionCommand ??= new RelayCommand(obj =>
                {
                    _transactionWindow = new TransactionWindow(_accountService, _transactionService, _transactionCategoryService, _goalService, LoggedUserId);
                    var result = _transactionWindow.ShowDialog();
                    if (result == true)
                    {
                        Accounts = new ObservableCollection<AccountDTO>(_accountService.GetAccountsByUserId(LoggedUserId));
                        Transactions = new ObservableCollection<TransactionDTO>(_transactionService.GetTransactionsByUserId(LoggedUserId));
                        Goals = new ObservableCollection<GoalDTO>(_goalService.GetGoalsByUserId(LoggedUserId));

                        _notificationManager.Show("Успешно!", "Транзакция добавлена", NotificationType.Success, "WindowArea");
                    }
                });
            }
        }

        private ICommand _saveTransactionCommand;

        public ICommand SaveTransactionCommand
        {
            get
            {
                return _saveTransactionCommand ??= new RelayCommand(obj =>
                {
                    TransactionDTO transaction = obj as TransactionDTO;
                    if (transaction != null)
                    {
                        if (transaction.Amount == 0)
                        { _notificationManager.Show("Некорректные данные!", NotificationType.Error, "WindowArea"); }
                        else
                        {
                            transaction.TransactionType = transaction.Amount > 0 ? 1 : 2;
                            _transactionService.UpdateTransaction(transaction);
                            Accounts = new ObservableCollection<AccountDTO>(_accountService.GetAccountsByUserId(LoggedUserId));
                            Transactions = new ObservableCollection<TransactionDTO>(_transactionService.GetTransactionsByUserId(LoggedUserId));
                            Goals = new ObservableCollection<GoalDTO>(_goalService.GetGoalsByUserId(LoggedUserId));
                            _notificationManager.Show("Транзакция обновлена!", NotificationType.Success, "WindowArea");
                        }
                    }
                    else
                        _notificationManager.Show("Выберите транзакцию!", NotificationType.Warning, "WindowArea");
                });
            }
        }

        private ICommand _findTransactionsCommand;

        public ICommand FindTransactionsCommand
        {
            get
            {
                return _findTransactionsCommand ??= new RelayCommand(obj =>
                {
                    Transactions = new ObservableCollection<TransactionDTO>(_transactionService.GetTransactionsInDateRangeByUserId(LoggedUserId, StartDate, EndDate));

                    //List<int> expensesList = Transactions.Where(x => x.TransactionType == 2).Select(x => x.TransactionCategoryId).Distinct().ToList();
                    //List<int> incomesList = Transactions.Where(x => x.TransactionType == 1).Select(x => x.TransactionCategoryId).Distinct().ToList();
                    //IncomeCategories = new ObservableCollection<TransactionCategoryDTO>(_transactionCategoryService.GetSliceOfCategories(incomesList));
                    //ExpenseCategories = new ObservableCollection<TransactionCategoryDTO>(_transactionCategoryService.GetSliceOfCategories(expensesList));
                    //foreach (TransactionCategoryDTO incomeCategory in IncomeCategories)
                    //{
                    //    IncomesSeries.Add(new PieSeries<double> { Name = incomeCategory.Name , Values = new List<double> { } });
                    //}
                    //foreach (TransactionCategoryDTO expenseCategory in ExpenseCategories)
                    //{
                    //    ExpensesSeries.Add(new PieSeries<double> { Name = expenseCategory.Name, Values = new List<double> { } });
                    //}
                    //foreach (PieSeries<double> incomeSeries in IncomesSeries)
                    //{
                    //    foreach (TransactionDTO transaction in Transactions)
                    //    {
                    //        if (incomeSeries.Name == transaction.TransactionCategoryId)
                    //    }
                    //}
                    IncomesSeries.Clear();
                    ExpensesSeries.Clear();
                    TotalExpenses = 0;
                    TotalIncomes = 0;
                    var incomes = Transactions
                            .Where(transaction => transaction.TransactionType == 1)
                            .Join(TransactionCategories,
                                transaction => transaction.TransactionCategoryId,
                                category => category.Id,
                                (transaction, category) => new { Transaction = transaction, Category = category })
                            .GroupBy(x => x.Category.Name)
                            .Select(g => new { Name = g.Key, TotalValue = g.Sum(x => x.Transaction.Amount) }).ToList();
                    var expenses = Transactions
                                  .Where(transaction => transaction.TransactionType == 2)
                                  .Join(TransactionCategories,
                                      transaction => transaction.TransactionCategoryId,
                                      category => category.Id,
                                      (transaction, category) => new { Transaction = transaction, Category = category })
                                  .GroupBy(x => x.Category.Name)
                                  .Select(g => new { Name = g.Key, TotalValue = g.Sum(x => x.Transaction.Amount) }).ToList();
                    foreach (var item in incomes )
                    {
                        IncomesSeries.Add(new PieSeries<double> { Name = item.Name, Values = new[] { item.TotalValue } });
                        TotalIncomes += item.TotalValue;
                    }
                    foreach (var item in expenses )
                    {
                        ExpensesSeries.Add(new PieSeries<double> { Name = item.Name, Values = new[] { Math.Abs(item.TotalValue) } });
                        TotalExpenses += item.TotalValue;
                    }
                });
            }
        }
        #endregion

        #region TransactionCategories
        private ObservableCollection<TransactionCategoryDTO> _transactionCategories;
        public ObservableCollection<TransactionCategoryDTO> TransactionCategories
        {
            get { return _transactionCategories; }
            set { _transactionCategories = value; OnPropertyChanged(); }
        }

        private ObservableCollection<TransactionCategoryDTO> _userCategories;
        public ObservableCollection<TransactionCategoryDTO> UserCategories
        {
            get { return _userCategories; }
            set { _userCategories = value; OnPropertyChanged(); }   
        }

        private TransactionCategoryDTO _selectedCategory;
        public TransactionCategoryDTO SelectedCategory
        {
            get { return _selectedCategory; }
            set { _selectedCategory = value; OnPropertyChanged(); }
        }
        private ICommand _saveTransactionCategories;
        public ICommand SaveTransactionCategories
        {
            get
            {
                return _saveTransactionCategories ??= new RelayCommand(obj =>
                {
                    TransactionCategoryDTO category = obj as TransactionCategoryDTO;
                    if (category != null)
                    {
                        if (category.Name != null && category.Name != "")
                        {
                            _transactionCategoryService.UpdateTransactionCategory(category);
                            TransactionCategories = new ObservableCollection<TransactionCategoryDTO>(_transactionCategoryService.GetTransactionCategoriesByUserId(LoggedUserId));
                            Transactions = new ObservableCollection<TransactionDTO>(_transactionService.GetTransactionsInDateRangeByUserId(LoggedUserId, StartDate, EndDate));

                        }
                        else
                        {
                            _notificationManager.Show("Некорректные данные!", NotificationType.Error, "WindowArea");
                        }
                    }
                    else
                    {
                        _notificationManager.Show("Выберите категорию!", NotificationType.Warning, "WindowArea");
                    }
                });
            }
        }

        private ICommand _addNewTransactionCategoryCommand;
        public ICommand AddNewTransactionCategoryCommand
        {
            get
            {
                return _addNewTransactionCategoryCommand ??= new RelayCommand(obj =>
                {
                    _transactionCategoryWindow = new TransactionCategoryWindow(_transactionCategoryService, LoggedUserId);
                    var result = _transactionCategoryWindow.ShowDialog();
                    if (result == true)
                    {
                        Accounts = new ObservableCollection<AccountDTO>(_accountService.GetAccountsByUserId(LoggedUserId));
                        Transactions = new ObservableCollection<TransactionDTO>(_transactionService.GetTransactionsByUserId(LoggedUserId));
                        TransactionCategories = new ObservableCollection<TransactionCategoryDTO>(_transactionCategoryService.GetTransactionCategoriesByUserId(LoggedUserId));
                        UserCategories = new ObservableCollection<TransactionCategoryDTO>(_transactionCategoryService.GetOnlyUserCategories(LoggedUserId));
                        _notificationManager.Show("Успешно!", "Транзакция добавлена", NotificationType.Success, "WindowArea");
                    }
                });
            }
        }
        #endregion

        #region Reports
        #endregion
        public MainWindowViewModel(MainWindow mainWindow, int userId) 
        {
            _mainWindow = mainWindow;
            LoggedUserId = userId;
            var builder = new ContainerBuilder();
            builder.RegisterModule(new ReposModule("Data Source=F:\\Poltos\\PoltosApplication\\database123.db"));
            var container = builder.Build();
            _reportService = container.Resolve<IReportService>();
            _userService = container.Resolve<IUserService>();
            _goalService = container.Resolve<IGoalService>();
            _accountService = container.Resolve<IAccountService>();
            _transactionService = container.Resolve<ITransactionService>();
            _transactionCategoryService = container.Resolve<ITransactionCategoryService>();
            _accountService.UpdateLimitAccountsByUserId(LoggedUserId);
            Accounts = new ObservableCollection<AccountDTO>(_accountService.GetAccountsByUserId(LoggedUserId));
            Transactions = new ObservableCollection<TransactionDTO>(_transactionService.GetTransactionsInDateRangeByUserId(LoggedUserId, StartDate, EndDate));
            TransactionCategories = new ObservableCollection<TransactionCategoryDTO>(_transactionCategoryService.GetTransactionCategoriesByUserId(LoggedUserId));
            UserCategories = new ObservableCollection<TransactionCategoryDTO>(_transactionCategoryService.GetOnlyUserCategories(LoggedUserId));
            Goals = new ObservableCollection<GoalDTO>(_goalService.GetGoalsByUserId(LoggedUserId));
            ExpensesSeries = new ObservableCollection<ISeries>();
            IncomesSeries = new ObservableCollection<ISeries>();
            
        }
    }
}
