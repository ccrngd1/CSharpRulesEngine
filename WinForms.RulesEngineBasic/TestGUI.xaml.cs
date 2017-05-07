using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using RulesEngine.DAL;

namespace LawsonCS.WinForms.RulesEngineBasic
{
    /// <summary>
    /// Interaction logic for TestGUI.xaml
    /// </summary>
    public partial class TestGUI : INotifyPropertyChanged
    {
        private readonly IRulesEngineDAL _rulesDAL;
        private readonly string _ruleTable;

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public EnvironmentEnum CurrentEnvironmentEnum
        {
            private get { return (EnvironmentEnum)GetValue(CurrentEnvironmentEnumProperty); }
            set
            {
                SetValue(CurrentEnvironmentEnumProperty, value);
                NotifyPropertyChanged("CurrentEnvironmentEnum");
            }
        }

        public static readonly DependencyProperty CurrentEnvironmentEnumProperty =
            DependencyProperty.Register("CurrentEnvironmentEnum", typeof(EnvironmentEnum), typeof(TestGUI));

        public TestGUI()
        {
            InitializeComponent();

            _rulesDAL = new BaseRuleEngineDL(ConfigurationManager.AppSettings["RulesEngine"], new TimeSpan(1,0,0,0));
        }
    }
}
