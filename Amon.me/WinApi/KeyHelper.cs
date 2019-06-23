using System;

namespace Me.Amon.WinApi
{
    public class KeyHelper
    {
        /// <summary>
        /// 发送按键
        /// </summary>
        /// <param name="asiiCode">键盘ascii码</param>
        private void SendKey(byte asiiCode)
        {
            AttachThreadInput(true);
            int getFocus = Win32Api.GetFocus();
            //向前台窗口发送按键消息
            //Win32Api.PostMessage(getFocus, Win32Api.WM_KEYDOWN, asiiCode, );
            AttachThreadInput(false); //取消线程亲和的关联
        }
        /// <summary>
        /// 设置线程亲和,附到前台窗口所在线程,只有在线程内才可以获取线程内控件的焦点
        /// </summary>
        /// <param name="b">是否亲和</param>
        private void AttachThreadInput(bool b)
        {
            //Win32Api.AttachThreadInput(
            //       Win32Api.GetWindowThreadProcessId(
            //       Win32Api.GetForegroundWindow(),),
            //       Win32Api.GetCurrentThreadId(), Convert.ToInt32(b));
        }
    }
}
