using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
        void Meta_KeyDown(KeyEventArgs e);

        void Text_Changed(TextChangedEventArgs e);

        /// <summary>
        /// 拖放进入
        /// </summary>
        /// <param name="e"></param>
        void Drag_Enter(DragEventArgs e);

        /// <summary>
        /// 拖放完成
        /// </summary>
        /// <param name="e"></param>
        void Drag_Droped(DragEventArgs e);
    }
}
