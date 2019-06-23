using System.Windows;
using System.Windows.Controls;

namespace Me.Amon
{
    public interface IMainForm
    {
        #region 窗口相关
        /// <summary>
        /// 显示窗口
        /// </summary>
        void ShowWindow();

        /// <summary>
        /// 隐藏窗口
        /// </summary>
        void HideWindow();
        #endregion

        #region 命令相关
        void ShowCommand(bool visible);

        string CommandText { get; set; }
        #endregion

        #region 插件相关
        /// <summary>
        /// 追加用户控件
        /// </summary>
        /// <param name="control"></param>
        void AddPluginView(UserControl control);

        /// <summary>
        /// 显示用户控件
        /// </summary>
        /// <param name="key"></param>
        void SetPluginView(string key, Visibility visibility);

        void ShowPluginIcon(string icon);
        #endregion

        /// <summary>
        /// 关于
        /// </summary>
        void ShowAbout();

        /// <summary>
        /// 系统退出
        /// </summary>
        void Exit();
    }
}
