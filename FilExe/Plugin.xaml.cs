using Me.Amon.FilExe.Dao;
using Me.Amon.FilExe.Dvo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Me.Amon.FilExe
{
    /// <summary>
    /// Plugin.xaml 的交互逻辑
    /// </summary>
    public partial class Plugin : UserControl, IPlugin
    {
        private IMainForm _Main;
        private IUserInfo _User;
        private ObservableCollection<AppDvo> _Files = new ObservableCollection<AppDvo>();
        private IEnumerable<AppDvo> _List;

        private static string StartUp;

        public Plugin()
        {
            InitializeComponent();
        }

        #region 接口实现
        public void Init(IMainForm form, IUserInfo user)
        {
            _Main = form;
            _User = user;

            StartUp = AppDomain.CurrentDomain.BaseDirectory;

            _Main.AddPluginView(this);

            LbResult.ItemsSource = _Files;

            var dvo = new AppDvo { os = "win10" };
            _List = new AppDao().List<AppDvo>(dvo);
            foreach (var item in _List)
            {
                item.Decode();
            }
        }

        public void Text_Changed(TextChangedEventArgs e)
        {
            var text = _Main.CommandText;
            if (string.IsNullOrWhiteSpace(text))
            {
                return;
            }

            Search(text);
        }

        public void Meta_KeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                SelectPrev();
                return;
            }
            if (e.Key == Key.Down)
            {
                SelectNext();
                return;
            }

            if (e.Key == Key.Enter)
            {
                Execute();
                return;
            }
        }

        public void Drag_Enter(DragEventArgs e)
        {

        }

        public void Drag_Droped(DragEventArgs e)
        {

        }
        #endregion

        #region 事件处理
        private void LbResult_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Execute();
        }

        private void LbResult_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key != System.Windows.Input.Key.Enter)
            {
                return;
            }

            Execute();
        }
        #endregion

        /// <summary>
        /// 数据查找
        /// </summary>
        /// <param name="meta"></param>
        private void Search(string meta)
        {
            _Files.Clear();

            meta = meta.ToLower();

            var list1 = new List<AppDvo>();
            var list2 = new List<AppDvo>();
            foreach (var item in _List)
            {
                item.IsMatch(meta, list1, list2);
            }

            SortList(list1);
            SortList(list2);

            if (_Files.Count > 0)
            {
                _Main.SetPluginView("", System.Windows.Visibility.Visible);
            }
            else
            {
                _Main.SetPluginView("", System.Windows.Visibility.Collapsed);
            }

            LbResult.SelectedIndex = 0;
        }

        private void SortList(List<AppDvo> list)
        {
            for (int i = 0; i < list.Count; i += 1)
            {
                var src = list[i];
                for (int j = i + 1; j < list.Count; j += 1)
                {
                    var dst = list[j];
                    if (src.od < dst.od)
                    {
                        list[i] = dst;
                        list[j] = src;
                        src = dst;
                    }
                }
                _Files.Add(src);
            }
        }

        /// <summary>
        /// 将全路径转换为相对路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string EncodePath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return path;
            }

            if (path.IndexOf(System.IO.Path.DirectorySeparatorChar) < 0)
            {
                return path;
            }

            path = path.Replace(StartUp, ".\\");

            var types = typeof(Environment.SpecialFolder);
            var keys = Enum.GetNames(types);
            foreach (var key in keys)
            {
                var value = (Environment.SpecialFolder)Enum.Parse(types, key);
                var temp = Environment.GetFolderPath(value);
                if (!string.IsNullOrWhiteSpace(temp))
                {
                    path = Regex.Replace(path, Regex.Escape(temp), '<' + key + '>', RegexOptions.IgnoreCase);
                }
            }

            return path;
        }

        /// <summary>
        /// 将相对路径转换为全路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string DecodePath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return path;
            }

            if (path.IndexOf(System.IO.Path.DirectorySeparatorChar) < 0)
            {
                return path;
            }

            path = path.Replace(".\\", StartUp);

            var types = typeof(Environment.SpecialFolder);
            var matches = Regex.Matches(path, "<\\w+>");
            foreach (Match match in matches)
            {
                if (!match.Success)
                {
                    continue;
                }

                var key = match.Value;
                if (string.IsNullOrWhiteSpace(key) || key.Length < 2)
                {
                    continue;
                }

                var enums = key.Substring(1, key.Length - 2);
                if (!Enum.IsDefined(types, enums))
                {
                    return path;
                }

                var value = (Environment.SpecialFolder)Enum.Parse(types, enums);
                var temp = Environment.GetFolderPath(value);
                path = path.Replace(key, temp);
            }

            return path;
        }

        /// <summary>
        /// 执行命令
        /// </summary>
        private void Execute()
        {
            if (_Files.Count < 1)
            {
                return;
            }

            var item = LbResult.SelectedItem as AppDvo;
            if (item == null)
            {
                item = _Files[0];
                if (item == null)
                {
                    return;
                }
            }

            try
            {
                var path = DecodePath(item.path);
                System.Diagnostics.Process.Start(path);
                _Main.SetPluginView("", Visibility.Collapsed);
                _Main.ShowCommand(false);
            }
            catch
            {
                MessageBox.Show("无法启动浏览器，请尝试手动打开！");
            }
        }

        private void SelectPrev()
        {
            var cnt = LbResult.Items.Count;
            if (cnt < 2)
            {
                return;
            }

            var idx = LbResult.SelectedIndex;
            idx -= 1;
            if (idx < 0)
            {
                idx = cnt - 1;
            }

            LbResult.SelectedIndex = idx;
            LbResult.ScrollIntoView(LbResult.Items[idx]);
        }

        private void SelectNext()
        {
            var cnt = LbResult.Items.Count;
            if (cnt < 2)
            {
                return;
            }

            var idx = LbResult.SelectedIndex;
            idx += 1;
            if (idx >= cnt)
            {
                idx = 0;
            }

            LbResult.SelectedIndex = idx;
            LbResult.ScrollIntoView(LbResult.Items[idx]);
        }
    }
}
