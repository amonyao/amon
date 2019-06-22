using Me.Amon.FilExe.Dto;
using System.Windows;

namespace Me.Amon.FilExe
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void Init()
        {
        }

        private void BtAccept_Click(object sender, RoutedEventArgs e)
        {
            var appDto = new AppDto();
            Close();
        }

        private void BtCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
