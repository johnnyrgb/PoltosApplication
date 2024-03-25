using BusinessLogicLayer.DataTransferObjects;
using BusinessLogicLayer.Interfaces;
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

namespace PresentationLayer.ViewModels
{
    public class EditAccountWindowViewModel : ViewModel
    {
        #region Services
        private IAccountService _accountService;
        private NotificationManager _notificationManager = new NotificationManager();
        #endregion
        #region Fields
        private EditAccountWindow _editAccountWindow;
        private AccountDTO _account;
        private int _id;
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
            set { _userId = value; OnPropertyChanged(); }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }
        public double Balance
        {
            get { return _balance; }
            set { _balance = value; OnPropertyChanged(); }
        }
        public double? Limit
        {
            get { return _limit; }
            set { _limit = value; OnPropertyChanged(); }
        }
        public string? Number
        {
            get { return _number; }
            set { _number = value; OnPropertyChanged(); }
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
                        if (Name != null && Name != "")
                        {
                            _account.UserId = UserId;
                            _account.Name = Name;
                            _account.Balance = Balance;
                            _account.Limit = Limit;
                            _account.Number = Number;
                            if (LimitRenewalFrequency != null)
                            {
                                DateTime newDate = (DateTime)_account.LimitRenewalDate;
                                double delta = (double)this.LimitRenewalFrequency - (double)_account.LimitRenewalFrequency;
                                newDate = newDate.AddDays(delta);
                                if (newDate < DateTime.Now)
                                {
                                    newDate = DateTime.Now;
                                }
                                _account.LimitRenewalDate = newDate;
                                _account.LimitRenewalFrequency = LimitRenewalFrequency;
                            }
                            else _account.LimitRenewalDate = null;
                            _accountService.UpdateAccount(_account);
                            _editAccountWindow.DialogResult = true;
                        }
                        else
                        {
                            _notificationManager.Show("Некорректные данные!", NotificationType.Warning, "WindowArea");
                        }

                    }
                    catch (Exception ex)
                    {
                        _editAccountWindow.DialogResult= false;
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
                    _editAccountWindow.DialogResult = false;
                });
            }
        }
        #endregion

        public EditAccountWindowViewModel(EditAccountWindow editAccountWindow, IAccountService accountService, int loggedUserId, AccountDTO account)
        {
            _editAccountWindow = editAccountWindow;
            _accountService = accountService;
            _account = account;
            UserId = loggedUserId;
            FrequencyItems = new ObservableCollection<int>(Enumerable.Range(1, 30));
            Name = account.Name;
            Balance = account.Balance;
            Limit = account.Limit;
            Number = account.Number;
            LimitRenewalFrequency = account.LimitRenewalFrequency;
            if (account.LimitRenewalFrequency == null)
                IsLimited = false;
            else
                IsLimited = true;

        }
    }
}
