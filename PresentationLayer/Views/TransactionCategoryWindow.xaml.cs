using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using PresentationLayer.ViewModels;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для TransactionCategoryWindow.xaml
    /// </summary>
    public partial class TransactionCategoryWindow : Window
    {
        public TransactionCategoryWindow(ITransactionCategoryService transactionCategoryService, int loggedUserId)
        {
            InitializeComponent();
            DataContext = new TransactionCategoryWindowViewModel(this, transactionCategoryService, loggedUserId);
        }
    }
}
