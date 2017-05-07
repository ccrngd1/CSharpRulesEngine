using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using RulesEngine.DAL;
using LawsonCS.Business.RulesEngine;
using LawsonCS.RulesEngine.Data;

namespace LawsonCS.WinForms.RulesEngineBasic
{
    /// <summary>
    /// Interaction logic for BasicGUI.xaml
    /// </summary>
    public partial class BasicGUI : RuleGUITabPage
    {
        #region public IRulesEngineDAL RulesDAL
        public IRulesEngineDAL RulesDAL
        {
            get { return (IRulesEngineDAL)(GetValue(RuleDALProperty)); }
            set { SetValue(RuleDALProperty, value);
                NotifyPropertyChanged("RulesDAL");
            }
        }
        public static readonly DependencyProperty RuleDALProperty =
            DependencyProperty.Register("RulesDAL", typeof(IRulesEngineDAL), typeof(BasicGUI));
        #endregion

        #region public RuleAppliedBase RuleAppliedInstance
        public RuleAppliedBase RuleAppliedInstance
        {
            get { return (RuleAppliedBase)GetValue(RuleAppliedInstanceProperty); }
            set { SetValue(RuleAppliedInstanceProperty, value);
                NotifyPropertyChanged("RuleAppliedInstance");
            }
        }
        public static readonly DependencyProperty RuleAppliedInstanceProperty =
            DependencyProperty.Register("RuleAppliedInstance", typeof(RuleAppliedBase), typeof(BasicGUI));
        #endregion

        #region public int RuleAppliedId
        public int RuleAppliedId
        {
            get { return (int) GetValue(RuleAppliedIdProperty); }
            set { SetValue(RuleAppliedIdProperty, value);
                NotifyPropertyChanged("RuleAppliedId");
            }
        }
        public static readonly DependencyProperty RuleAppliedIdProperty =
            DependencyProperty.Register("RuleAppliedId", typeof(int), typeof(BasicGUI));
        #endregion 

        #region public string CodeText
        public string CodeText
        {
            get { return (string)GetValue(CodeTextProperty); }
            set
            {
                SetValue(CodeTextProperty, value);
                NotifyPropertyChanged("CodeText");
            }
        }
        public static readonly DependencyProperty CodeTextProperty =
            DependencyProperty.Register("CodeText", typeof(string), typeof(BasicGUI));
        #endregion

        #region public string CodeConditionText
        public string CodeConditionText
        {
            get { return (string)GetValue(CodeConditionTextProperty); }
            set
            {
                SetValue(CodeConditionTextProperty, value);
                NotifyPropertyChanged("CodeConditionText");
            }
        }

        public static readonly DependencyProperty CodeConditionTextProperty =
            DependencyProperty.Register("CodeConditionText", typeof(string), typeof(BasicGUI));
        #endregion

        #region public List<string> RuleAppliedTables
        public IList RuleAppliedTables
        {
            get { return (IList)GetValue(RuleAppliedTablesProperty); }
            set
            {
                SetValue(RuleAppliedTablesProperty, value);
                NotifyPropertyChanged("RuleAppliedTables");
            }
        }

        public static readonly DependencyProperty RuleAppliedTablesProperty =
            DependencyProperty.Register("RuleAppliedTables", typeof(IList), typeof(BasicGUI));
        #endregion

        #region public bool RuleTableTBEnabled
        public bool RuleTableTBEnabled
        {
            get { return (bool)GetValue(CodeConditionTextProperty); }
            set
            {
                SetValue(RuleTableTBEnabledProperty, value);
                NotifyPropertyChanged("RuleTableTBEnabled");
            }
        }

        public static readonly DependencyProperty RuleTableTBEnabledProperty =
            DependencyProperty.Register("RuleTableTBEnabled", typeof(bool), typeof(BasicGUI));
        #endregion

        #region public bool RuleAppliedIdEnabled
        public bool RuleAppliedIdTBEnabled
        {
            get { return (bool)GetValue(RuleAppliedIdTBEnabledProperty); }
            set { SetValue(RuleAppliedIdTBEnabledProperty, value);
                NotifyPropertyChanged("RuleAppliedIdTBEnabled");
            }
        }

