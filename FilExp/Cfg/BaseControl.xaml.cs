using Me.Amon.FilExp.Dto;
using Me.Amon.FilExp.Uc;
using System.Windows;
using System.Windows.Controls;

namespace Me.Amon.FilExp.Cfg
{
    /// <summary>
    /// BaseControl.xaml 的交互逻辑
    /// </summary>
    public partial class BaseControl : UserControl
    {
        private UserCfg _Cfg;

        public BaseControl()
        {
            InitializeComponent();
        }

        public void Init(UserCfg cfg)
        {
            _Cfg = cfg;
            if (cfg == null)
            {
                return;
            }

            TbDataDir.Text = _Cfg.RootDir.Replace("/", "\\");
        }

        private void BtDataDir_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new FolderDialog();
            var result = dialog.ShowDialog();
            if (result != null && !result.Value)
            {
                return;
            }

            TbDataDir.Text = dialog.FileName;
            _Cfg.RootDir = dialog.FileName.Replace("\\", "/");
        }
    }
}
