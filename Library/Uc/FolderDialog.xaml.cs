using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace Me.Amon.FilExp.Uc
{
    /// <summary>
    /// FolderDialog.xaml 的交互逻辑
    /// </summary>
    public partial class FolderDialog : Window
    {
        public FolderDialog()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            var model = new ObservableCollection<FolderModel>();
            model.Add(new FolderModel { Name = "桌面", Icon = "/Images/Desktop.png", Type = FolderType.Desktop });
            model.Add(new FolderModel { Name = "我的电脑", Icon = "/Images/Computer.png", Type = FolderType.Computer });
            //model.Add(new FolderModel { Name = "我的文档", Icon = "/Images/Computer.png", Type = FolderType.Computer });
            TvTree.ItemsSource = model;
        }

        public bool Multiselect { get; set; }

        public string FileName { get; set; }

        private void BtAccept_Click(object sender, RoutedEventArgs e)
        {
            var item = TvTree.SelectedItem as FolderModel;
            if (item == null)
            {
                LbTips.Text = "请选择一个目录！";
                return;
            }

            FileName = item.Path;
            DialogResult = true;
            Close();
        }

        private void BtCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }

    public enum FolderType
    {
        /// <summary>
        /// 计算机
        /// </summary>
        Computer,
        /// <summary>
        /// 桌面
        /// </summary>
        Desktop,
        /// <summary>
        /// 磁盘驱动器
        /// </summary>
        Disk,
        /// <summary>
        /// 表明这是一个文件夹
        /// </summary>
        Folder,
    }

    public class FolderModel
    {
        public string Icon { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public FolderType Type { get; set; }

        private ObservableCollection<FolderModel> _Children;
        public ObservableCollection<FolderModel> Children
        {
            get
            {
                if (_Children == null)
                {
                    ListItems(this);
                }
                return _Children;
            }
        }

        public bool Loaded { get; set; }

        private void ListItems(FolderModel model)
        {
            if (model.Loaded)
            {
                return;
            }

            if (model.Type == FolderType.Desktop)
            {
                return;
            }

            if (model.Type == FolderType.Computer)
            {
                foreach (var device in Environment.GetLogicalDrives())
                {
                    if (Directory.Exists(device))
                    {
                        var child = new FolderModel();
                        child.Icon = "/Images/Disk.png";
                        child.Name = device;
                        child.Path = device;
                        child.Type = FolderType.Disk;
                        model.AppendChild(child);
                    }
                }
                return;
            }

            if (model.Type == FolderType.Disk || model.Type == FolderType.Folder)
            {
                //var info = new DirectoryInfo(model.Path);
                //if ((info.Attributes & FileAttributes.Directory) == FileAttributes.Directory)
                //{
                //    var ac = Directory.GetAccessControl(model.Path);
                //    if (!ac.AreAccessRulesProtected)
                //    {
                try
                {
                    foreach (var item in Directory.GetDirectories(model.Path))
                    {
                        var di = new DirectoryInfo(item);
                        if ((di.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
                        {
                            continue;
                        }

                        var child = new FolderModel();
                        child.Icon = "/Images/Folder.png";
                        child.Name = System.IO.Path.GetFileName(item);
                        child.Path = item;
                        child.Type = FolderType.Folder;
                        model.AppendChild(child);
                    }
                }
                catch (Exception exp)
                {

                }
                //}
                //}
                return;
            }
        }

        public void AppendChild(FolderModel model)
        {
            if (_Children == null)
            {
                _Children = new ObservableCollection<FolderModel>();
            }
            _Children.Add(model);
        }
    }
}