        public static readonly DependencyProperty RuleAppliedIdTBEnabledProperty = 
            DependencyProperty.Register("RuleAppliedIdTBEnabled", typeof(bool), typeof(BasicGUI));
        #endregion

        #region public bool GetButtonEnabled 
        public bool GetButtonEnabled
        {
            get { return (bool) GetValue(GetButtonEnabledProperty); }
            set
            {
                SetValue(GetButtonEnabledProperty, value);
                NotifyPropertyChanged("GetButtonEnabled");
            }
        }

        public static readonly DependencyProperty GetButtonEnabledProperty =
            DependencyProperty.Register("GetButtonEnabled", typeof(bool), typeof(BasicGUI));
        #endregion

        #region public bool RuleCategoryCBEnabled
        public bool RuleCategoryCBEnabled
        {
            get { return (bool)GetValue(RuleCategoryCBEnableddProperty); }
            set
            {
                SetValue(RuleCategoryCBEnableddProperty, value);
                NotifyPropertyChanged("RuleCategoryCBEnabled");
            }
        }

        public static readonly DependencyProperty RuleCategoryCBEnableddProperty =
            DependencyProperty.Register("RuleCategoryCBEnabled", typeof(bool), typeof(BasicGUI));
        #endregion

        #region public bool RuleubCategoryCBEnabled
        public bool RuleSubCategoryCBEnabled
        {
            get { return (bool)GetValue(RuleSubCategoryCBEnabledProperty); }
            set
            {
                SetValue(RuleSubCategoryCBEnabledProperty, value);
                NotifyPropertyChanged("RuleSubCategoryCBEnabled");
            }
        }

        public static readonly DependencyProperty RuleSubCategoryCBEnabledProperty =
            DependencyProperty.Register("RuleSubCategoryCBEnabled", typeof(bool), typeof(BasicGUI));
        #endregion

        #region public bool SaveButtonEnabled
        public bool SaveButtonEnabled
        {
            get { return (bool)GetValue(SaveButtonEnabledProperty); }
            set
            {
                SetValue(SaveButtonEnabledProperty, value);
                NotifyPropertyChanged("SaveButtonEnabled");
            }
        }

        public static readonly DependencyProperty SaveButtonEnabledProperty =
            DependencyProperty.Register("SaveButtonEnabled", typeof(bool), typeof(BasicGUI));
        #endregion

        #region public bool PromoteButtonEnabled
        public bool PromoteButtonEnabled
        {
            get { return (bool)GetValue(PromoteButtonEnabledProperty); }
            set
            {
                SetValue(PromoteButtonEnabledProperty, value);
                NotifyPropertyChanged("PromoteButtonEnabled");
            }
        }

        public static readonly DependencyProperty PromoteButtonEnabledProperty =
            DependencyProperty.Register("PromoteButtonEnabled", typeof(bool), typeof(BasicGUI));
        #endregion

        #region public bool RuleInputTypesLBEnabled
        public bool RuleInputTypesLBEnabled
        {
            get { return (bool)GetValue(RuleInputTypesLBEnabledProperty); }
            set
            {
                SetValue(RuleInputTypesLBEnabledProperty, value);
                NotifyPropertyChanged("RuleInputTypesLBEnabled");
            }
        }

        public static readonly DependencyProperty RuleInputTypesLBEnabledProperty =
            DependencyProperty.Register("RuleInputTypesLBEnabled", typeof(bool), typeof(BasicGUI));
        #endregion

        #region public bool ConditionTextEnabled
        public bool ConditionTextEnabled
        {
            get { return (bool)GetValue(ConditionTextEnabledProperty); }
            set
            {
                SetValue(ConditionTextEnabledProperty, value);
                NotifyPropertyChanged("ConditionTextEnabled");
            }
        }

        public static readonly DependencyProperty ConditionTextEnabledProperty =
            DependencyProperty.Register("ConditionTextEnabled", typeof(bool), typeof(BasicGUI));
        #endregion

        #region public bool RuleOutputTypesLBEnabled
        public bool RuleOutputTypesLBEnabled
        {
            get { return (bool)GetValue(RuleOutputTypesLBEnabledProperty); }
            set
            {
                SetValue(RuleOutputTypesLBEnabledProperty, value);
                NotifyPropertyChanged("RuleOutputTypesLBEnabled");
            }
        }

