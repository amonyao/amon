using System.Windows;

namespace Me.Amon
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var window = new MainWindow();
            window.Init();
            window.Show();
        }
    }
}
