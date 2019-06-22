using Me.Amon.FilExp.Dto;
using System.Windows.Controls;

namespace Me.Amon.FilExp.Editor
{
    /// <summary>
    /// DocEditor.xaml 的交互逻辑
    /// </summary>
    public partial class DocEditor : UserControl, IEditor
    {
        private DocDto _CurrDto;

        public DocEditor()
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
