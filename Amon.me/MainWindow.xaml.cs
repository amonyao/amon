using Me.Amon.FilExt;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Me.Amon
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window, MainForm
    {
        private IPlugin _Plugin;

        public MainWindow()
        {
            InitializeComponent();
        }

        public void Init()
        {
            Left = SystemParameters.PrimaryScreenWidth / 2;
            Top = 0;

            _Plugin = new Plugin();

            ShowSearch(false);

            ShowUpgrade();
        }

        #region 接口实现
        public void Register(IPlugin plugin)
        {

        }
        #endregion

        #region 事件处理
        /// <summary>
        /// 窗口移动事项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        /// <summary>
        /// 拖拽进入事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Link;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
        }

        /// <summary>
        /// 窗口拖拽事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Drop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                return;
            }

            var data = e.Data.GetData(DataFormats.FileDrop);
            if (data == null || !(data is string[]))
            {
                return;
            }

            e.Handled = true;

            var list = (string[])data;
            if (_Plugin != null)
            {
                _Plugin.Enter(list);
                return;
            }
        }

        private void MiRule_Click(object sender, RoutedEventArgs e)
        {
            var window = new RuleList();
            window.Init();
            window.Show();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MiAid_Click(object sender, RoutedEventArgs e)
        {
            var visible = MiAid.IsChecked;
            ShowAid(true);
        }

        /// <summary>
        /// 系统配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MiCfg_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// 关于
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MiInfo_Click(object sender, RoutedEventArgs e)
        {
            if (_About == null || !_About.IsVisible)
            {
                _About = new About();
            }
            _About.Show();
        }
        private About _About;

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MiExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
            Application.Current.Shutdown();
        }

        /// <summary>
        /// LOGO双击事项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LbFilexp_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var visible = BdSearch.Visibility == Visibility.Visible;
            ShowSearch(!visible);
        }

        /// <summary>
        /// 文本输入事项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TbSearch_KeyDown(object sender, KeyEventArgs e)
        {

        }
        #endregion

        private void ShowSearch(bool visible)
        {
            var t = visible ? Visibility.Visible : Visibility.Hidden;
            BdSearch.Visibility = t;

            if (visible)
            {
                var tmp = BdWindow.Tag as WindowProperty;
                BdWindow.Background = tmp.BgBrush;
                BdWindow.BorderBrush = tmp.BdBrush;
            }
            else
            {
                BdWindow.Tag = new WindowProperty() { BgBrush = BdWindow.Background, BdBrush = BdWindow.BorderBrush };
                BdWindow.Background = Brushes.Transparent;
                BdWindow.BorderBrush = Brushes.Transparent;
            }
        }

        /// <summary>
        /// 系统更新
        /// </summary>
        private void ShowUpgrade()
        {
            // 执行数据库更新
            if (File.Exists(Upgrade.UPGRADE_SQL))
            {
            }

            // 显示更新日志窗口
            if (File.Exists(Upgrade.UPGRADE_LOG))
            {
                var window = new Upgrade();
                window.Init();
                window.Show();
            }
        }

        private void ShowAid(bool visible)
        {
            if (_AidWindow == null)
            {
                _AidWindow = new AidWindow();
            }

            if (visible)
            {
                _AidWindow.Top = Top + Height;
                _AidWindow.Left = Left;

                _AidWindow.Show();
            }
            else
            {
                _AidWindow.Hide();
            }

            _AidWindow.Visibility = visible ? Visibility.Visible : Visibility.Hidden;
            MiAid.IsChecked = visible;
        }
        private AidWindow _AidWindow;
    }

    public class WindowProperty
    {
        public Brush BgBrush;
        public Brush BdBrush;
    }
}
