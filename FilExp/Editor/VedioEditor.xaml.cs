using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Me.Amon.FilExp.Editor
{
    /// <summary>
    /// VedioEditor.xaml 的交互逻辑
    /// </summary>
    public partial class VedioEditor : UserControl,IEditor
    {
        public VedioEditor()
        {
            InitializeComponent();
        }

        #region 接口实现
        public void Create()
        {
        }

        public void Rename()
        {

        }

        public void Delete()
        {
        }

        public void Goto(string uri)
        {
        }

        public void Home()
        {
        }

        public void Cut()
        {
        }

        public void Copy()
        {
        }

        public void Paste()
        {
        }

        public void ImportByDoc()
        {
        }

        public void ImportByCat()
        {
        }
        #endregion
    }
}
