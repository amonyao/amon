using Me.Amon.Dao;

namespace Me.Amon.FilExt
{
    public class Plugin : IPlugin
    {
        private MainForm _Main;
        private UserInfo _User;

        #region 接口实现
        public void Init(MainForm form, UserInfo user)
        {
            _Main = form;
            _User = user;
        }

        public void Input()
        {
        }

        public void Enter(params string[] args)
        {
            var rules = new RuleDao().List<RuleDto>(null);
            if (rules == null)
            {
                return;
            }

            var window = new FileList();
            window.Init(rules, args);
            window.Show();
        }
        #endregion
    }
}
