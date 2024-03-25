using BusinessLogicLayer.DataTransferObjects;
using BusinessLogicLayer.Interfaces;
using PresentationLayer.Util.Navigation;
using PresentationLayer.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Notification.Wpf;

namespace PresentationLayer.ViewModels
{
    public class AccountWindowViewModel : ViewModel
    {
        #region Services
        private IAccountService _accountService;
        private NotificationManager _notificationManager = new NotificationManager();
        #endregion
        #region Fields
        private AccountWindow _accountWindow;
        private int _userId;
        private string _name;
        private double _balance;
        private double? _limit;
        private string? _number;
        private bool _isLimited;
        private DateTime? _limitRenewalDate;
        private int? _limitRenewalFrequency;
        
        public int UserId
        {
            get { return _userId; }
            set { _userId = value ; OnPropertyChanged();  } 
        }
        public string Name
        {
            get { return _name; }
            set { _name = value ; OnPropertyChanged(); }
        }
        public double Balance
        {
            get { return _balance; }
            set { _balance = value ; OnPropertyChanged(); }
        }
        public double? Limit
        {
            get { return _limit; }
            set { _limit = value ; OnPropertyChanged(); }
        }
        public string? Number
        {
            get { return _number; }
            set { _number = value ; OnPropertyChanged(); }
        }
        public int? LimitRenewalFrequency
        {
            get { return _limitRenewalFrequency; }
            set { _limitRenewalFrequency = value; OnPropertyChanged(); }
        }

        public bool IsLimited
        {
            get { return _isLimited; }
            set { _isLimited = value; OnPropertyChanged(); }
        }

        public ObservableCollection<int> FrequencyItems { get; set; }
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
                            AccountDTO account = new AccountDTO();
                            if (Name != null && Name != "")
                            {
                                account.UserId = UserId;
                                account.Name = Name;
                                account.Balance = Balance;
                                account.Limit = Limit;
                                account.Number = Number;
                                account.LimitRenewalFrequency = LimitRenewalFrequency;
                                if (LimitRenewalFrequency != null)
                                    account.LimitRenewalDate = DateTime.Now.AddDays((double)LimitRenewalFrequency);
                                else account.LimitRenewalDate = null;

                                _accountService.CreateAccount(account);
                                _accountWindow.DialogResult = true;
                            }
                            else
                            {
                                _notificationManager.Show("Некорректные данные!", NotificationType.Warning, "WindowArea");
                            }
                        }
                        catch (Exception ex)
                        {
                            _accountWindow.DialogResult = false;
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
                    _accountWindow.DialogResult = false;
                });
            }
        }
        #endregion

        public AccountWindowViewModel(AccountWindow accountWindow, IAccountService accountService, int loggedUserId)
        {
            _accountWindow = accountWindow;
            _accountService = accountService;
            UserId = loggedUserId;
            FrequencyItems = new ObservableCollection<int>(Enumerable.Range(1, 30));

        }
    }
}
