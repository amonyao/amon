using Me.Amon.FilExe.Dao;
using Me.Amon.FilExe.Dvo;
using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace Me.Amon.FilExe
{
    /// <summary>
    /// CmdEdit.xaml 的交互逻辑
    /// </summary>
    public partial class CmdEdit : Window
    {
        private AppDvo _Dvo;

        public CmdEdit()
        {
            InitializeComponent();
        }

        public void Init(AppDvo dvo)
        {
            _Dvo = dvo;

            if (_Dvo == null)
            {
                _Dvo = new AppDvo();
            }

            TbText.Text = _Dvo.text;
            TbTips.Text = _Dvo.tips;
            TbPath.Text = _Dvo.path;
            TbKeys.Text = _Dvo.keys;
            CkStatus.IsChecked = _Dvo.Enabled;
        }

        private void BtAccept_Click(object sender, RoutedEventArgs e)
        {
            var text = TbText.Text.Trim();
            if (string.IsNullOrEmpty(text))
            {
                LtNotice.Text = "文本不能为空！";
                return;
            }
            var tips = TbTips.Text.Trim();

            var path = TbPath.Text.Trim();
            if (string.IsNullOrEmpty(path))
            {
                LtNotice.Text = "命名不能为空！";
                return;
            }

            var keys = TbKeys.Text.Trim();

            var enabled = CkStatus.IsChecked;

            _Dvo.text = text;
            _Dvo.tips = tips;
            _Dvo.path = Plugin.EncodePath(path);
            _Dvo.file = Path.GetFileNameWithoutExtension(path);
            _Dvo.keys = keys;
            _Dvo.Enabled = enabled ?? false;

            new AppDao().Save(_Dvo);

            Close();
        }

        private void BtCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtPath_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            var result = dialog.ShowDialog(this);
            if (result != true)
            {
                return;
            }

            var file = dialog.FileName;
            TbPath.Text = file;
            TbKeys.Text = Path.GetFileNameWithoutExtension(file);
        }
    }
}
