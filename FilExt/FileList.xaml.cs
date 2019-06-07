using Me.Amon.FilExt.Dvo;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace Me.Amon.FilExt
{
    /// <summary>
    /// FileList.xaml 的交互逻辑
    /// </summary>
    public partial class FileList : Window
    {
        private ObservableCollection<FileDvo> _Files;
        private FileDvo _Dvo;
        private bool _Done;

        public FileList()
        {
            InitializeComponent();
        }

        public void Init(IEnumerable<RuleDto> rules, params string[] args)
        {
            _Files = new ObservableCollection<FileDvo>();
            if (args != null)
            {
                foreach (var arg in args)
                {
                    var dto = new FileDvo { SrcPath = System.IO.Path.GetDirectoryName(arg), SrcFile = System.IO.Path.GetFileName(arg) };
                    MatchRule(rules, dto);
                    _Files.Add(dto);
                }
            }

            DgList.ItemsSource = _Files;
        }

        private void MatchRule(IEnumerable<RuleDto> rules, FileDvo dto)
        {
            var list = new List<RuleDto>();
            var opt = new AFileOpt();
            foreach (var rule in rules)
            {
                if (opt.Test(rule, dto))
                {
                    list.Add(rule);
                }
            }
            dto.OptionalList = list;
            if (list.Count == 1)
            {
                dto.CurrentRule = list[0];
            }
        }

        private void DgList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _Dvo = DgList.SelectedItem as FileDvo;
            if (_Dvo == null)
            {
                return;
            }

            LbRule.ItemsSource = _Dvo.OptionalList;
            LcSrc.Text = _Dvo.SrcFile;
            LcDst.Text = _Dvo.DstPath;
        }

        private void LbRule_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (_Dvo == null)
            {
                return;
            }

            var rule = LbRule.SelectedItem as RuleDto;
            if (rule == null)
            {
                return;
            }

            _Dvo.CurrentRule = rule;
            new AFileOpt().Test(rule, _Dvo);
        }

        private void BtAccept_Click(object sender, RoutedEventArgs e)
        {
            if (_Files == null || _Done)
            {
                Close();
                return;
            }

            foreach (var file in _Files)
            {
                var rule = file.CurrentRule;
                if (rule == null)
                {
                    file.Reason = "未处理";
                    continue;
                }

                var opt = AFileOpt.GetInstance(rule.method);
                opt.Deal(file.CurrentRule, file);
                file.Reason = "处理完成";
            }

            _Done = true;
            BtAccept.Content = "关闭";
        }
    }
}
