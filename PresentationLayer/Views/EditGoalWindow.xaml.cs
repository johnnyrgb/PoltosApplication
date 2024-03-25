using BusinessLogicLayer.DataTransferObjects;
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
    /// Логика взаимодействия для EditGoalWindow.xaml
    /// </summary>
    public partial class EditGoalWindow : Window
    {
        public EditGoalWindow(IGoalService goalService, int loggedUserId, GoalDTO goalDTO)
        {
            InitializeComponent();
            DataContext = new EditGoalWindowViewModel(this, goalService, loggedUserId, goalDTO);
        }
    }
}
