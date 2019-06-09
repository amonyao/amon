using System.Reflection;
using System.Windows;

namespace Me.Amon
{
    /// <summary>
    /// About.xaml 的交互逻辑
    /// </summary>
    public partial class About : Window
    {
        public About()
        {
            InitializeComponent();
        }

        public void Init()
        {
            var assembly = Assembly.GetExecutingAssembly().GetName();
            LcVer.Content = assembly.Version.ToString();
        }

        private void BtAccept_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void LcSite_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://amon.me/");
            }
            catch
            {
                MessageBox.Show("无法启动浏览器，请尝试手动打开！");
            }
        }

        private void LcChat_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Clipboard.SetText("415872667");
            }
            catch
            {
                MessageBox.Show("无法访问剪贴板，请尝试手动访问！");
            }
        }
    }
}
