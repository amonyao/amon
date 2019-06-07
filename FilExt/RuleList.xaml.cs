using Me.Amon.Dao;
using Me.Amon.FilExt.Dvo;
using System.Collections.ObjectModel;
using System.Windows;

namespace Me.Amon.FilExt
{
    /// <summary>
    /// RuleList.xaml 的交互逻辑
    /// </summary>
    public partial class RuleList : Window
    {
        private ObservableCollection<RuleDvo> _Rules = new ObservableCollection<RuleDvo>();

        public RuleList()
        {
            InitializeComponent();
        }

        public void Init()
        {
            DgList.ItemsSource = _Rules;
            LoadRule();
        }

        #region 事件处理
        /// <summary>
        /// 新增事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtAppend_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new RuleEdit();
            dialog.Owner = this;
            dialog.Init(new RuleDto());
            dialog.ShowDialog();

            _Rules.Clear();
            LoadRule();
        }

        /// <summary>
        /// 更新事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtUpdate_Click(object sender, RoutedEventArgs e)
        {
            var dto = DgList.SelectedItem as RuleDto;
            if (dto == null)
            {
                return;
            }

            var dialog = new RuleEdit();
            dialog.Owner = this;
            dialog.Init(dto);
            dialog.ShowDialog();

            _Rules.Clear();
            LoadRule();
        }

        /// <summary>
        /// 删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtDelete_Click(object sender, RoutedEventArgs e)
        {
            var dto = DgList.SelectedItem as RuleDto;
            if (dto == null)
            {
                return;
            }

            var reuslt = MessageBox.Show("确认要删除选中的规则吗？", "提示", MessageBoxButton.YesNoCancel);
            if (reuslt != MessageBoxResult.Yes)
            {
                return;
            }

            new RuleDao().Delete(dto.id);
        }
        #endregion

        private void LoadRule()
        {
            var list = new RuleDao().List<RuleDvo>(null);
            foreach (var item in list)
            {
                _Rules.Add(item);
            }
        }
    }
}
