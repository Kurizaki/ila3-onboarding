using clock.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace clock
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public clock.Classes.Timer timer;

        public MainWindow()
        {
            InitializeComponent();
            timer = new clock.Classes.Timer(ComboBoxTimerHours, ComboBoxTimerMinutes,ComboBoxTimerSeconds, LabelTimer);
            timer.PopulateComboBoxes();
            Launch();
        }

        private void Launch()
        {

        }

        private void ButtonTimer_Click(object sender, RoutedEventArgs e)
        {
            timer.StartStop();
        }

        private void ComboBoxTimer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            timer.SelectionChanged();
        }
    }
}
