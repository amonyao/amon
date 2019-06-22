using System.Windows;
using System.Windows.Controls;

namespace Me.Amon
{
    public interface IMainForm
    {
        /// <summary>
        /// 追加用户控件
        /// </summary>
        /// <param name="control"></param>
        void AddUserView(UserControl control);

        /// <summary>
        /// 显示用户控件
        /// </summary>
        /// <param name="key"></param>
        void ShowUserView(string key, Visibility visibility);

        void ShowAppIcon(string icon);

        /// <summary>
        /// 显示窗口
        /// </summary>
        void ShowWindow();

        /// <summary>
        /// 隐藏窗口
        /// </summary>
        void HideWindow();

        void ShowSearch(bool visible);

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
