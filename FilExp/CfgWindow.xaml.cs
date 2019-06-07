using Me.Amon.FilExp.Dto;
using System.Windows;
using System.Windows.Controls;

namespace Me.Amon.FilExp
{
    /// <summary>
    /// CfgWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CfgWindow : Window
    {
        private UserCfg _Cfg;

        public CfgWindow()
        {
            InitializeComponent();
        }

        public void Init(UserCfg cfg)
        {
            _Cfg = cfg;
            UcBase.Init(cfg);
            UcKeys.Init();
        }

        private void LbTab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = LbTab.SelectedItem as ListBoxItem;
            if (item == null)
            {
                return;
            }

            HideAll();

            var key = item.Tag as string;
            if (key == "base")
            {
                UcBase.Visibility = Visibility.Visible;
                return;
            }
            if (key == "json")
            {
                UcJson.Visibility = Visibility.Visible;
                return;
            }
            if (key == "info")
            {
                UcInfo.Visibility = Visibility.Visible;
                return;
            }
            if (key == "keys")
            {
                UcKeys.Visibility = Visibility.Visible;
                return;
            }
        }

        private void HideAll()
        {
            UcBase.Visibility = Visibility.Collapsed;
            UcKeys.Visibility = Visibility.Collapsed;
            UcJson.Visibility = Visibility.Collapsed;
            UcInfo.Visibility = Visibility.Collapsed;
        }

        private void BtAccept_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
