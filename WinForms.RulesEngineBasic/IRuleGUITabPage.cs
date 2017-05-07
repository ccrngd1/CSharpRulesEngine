using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LawsonCS.WinForms.RulesEngineBasic
{
    public class RuleGUITabPage : UserControl, IDisposable, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged 
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        #endregion

        #region public EnvironmentEnum CurrentEnvironmentEnum
        public EnvironmentEnum CurrentEnvironmentEnum
        {
            get { return (EnvironmentEnum)GetValue(CurrentEnvironmentEnumProperty); }
            set
            {
                SetValue(CurrentEnvironmentEnumProperty, value);
                NotifyPropertyChanged("CurrentEnvironmentEnum");
            }
        }
        public static readonly DependencyProperty CurrentEnvironmentEnumProperty =
            DependencyProperty.Register("CurrentEnvironmentEnum", typeof(EnvironmentEnum), typeof(RuleGUITabPage));
        #endregion

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
