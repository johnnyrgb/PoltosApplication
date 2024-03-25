using BusinessLogicLayer.Interfaces;
using PresentationLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PresentationLayer.Views
{
    /// <summary>
    /// Логика взаимодействия для TransactionWindow.xaml
    /// </summary>
    public partial class TransactionWindow : Window
    {
        public TransactionWindow(IAccountService accountService,ITransactionService transactionService, ITransactionCategoryService transactionCategoryService, IGoalService goalService, int loggedUserId)
        {
            InitializeComponent();
            DataContext = new TransactionWindowViewModel(this, accountService, transactionService, transactionCategoryService, goalService, loggedUserId);
        }
    }
    
}
