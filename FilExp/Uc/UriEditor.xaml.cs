using System.Windows;
using System.Windows.Controls;

namespace Me.Amon.FilExp.Uc
{
    /// <summary>
    /// UriEditor.xaml 的交互逻辑
    /// </summary>
    public partial class UriEditor : UserControl
    {
        public IEditor Editor { get; set; }

        public UriEditor()
        {
            InitializeComponent();
        }

        public void ShowUri(string uri)
        {
            MyUri.Text = uri;
        }

        private void BtCopyto_Click(object sender, RoutedEventArgs e)
        {
            if (Editor != null)
            {
                Editor.Copy();
            }
        }

        private void BtPaste_Click(object sender, RoutedEventArgs e)
        {
            if (Editor != null)
            {
                Editor.Paste();
            }
        }

        private void BtMoveto_Click(object sender, RoutedEventArgs e)
        {
            if (Editor != null)
            {
                Editor.Cut();
            }
        }

        private void BtAppend_Click(object sender, RoutedEventArgs e)
        {
            if (Editor != null)
            {
                Editor.Create();
            }
        }

        private void BtDelete_Click(object sender, RoutedEventArgs e)
        {
            if (Editor != null)
            {
                Editor.Delete();
            }
        }

        private void BtHome_Click(object sender, RoutedEventArgs e)
        {
            if (Editor != null)
            {
                Editor.Home();
            }
        }

        private void BtImportDoc_Click(object sender, RoutedEventArgs e)
        {
            if (Editor != null)
            {
                Editor.ImportByDoc();
            }
        }

        private void BtImportCat_Click(object sender, RoutedEventArgs e)
        {
            if (Editor != null)
            {
                Editor.ImportByCat();
            }
        }
    }
}
