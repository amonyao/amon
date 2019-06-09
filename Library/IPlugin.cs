namespace Me.Amon
{
    public interface IPlugin
    {
        /// <summary>
        /// 初始化
        /// </summary>
        void Init(IMainForm form, IUserInfo user);

        /// <summary>
        /// 输入过程
        /// </summary>
        void Input(params string[] args);

        /// <summary>
        /// 输入确认
        /// </summary>
        void Enter(params string[] args);
    }
}
