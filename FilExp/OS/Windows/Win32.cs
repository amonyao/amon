using Microsoft.Win32;
using System;

namespace Me.Amon.FilExp.OS.Windows
{
    public class Win32 : IOs
    {
        public void AppendContentMenu()
        {
            var file = AppDomain.CurrentDomain.BaseDirectory + "FilExp.exe";
            WriteReg("*\\shell\\FilExp", file);
            WriteReg("Directory\\shell\\FilExp", file);
        }

        public void AppendResMgrListener()
        {
            throw new NotImplementedException();
        }

        public void RemoveContentMenu()
        {
            throw new NotImplementedException();
        }

        public void RemoveResMgrListener()
        {
            throw new NotImplementedException();
        }

        private void WriteReg(string key, string value)
        {
            var rootKey = Registry.ClassesRoot;
            var shell = rootKey.OpenSubKey(key, true);
            if (shell == null)
            {
                shell = rootKey.CreateSubKey(key);
            }

            shell.SetValue("", "天祐ABC");
            shell.SetValue("icon", "1111");

            key += "\\command";
            var command = rootKey.OpenSubKey(key, true);
            if (command == null)
            {
                command = rootKey.CreateSubKey(key);
            }
            command.SetValue("", value);
        }
    }
}
