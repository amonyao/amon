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
    }
}
