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
    public class GoalWindowViewModel : ViewModel
    {
        #region Services
        private IGoalService _goalService;
        private NotificationManager _notificationManager = new NotificationManager();
        #endregion
        #region Fields
        private GoalWindow _goalWindow;
        private int _userId;
        private string _name;
        private double _balance;
        private double _amount;
        private bool _isReached;
        private bool _isExpired;
        private DateTime _dateToSaveUp;
        private DateTime _dateOfCreation;

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
        public double Amount
        {
            get { return _amount; }
            set { _amount = value; OnPropertyChanged(); }
        }
        
        public DateTime DateToSaveUp
        {
            get { return _dateToSaveUp; }
            set { _dateToSaveUp = value; OnPropertyChanged(); }
        }

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
                        GoalDTO goal = new GoalDTO();
                        if (Name != null && Name != "" && Amount != null && Amount > 0 && (DateToSaveUp > DateTime.Now) && Balance >= 0 && Balance != null && Balance < Amount)
                        {
                            goal.Name = Name;
                            goal.Amount = Amount;
                            goal.Balance = Balance;
                            goal.DateOfCreation = DateTime.Now;
                            goal.DateToSaveUp = DateToSaveUp;
                            goal.IsExpired = false;
                            goal.IsReached = false;
                            goal.UserId = UserId;
                            _goalService.CreateGoal(goal);
                            _goalWindow.DialogResult = true;
                        }
                        else
                        {
                            _notificationManager.Show("Некорректные данные!", NotificationType.Warning, "WindowArea");
                        }
                    }
                    catch (Exception ex)
                    {
                        _goalWindow.DialogResult = false;
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
                    _goalWindow.DialogResult = false;
                });
            }
        }
        #endregion

        public GoalWindowViewModel(GoalWindow goalWindow, IGoalService goalService, int loggedUserId)
        {
            _goalService = goalService;
            _goalWindow = goalWindow;
            UserId = loggedUserId;
            DateToSaveUp = DateTime.Now.AddDays(180);
        }
    }
}

