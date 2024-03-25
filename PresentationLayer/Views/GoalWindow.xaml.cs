using BusinessLogicLayer.Interfaces;
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
    /// Логика взаимодействия для GoalWindow.xaml
    /// </summary>
    public partial class GoalWindow : Window
    {
        public GoalWindow(IGoalService goalService, int loggedUserId)
        {
            InitializeComponent();
            DataContext = new GoalWindowViewModel(this, goalService, loggedUserId);
        }
    }
}
