using System.Windows;
using System.Windows.Input;

namespace Me.Amon
{
    /// <summary>
    /// AidWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AidWindow : Window
    {
        public AidWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
