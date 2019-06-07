using System.Windows;
using System.Windows.Controls;

namespace Me.Amon.FilExp.Uc
{
    /// <summary>
    /// InputDialog.xaml 的交互逻辑
    /// </summary>
    public partial class InputDialog : Window
    {
        private InputHandler _Handler;

        public InputDialog()
        {
            InitializeComponent();
        }

        public bool? ShowDialog(Window owner, InputHandler handler)
        {
            this.Owner = owner;
            this._Handler = handler;
            return ShowDialog();
        }

        public string Text
        {
            get
            {
                return TbInput.Text.Trim();
            }
            set
            {
                TbInput.Text = value;
            }
        }

        private void BtCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void BtAccept_Click(object sender, RoutedEventArgs e)
        {
            Accept();
        }

        private void TbInput_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key != System.Windows.Input.Key.Enter)
            {
                return;
            }

            e.Handled = true;
            Accept();
        }

        private void Accept()
        {
            if (_Handler != null)
            {
                if (!_Handler(TbInput.Text, LtInput))
                {
                    LtInput.Focus();
                    return;
                }
            }

            DialogResult = true;
            Close();
        }
    }

    public delegate bool InputHandler(string input, TextBlock error);
}
