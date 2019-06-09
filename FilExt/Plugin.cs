using Me.Amon.Dao;

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

        public void Input(params string[] args)
        {
        }

        public void Enter(params string[] args)
        {
            if (args == null || args.Length < 1)
            {
                return;
            }

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
