using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace LawsonCS.WinForms.RulesEngineBasic
{
    /// <summary>
    /// Interaction logic for MiscTestHarness.xaml
    /// </summary>
    public partial class MiscTestHarness : RuleGUITabPage
    { 
        public MiscTestHarness()
        {
            InitializeComponent();
        }

        private void RunWFETest_OnClick(object sender, RoutedEventArgs e)
        {
            RulesEngineUnitTest.UnitTest1 ut = new RulesEngineUnitTest.UnitTest1();
            ut.TestWfeMigration();
        }
    }
}
