using Autofac;
using BusinessLogicLayer.DataTransferObjects;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using PresentationLayer.Util;
using PresentationLayer.Util.Navigation;
using PresentationLayer.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using System.Windows.Controls;
using System.Windows;
using Notification.Wpf;

namespace PresentationLayer.ViewModels
{

    public class LoginWindowViewModel : ViewModel
    {
        private NotificationManager _notificationManager = new NotificationManager();
        private IUserService _userService;
        private MainWindow _mainWindow;
        private LoginWindow _loginWindow;
        private ObservableCollection<UserDTO> _users;
        public ObservableCollection<UserDTO> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                OnPropertyChanged();
            }
        }
        private void openMainWindow(int userId)
        {
            _mainWindow = new MainWindow(userId);
            _mainWindow.Show();
            _loginWindow.Close();
        }
        private int _selectedUserId;
        private string _password;
        public int SelectedUserId
        {
            get => _selectedUserId; 
            set
            {
                _selectedUserId = value;
                OnPropertyChanged();
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }
        private void Authentication(object obj)
        {
            PasswordBox box = obj as PasswordBox;
            Password = box.Password;

            if (_selectedUserId != 0)
            {
                var user = _userService.GetUser(_selectedUserId);
                if (user.Password == this.Password)
                    openMainWindow(SelectedUserId);
                else
                    _notificationManager.Show("Ошибка", "Неверный пароль", NotificationType.Error, "WindowArea");
            }
            else
                _notificationManager.Show("Ошибка", "Выберите пользователя", NotificationType.Error, "WindowArea");
        }
        public ICommand AuthenticationCommand { get; set; }
        public LoginWindowViewModel(LoginWindow loginWindow)
        {
            _loginWindow = loginWindow;
            var builder = new ContainerBuilder();
            builder.RegisterModule(new ReposModule("Data Source=F:\\Poltos\\PoltosApplication\\database123.db"));
            var container = builder.Build();
            _userService = container.Resolve<IUserService>();
            Users = new ObservableCollection<UserDTO>(_userService.GetUsers());
            AuthenticationCommand = new RelayCommand(Authentication);
        }
    }
}
