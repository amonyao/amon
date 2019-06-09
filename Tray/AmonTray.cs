using System;
using System.Windows.Forms;

namespace Me.Amon.Tray
{
    public class AmonTray
    {
        private IMainForm _Main;
        private IUserInfo _User;

        private NotifyIcon _Tray;

        public AmonTray(IMainForm main, IUserInfo user)
        {
            _Main = main;
            _User = user;

            //设置托盘的各个属性
            _Tray = new NotifyIcon();
            _Tray.Text = "Amon.me";
            _Tray.Icon = new System.Drawing.Icon("Logo\\Logo.ico");
            _Tray.Visible = true;
            _Tray.MouseClick += new MouseEventHandler(Tray_MouseClick);
            _Tray.MouseDoubleClick += Tray_MouseDoubleClick;

            var menu = new ContextMenu();

            //设置菜单项
            var about = new MenuItem("关于(&A)");
            about.Click += About_Click;
            menu.MenuItems.Add(about);

            var hide = new MenuItem("隐藏窗口(&A)");
            hide.Click += Hide_Click;
            menu.MenuItems.Add(hide);

            //退出菜单项
            var exit = new MenuItem("退出(&X)");
            exit.Click += Exit_Click;
            menu.MenuItems.Add(exit);

            _Tray.ContextMenu = menu;
        }

        public void ShowTips(string message)
        {
            _Tray.BalloonTipText = message;
            _Tray.ShowBalloonTip(2000);
        }

        private void Tray_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void About_Click(object sender, EventArgs e)
        {
            if (_Main != null)
            {
                _Main.ShowAbout();
            }
        }

        private void Hide_Click(object sender, EventArgs e)
        {
            if (_Main != null)
            {
                _Main.HideWindow();
            }
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            if (_Main != null)
            {
                _Main.Exit();
            }
        }

        private void Tray_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (_Main != null)
                {
                    _Main.ShowWindow();
                }
            }
        }
    }
}
