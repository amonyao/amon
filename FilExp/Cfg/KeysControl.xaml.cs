using System.Collections.Generic;
using System.Windows.Controls;

namespace Me.Amon.FilExp.Cfg
{
    /// <summary>
    /// KeysControl.xaml 的交互逻辑
    /// </summary>
    public partial class KeysControl : UserControl
    {
        public KeysControl()
        {
            InitializeComponent();
        }

        public void Init()
        {
            var list = new List<HotKeys>();
            list.Add(new HotKeys { Action = "Ctrl + X", Remark = "剪切" });
            list.Add(new HotKeys { Action = "Ctrl + C", Remark = "复制" });
            list.Add(new HotKeys { Action = "Ctrl + V", Remark = "粘贴" });
            list.Add(new HotKeys { Action = "F2", Remark = "重命名" });
            MyGrid.ItemsSource = list;
        }
    }

    public class HotKeys
    {
        public string Action { get; set; }
        public string Remark { get; set; }
    }
}
