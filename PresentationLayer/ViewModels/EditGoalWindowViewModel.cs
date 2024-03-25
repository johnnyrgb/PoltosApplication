using BusinessLogicLayer.DataTransferObjects;
using BusinessLogicLayer.Interfaces;
using Notification.Wpf;
using PresentationLayer.Util.Navigation;
using PresentationLayer.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PresentationLayer.ViewModels
{
    public class EditGoalWindowViewModel : ViewModel
    {
        #region Services
        private IGoalService _goalService;
        private NotificationManager _notificationManager = new NotificationManager();
        #endregion
        #region Fields
        private EditGoalWindow _editGoalWindow;
        private GoalDTO _goal;
        private int _userId;
        private string _name;
        private double _balance;
        private double _amount;
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
                        if (Name != null && Name != "" && Amount != null && Amount > 0 && (DateToSaveUp > _dateOfCreation) && Balance >= 0 && Balance != null)
                        {
                            _goal.Name = Name;
                            _goal.Amount = Amount;
                            _goal.Balance = Balance;
                            _goal.DateToSaveUp = DateToSaveUp;
                            if (DateToSaveUp <= _dateOfCreation)
                                _goal.IsExpired = true;
                            else _goal.IsExpired = false;
                            if (Balance >= Amount)
                                _goal.IsReached = true;
                            else _goal.IsReached = false;
                            _goal.UserId = UserId;
                            _goalService.UpdateGoal(_goal);
                            _editGoalWindow.DialogResult = true;
                        }
                        else
                        {
                            _notificationManager.Show("Некорректные данные!", NotificationType.Warning, "WindowArea");
                        }
                    }
                    catch (Exception ex)
                    {
                        _editGoalWindow.DialogResult = false;
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
                    _editGoalWindow.DialogResult = false;
                });
            }
        }
        #endregion

        public EditGoalWindowViewModel(EditGoalWindow editGoalWindow, IGoalService goalService, int loggedUserId, GoalDTO goal)
        {
            _goal = goal;
            _goalService = goalService;
            _editGoalWindow = editGoalWindow;
            UserId = loggedUserId;
            Name = goal.Name;
            Amount = goal.Amount;
            Balance = goal.Balance;
            DateToSaveUp = goal.DateToSaveUp;
            _dateOfCreation = goal.DateOfCreation;
            DateToSaveUp = DateTime.Now.AddDays(180);
        }
    }
}

