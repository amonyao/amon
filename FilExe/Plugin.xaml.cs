using Me.Amon.FilExe.Dao;
using Me.Amon.FilExe.Dvo;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows.Controls;

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

        public Plugin()
        {
            InitializeComponent();
        }

        #region 接口实现
        public void Init(IMainForm form, IUserInfo user)
        {
            _Main = form;
            _User = user;

            _Main.AddUserView(this);

            LbResult.ItemsSource = _Files;

            var dvo = new AppDvo { os = "win10" };
            _List = new AppDao().List<AppDvo>(dvo);
            foreach (var item in _List)
            {
                item.Decode();
            }
        }

        public void Input(params string[] args)
        {
            if (args == null || args.Length < 1)
            {
                return;
            }

            var key = args[0];
            Search(key);
        }

        public void Enter(params string[] args)
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
                System.Diagnostics.Process.Start(item.file);
            }
            catch
            {
                //MessageBox.Show("无法启动浏览器，请尝试手动打开！");
            }
        }
        #endregion

        private void Search(string meta)
        {
            _Files.Clear();

            meta = meta.ToLower();

            var list1 = new List<AppDvo>();
            var list2 = new List<AppDvo>();
            foreach (var item in _List)
            {
                foreach (var key in item.kkkk)
                {
                    if (key.IndexOf(meta) >= 0)
                    {
                        list1.Add(item);
                        continue;
                    }
                    if (IsMatch(key, meta))
                    {
                        list2.Add(item);
                        continue;
                    }
                }
            }

            SortList(list1);
            SortList(list2);

            if (_Files.Count > 0)
            {
                _Main.ShowUserView("", System.Windows.Visibility.Visible);
            }
            else
            {
                _Main.ShowUserView("", System.Windows.Visibility.Collapsed);
            }

            LbResult.SelectedIndex = 0;
        }

        private List<AppDvo> SearchByPattern(string pattern)
        {
            var list = new List<AppDvo>();

            foreach (var item in _List)
            {
                if (item.kkkk == null || item.kkkk.Length < 1)
                {
                    continue;
                }

                foreach (var key in item.kkkk)
                {
                    if (Regex.IsMatch(key, pattern, RegexOptions.IgnoreCase))
                    {
                        list.Add(item);
                        break;
                    }
                }
            }

            return list;
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
        /// 按关键时顺序匹配
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        private bool IsMatch(string input, string pattern)
        {
            if (string.IsNullOrWhiteSpace(input) || string.IsNullOrWhiteSpace(pattern))
            {
                return false;
            }

            var srcIdx = 0;
            var srcQty = input.Length;
            var dstIdx = 0;
            var dstQty = pattern.Length;

            while (true)
            {
                if (pattern[dstIdx] == input[srcIdx])
                {
                    dstIdx += 1;
                }
                srcIdx += 1;

                if (dstIdx >= dstQty)
                {
                    return true;
                }

                if (srcIdx >= srcQty)
                {
                    return false;
                }
            }
        }
    }
}
