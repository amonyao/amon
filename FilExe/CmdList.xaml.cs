using Me.Amon.FilExe.Dao;
using Me.Amon.FilExe.Dvo;
using System.Collections.ObjectModel;
using System.Windows;

namespace Me.Amon.FilExe
{
    /// <summary>
    /// CmdList.xaml 的交互逻辑
    /// </summary>
    public partial class CmdList : Window
    {
        private ObservableCollection<AppDvo> _List = new ObservableCollection<AppDvo>();

        public CmdList()
        {
            InitializeComponent();
        }

        public void Init()
        {
            DgList.ItemsSource = _List;
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
            var dialog = new CmdEdit();
            dialog.Owner = this;
            dialog.Init(new AppDvo());
            dialog.ShowDialog();

            _List.Clear();
            LoadRule();
        }

        /// <summary>
        /// 更新事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtUpdate_Click(object sender, RoutedEventArgs e)
        {
            var dto = DgList.SelectedItem as AppDvo;
            if (dto == null)
            {
                return;
            }

            var dialog = new CmdEdit();
            dialog.Owner = this;
            dialog.Init(dto);
            dialog.ShowDialog();

            _List.Clear();
            LoadRule();
        }

        /// <summary>
        /// 删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtDelete_Click(object sender, RoutedEventArgs e)
        {
            var dto = DgList.SelectedItem as AppDvo;
            if (dto == null)
            {
                return;
            }

            var reuslt = MessageBox.Show("确认要删除选中的规则吗？", "提示", MessageBoxButton.YesNoCancel);
            if (reuslt != MessageBoxResult.Yes)
            {
                return;
            }

            new AppDao().Delete(dto.id);
        }
        #endregion

        private void LoadRule()
        {
            var list = new AppDao().List<AppDvo>(null);
            foreach (var item in list)
            {
                _List.Add(item);
            }
        }
    }
}
