using Me.Amon.Tray;
using Me.Amon.WinApi;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Me.Amon
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window, IMainForm
    {
        private IUserInfo _User;
        private IPlugin _Plugin;

        private KeyboardHook _Hook;

        public MainWindow()
        {
            InitializeComponent();
        }

        public void Init()
        {
            Left = SystemParameters.PrimaryScreenWidth / 2;
            Top = 0;

            _User = new Ur.UserInfo();

            //_Plugin = new FilExt.Plugin();
            _Plugin = new FilExe.Plugin();
            _Plugin.Init(this, _User);

            ShowCommand(false);

            _Hook = new KeyboardHook();
            _Hook.KeyDownEvent += Hook_KeyDownEvent;
            _Hook.SetHook();

            ShowTray();

            ShowUpgrade();
        }

        private int CtrlQty = 0;
        private DateTime CtrlTime = DateTime.Now;
        private void Hook_KeyDownEvent(Key key)
        {
            var now = DateTime.Now;
            if (key == Key.LeftCtrl || key == Key.RightCtrl)
            {
                if ((now - CtrlTime).Milliseconds > 300)
                {
                    CtrlQty = 0;
                }

                CtrlQty += 1;
                if (CtrlQty > 2)
                {
                    Activate();
                    ShowCommand(true);
                    CtrlQty = 0;
                }

                CtrlTime = now;
                return;
            }

            CtrlQty = 0;
            CtrlTime = now;
        }

        private void ShowTray()
        {
            new AmonTray(this, _User);
        }

        #region 接口实现
        public void ShowCommand(bool visible)
        {
            var t = visible ? Visibility.Visible : Visibility.Hidden;
            BdSearch.Visibility = t;
            //BdResult.Visibility = t;

            if (visible)
            {
                var tmp = BdWindow.Tag as WindowProperty;
                BdWindow.Background = tmp.BgBrush;
                BdWindow.BorderBrush = tmp.BdBrush;
                TbSearch.Focus();
                TbSearch.SelectAll();
            }
            else
            {
                BdWindow.Tag = new WindowProperty() { BgBrush = BdWindow.Background, BdBrush = BdWindow.BorderBrush };
                BdWindow.Background = Brushes.Transparent;
                BdWindow.BorderBrush = Brushes.Transparent;
            }
        }

        public string CommandText
        {
            get
            {
                return TbSearch.Text;
            }
            set
            {
                TbSearch.Text = value;
            }
        }

        public void AddPluginView(UserControl control)
        {
            BdResult.Child = control;
        }

        public void SetPluginView(string key, Visibility visibility)
        {
            this.BdResult.Dispatcher.Invoke(new Action(() => { this.BdResult.Visibility = visibility; }));
        }

        public void ShowPluginIcon(string icon)
        {
            throw new System.NotImplementedException();
        }

        public void ShowWindow()
        {
            Show();
            Activate();
        }

        public void HideWindow()
        {
            Hide();
        }

        public void ShowAbout()
        {
            if (_About == null || !_About.IsVisible)
            {
                _About = new About();
                _About.Init();
            }
            _About.Show();
        }
        private About _About;

        public void Exit()
        {
            if (_Hook != null)
            {
                _Hook.UnHook();
            }

            Close();
            Application.Current.Shutdown();
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
            if (_Plugin != null)
            {
                _Plugin.Drag_Enter(e);
            }
        }

        /// <summary>
        /// 窗口拖拽事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Drop(object sender, DragEventArgs e)
        {
            if (_Plugin != null)
            {
                _Plugin.Drag_Droped(e);
            }
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            //ShowCommand(false);
            //SetPluginView("", Visibility.Collapsed);
        }

        private void MiCmdEdit_Click(object sender, RoutedEventArgs e)
        {
            var window = new FilExe.CmdList();
            window.Init();
            window.Show();
        }

        private void MiRule_Click(object sender, RoutedEventArgs e)
        {
            var window = new FilExt.RuleList();
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
            ShowAbout();
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MiExit_Click(object sender, RoutedEventArgs e)
        {
            Exit();
        }

        /// <summary>
        /// LOGO双击事项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LbFilexp_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var visible = BdSearch.Visibility == Visibility.Visible;
            ShowCommand(!visible);
        }

        private void TbSearch_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                if (BdResult.Visibility == Visibility.Visible)
                {
                    BdResult.Visibility = Visibility.Collapsed;
                    return;
                }
                if (!string.IsNullOrWhiteSpace(TbSearch.Text))
                {
                    TbSearch.Text = "";
                    return;
                }
                ShowCommand(false);
            }

            if (_Plugin != null)
            {
                _Plugin.Meta_KeyDown(e);
            }
        }

        /// <summary>
        /// 文本输入事项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TbSearch_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void TbSearch_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            var text = TbSearch.Text.Trim();
            if (string.IsNullOrEmpty(text))
            {
                BdResult.Visibility = Visibility.Hidden;
                return;
            }

            if (_Plugin != null)
            {
                _Plugin.Text_Changed(e);
            }
        }
        #endregion

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