        public static readonly DependencyProperty RuleOutputTypesLBEnabledProperty =
            DependencyProperty.Register("RuleOutputTypesLBEnabled", typeof(bool), typeof(BasicGUI));
        #endregion

        #region public bool CodeTextEnabled
        public bool CodeTextEnabled
        {
            get { return (bool)GetValue(CodeTextEnabledProperty); }
            set
            {
                SetValue(CodeTextEnabledProperty, value);
                NotifyPropertyChanged("CodeTextEnabled");
            }
        }

        public static readonly DependencyProperty CodeTextEnabledProperty =
            DependencyProperty.Register("CodeTextEnabled", typeof(bool), typeof(BasicGUI));
        #endregion


        private BaseRuleEngineBL _rulesBL;
        private string _ruleTableSelected;
        
        public BasicGUI()
        {
            InitializeComponent();

            RulesDAL = new BaseRuleEngineDL(ConfigurationManager.AppSettings["RulesEngine"], new TimeSpan(1, 0, 0, 0));

            CodeTextEnabled = true;
            ConditionTextEnabled = true;
            GetButtonEnabled = true;
            PromoteButtonEnabled = true;
            RuleAppliedIdTBEnabled = true;
            RuleCategoryCBEnabled = true;
            RuleInputTypesLBEnabled = true;
            RuleOutputTypesLBEnabled = true;
            RuleSubCategoryCBEnabled = true;
            RuleTableTBEnabled = true;
            SaveButtonEnabled = true;

            RuleAppliedTables = RulesDAL.GetRuleppliedTables(); 
        }

        #region private void XButton_OnClick
        private void SaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            SetUpBL();
            throw new NotImplementedException();
        }

        private void PromoteButton_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void GetButton_OnClick(object sender, RoutedEventArgs e)
        {
            SetUpBL();

            if(_rulesBL!=null)
                RuleAppliedInstance = _rulesBL.GetBaseRuleApplied(RuleAppliedId, _ruleTableSelected);

            if (RuleAppliedInstance != null)
            {
                var codeIds = new List<int> {RuleAppliedInstance.CodeId};

                if(RuleAppliedInstance.ConditionCodeId!=null) codeIds.Add(RuleAppliedInstance.ConditionCodeId.Value);

                List<RuleCodeCache> codeMeta = RulesDAL.GetRuleCodeMetaData(codeIds);
                RulesDAL.GetRuleCode(codeMeta);

                RuleCodeCache codeCache = codeMeta.FirstOrDefault(c => c.RuleCodeId == RuleAppliedInstance.CodeId);
                if (codeCache != null)
                    CodeText = codeCache.CodeText;

                if (RuleAppliedInstance.ConditionCodeId != null)
                {
                    RuleCodeCache condCodeCache = codeMeta.FirstOrDefault(c => c.RuleCodeId == RuleAppliedInstance.ConditionCodeId.Value);
                    if (condCodeCache != null)
                        CodeConditionText = condCodeCache.CodeText;
                }
            }

            GUILogging.LogMessage(RuleAppliedInstance != null
                ? string.Format("RuleApplied {0} found", RuleAppliedInstance.RuleAppliedId)
                : string.Format("RuleApplied {0} not found", RuleAppliedId));
        }

        private void ClearButton_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
        #endregion

        private void SetUpBL()
        {
            if (_ruleTableSelected == null)
            {
                MessageBox.Show("No Rule table chosen");
                return;
            }

            if (_rulesBL == null || _rulesBL.RulesTable != _ruleTableSelected) _rulesBL = new BaseRuleEngineBL(RulesDAL, _ruleTableSelected);
        }

        private void DisableNewRuleFields()
        {
            RuleAppliedIdTBEnabled = false;
            GetButtonEnabled = false;
            PromoteButtonEnabled = false;
            RuleTableTBEnabled = false;
        }

        private void DisableRuleLookupFields()
        {
            GetButtonEnabled = false;
            PromoteButtonEnabled = false;
        }

        private void RuleTableCB_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _ruleTableSelected = e.AddedItems[0].ToString();
        }

        private void VerifyButton_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Not Implemented");
        }
    }
}
