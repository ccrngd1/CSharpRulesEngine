using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
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

namespace LawsonCS.WinForms.RulesEngineBasic
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged
    {
        private MenuItem _currentEnvironmentMenuItem;

        public MainWindow()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged; 

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        } 

        public EnvironmentEnum GlobalCurrentEnvironmentEnum
        {
            get { return (EnvironmentEnum)GetValue(GlobalCurrentEnvironmentEnumProperty); }
            set
            {
                SetValue(GlobalCurrentEnvironmentEnumProperty, value);
                NotifyPropertyChanged("GlobalCurrentEnvironmentEnum");
            }
        }

        public static readonly DependencyProperty GlobalCurrentEnvironmentEnumProperty =
            DependencyProperty.Register("GlobalCurrentEnvironmentEnum", typeof(EnvironmentEnum), typeof(MainWindow));


        private void MenuItem_OnChecked(object sender, RoutedEventArgs e)
        {
            if (_currentEnvironmentMenuItem != null)
                _currentEnvironmentMenuItem.IsChecked = false;

            _currentEnvironmentMenuItem = (MenuItem)e.Source;

            EnvironmentEnum temp;
            Enum.TryParse(_currentEnvironmentMenuItem.Header.ToString(), out temp);
            GlobalCurrentEnvironmentEnum = temp;
        }

        private void MenuItem_Refresh_OnChecked(object sender, RoutedEventArgs e)
        {
            RefreshDBCache();   
        }

        private void RefreshDBCache()
        {
            
        }
    }

    public enum EnvironmentEnum
    {
        Test = 0,
        Qa = 1,
        Prod = 2,
    }
}
