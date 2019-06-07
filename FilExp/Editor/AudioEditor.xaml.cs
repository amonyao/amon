using Microsoft.Win32;
using System;
using System.Windows.Controls;

namespace Me.Amon.FilExp.Editor
{
    /// <summary>
    /// AudioEditor.xaml 的交互逻辑
    /// </summary>
    public partial class AudioEditor : UserControl, IEditor
    {
        public AudioEditor()
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
