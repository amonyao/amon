using Me.Amon.Dao;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Me.Amon.FilExt
{
    public class Plugin : IPlugin
    {
        private IMainForm _Main;
        private IUserInfo _User;

        #region 接口实现
        public void Init(IMainForm form, IUserInfo user)
        {
            _Main = form;
            _User = user;
        }

        public void Meta_KeyDown(KeyEventArgs e)
        {
        }

        public void Text_Changed(TextChangedEventArgs e) { }

        public void Drag_Enter(DragEventArgs e)
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

        public void Drag_Droped(DragEventArgs e)
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
            if (list == null || list.Length < 1)
            {
                return;
            }

            var rules = new RuleDao().List<RuleDto>(null);
            if (rules == null)
            {
                return;
            }

            var window = new FileList();
            window.Init(rules, list);
            window.Show();
        }
        #endregion
    }
}
