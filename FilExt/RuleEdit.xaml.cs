using Me.Amon.Dao;
using Me.Amon.FilExp.Uc;
using Me.Amon.FilExt.Dto;
using Me.Amon.FilExt.Dvo;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Me.Amon.FilExt
{
    /// <summary>
    /// RuleWindow.xaml 的交互逻辑
    /// </summary>
    public partial class RuleEdit : Window
    {
        private RuleDto _Dto;

        public RuleEdit()
        {
            InitializeComponent();

            TbSrcPath.IsEnabled = false;
            BtSrcPath.IsEnabled = false;

            TbDstFile.IsEnabled = false;
            BtDstFile.IsEnabled = false;
        }

        public void Init(RuleDto dto)
        {
            _Dto = dto;
            if (_Dto == null)
            {
                _Dto = new RuleDto();
            }

            TbName.Text = _Dto.name;
            TbSrcFile.Text = _Dto.src_file;
            TbSrcPath.Text = _Dto.src_path;
            TbDstPath.Text = _Dto.dst_path;
            TbDstFile.Text = _Dto.dst_file;

            ShowMethod();
            ShowRepeat();
        }

        private void ShowMethod()
        {
            var method = new MethodDvo();
            var list = new DictDao().List<MethodDvo>(method);

            var idx = 0;
            foreach (var item in list)
            {
                CbMethod.Items.Add(item);

                if (item.key == _Dto.method)
                {
                    CbMethod.SelectedIndex = idx;
                }
                idx += 1;
            }
        }

        private void ShowRepeat()
        {
            var repeat = new RepeatDvo();
            var list = new DictDao().List<RepeatDvo>(repeat);

            var idx = 0;
            foreach (var item in list)
            {
                CbRepeat.Items.Add(item);

                if (item.key == _Dto.repeat)
                {
                    CbRepeat.SelectedIndex = idx;
                }
                idx += 1;
            }
        }

        #region 事件处理
        private void BtSrcPath_Click(object sender, RoutedEventArgs e)
        {
            ChooseFolder(TbSrcPath);
        }

        private void BtDstPath_Click(object sender, RoutedEventArgs e)
        {
            ChooseFolder(TbDstPath);
        }

        private void CbDstFile_Click(object sender, RoutedEventArgs e)
        {
            ContextMenu cm = this.FindResource("RenameMenu") as ContextMenu;
            cm.PlacementTarget = sender as Button;
            cm.IsOpen = true;
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtUpdate_Click(object sender, RoutedEventArgs e)
        {
            var name = TbName.Text.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                LcError.Text = "名称不能为空！";
                TbName.Focus();
                return;
            }

            var method = CbMethod.SelectedItem as MethodDto;
            if (method == null)
            {
                LcError.Text = "请选择一个操作！";
                CbMethod.Focus();
                return;
            }

            var srcFile = TbSrcFile.Text.Trim();
            if (string.IsNullOrWhiteSpace(srcFile))
            {
                LcError.Text = "来源文件不能为空！";
                TbSrcFile.Focus();
                return;
            }

            var srcPath = TbSrcPath.Text.Trim();
            if (string.IsNullOrWhiteSpace(srcPath) && string.IsNullOrWhiteSpace(srcFile))
            {
                LcError.Text = "来源文件及路径不能同时为空！";
                TbSrcPath.Focus();
                return;
            }

            var dstPath = TbDstPath.Text.Trim();
            if (string.IsNullOrWhiteSpace(srcFile))
            {
                LcError.Text = "目标路径不能为空！";
                TbDstPath.Focus();
                return;
            }

            var dstFile = TbDstFile.Text.Trim();
            if (string.IsNullOrWhiteSpace(dstFile) && string.IsNullOrWhiteSpace(dstPath))
            {
                LcError.Text = "目标文件及路径不能同时为空！";
                TbDstFile.Focus();
                return;
            }

            var repeat = CbRepeat.SelectedItem as RepeatDto;
            if (repeat == null)
            {
                LcError.Text = "请选择重名处理！";
                CbRepeat.Focus();
                return;
            }

            _Dto.name = name;
            _Dto.method = method.key;
            _Dto.src_file = srcFile;
            _Dto.src_path = srcPath;
            _Dto.dst_path = dstPath;
            _Dto.dst_file = dstFile;
            _Dto.repeat = repeat.key;
            _Dto.status = RuleDto.STATUS_1;
            _Dto.update_time = DateTime.Now;

            if (_Dto.create_time < DateTime.MinValue)
            {
                _Dto.create_time = _Dto.update_time;
            }

            new RuleDao().Save(_Dto);

            DialogResult = true;
            Close();
        }
        #endregion

        private void ChooseFolder(TextBox input)
        {
            var dialog = new FolderDialog();
            dialog.FileName = input.Text;
            dialog.Owner = this;
            var result = dialog.ShowDialog();
            if (result != null && result.Value)
            {
                input.Text = dialog.FileName;
            }
        }
    }
}
