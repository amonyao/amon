using Me.Amon.FilExe.Dao;
using Me.Amon.FilExe.Dvo;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
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
                System.Diagnostics.Process.Start(item.path);
                _Main.ShowUserView("", Visibility.Collapsed);
                _Main.ShowSearch(false);
            }
            catch
            {
                MessageBox.Show("无法启动浏览器，请尝试手动打开！");
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
                item.IsMatch(meta, list1, list2);
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
    }
}
