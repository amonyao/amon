using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Input;

namespace Me.Amon.WinApi
{
    public class KeyboardHook
    {
        private int hHook;
        private Win32Api.HookProc KeyboardHookDelegate;

        public event KeyEventHandler KeyDownEvent;
        //public event KeyPressEventHandler KeyPressEvent;
        public event KeyEventHandler KeyUpEvent;

        /// <summary>
        /// 安装键盘钩子
        /// </summary>
        public void SetHook()
        {
            KeyboardHookDelegate = new Win32Api.HookProc(KeyboardHookProc);
            ProcessModule cModule = Process.GetCurrentProcess().MainModule;
            var mh = Win32Api.GetModuleHandle(cModule.ModuleName);
            hHook = Win32Api.SetWindowsHookEx(Win32Api.WH_KEYBOARD_LL, KeyboardHookDelegate, mh, 0);
            if (hHook == 0)
            {
                UnHook();
                throw new Exception("安装键盘钩子失败");
            }
        }

        /// <summary>
        /// 卸载键盘钩子
        /// </summary>
        public void UnHook()
        {
            bool retKeyboard = true;
            if (hHook != 0)
            {
                retKeyboard = Win32Api.UnhookWindowsHookEx(hHook);
                hHook = 0;
            }

            if (!(retKeyboard)) throw new Exception("卸载钩子失败！");
        }

        /// <summary>
        /// 获取键盘消息
        /// </summary>
        /// <param name="nCode"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        private int KeyboardHookProc(int nCode, Int32 wParam, IntPtr lParam)
        {
            // 如果该消息被丢弃（nCode<0
            if (nCode >= 0)
            {
                Win32Api.KeyboardHookStruct KeyDataFromHook = (Win32Api.KeyboardHookStruct)Marshal.PtrToStructure(lParam, typeof(Win32Api.KeyboardHookStruct));

                int keyData = KeyDataFromHook.vkCode;

                //WM_KEYDOWN和WM_SYSKEYDOWN消息，将会引发OnKeyDownEvent事件
                if (KeyDownEvent != null && (wParam == Win32Api.WM_KEYDOWN || wParam == Win32Api.WM_SYSKEYDOWN))
                {
                    // 此处触发键盘按下事件
                    // keyData为按下键盘的值,对应 虚拟码
                    var key = KeyInterop.KeyFromVirtualKey(keyData);
                    KeyDownEvent(key);
                }

                //WM_KEYUP和WM_SYSKEYUP消息，将引发OnKeyUpEvent事件 
                if (KeyUpEvent != null && (wParam == Win32Api.WM_KEYUP || wParam == Win32Api.WM_SYSKEYUP))
                {
                    // 此处触发键盘抬起事件
                    var key = KeyInterop.KeyFromVirtualKey(keyData);
                    KeyDownEvent(key);
                }
            }

            return Win32Api.CallNextHookEx(hHook, nCode, wParam, lParam);
        }
    }

    public delegate void KeyEventHandler(Key key);
}
