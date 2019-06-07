using Me.Amon.FilExp.Dto;
using Me.Amon.FilExp.Editor;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Me.Amon.FilExp
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private DocDto _CurrDto;
        public static UserCfg User = new UserCfg();

        public MainWindow()
        {
            InitializeComponent();

            Init();
        }

        public void Init()
        {
            User.Load();

            MyFun.Items.Add(new ListBoxItem { Content = "我的文档", Tag = "doc" });
            MyFun.Items.Add(new ListBoxItem { Content = "我的图片", Tag = "image" });
            MyFun.Items.Add(new ListBoxItem { Content = "我的音频", Tag = "audio" });
            MyFun.Items.Add(new ListBoxItem { Content = "我的视频", Tag = "vedio" });
        }

        private void MyFun_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var funItem = MyFun.SelectedItem as ListBoxItem;
            if (funItem == null)
            {
                return;
            }

            var tag = funItem.Tag;
            switch (tag)
            {
                case "doc":
                    ShowDoc();
                    break;
                case "image":
                    ShowImage();
                    break;
                case "audio":
                    ShowAudio();
                    break;
                case "vedio":
                    ShowVedio();
                    break;
                default:
                    return;
            }
        }

        private DocEditor _DocEditor;
        private void ShowDoc()
        {
            if (_DocEditor == null)
            {
                _DocEditor = new DocEditor();
            }

            MyObj.Children.Clear();
            MyObj.Children.Add(_DocEditor);

            MyUri.Editor = _DocEditor;
        }

        private ImageEditor _ImageEditor;
        private void ShowImage()
        {
            if (_ImageEditor == null)
            {
                _ImageEditor = new ImageEditor();
                _ImageEditor.Init(this, User);
            }

            MyObj.Children.Clear();
            MyObj.Children.Add(_ImageEditor);

            MyUri.Editor = _ImageEditor;
        }

        private AudioEditor _AudioEditor;
        private void ShowAudio()
        {
            if (_AudioEditor == null)
            {
                _AudioEditor = new AudioEditor();
            }

            MyObj.Children.Clear();
            MyObj.Children.Add(_AudioEditor);

            MyUri.Editor = _AudioEditor;
        }

        private VedioEditor _VedioEditor;
        private void ShowVedio()
        {
            if (_VedioEditor == null)
            {
                _VedioEditor = new VedioEditor();
            }

            MyObj.Children.Clear();
            MyObj.Children.Add(_VedioEditor);

            MyUri.Editor = _VedioEditor;
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            try
            {
                var device = e.KeyboardDevice;
                var element = Keyboard.FocusedElement;

                // 重命名
                if (device.IsKeyDown(Key.F2))
                {
                    if (MyUri.Editor != null)
                    {
                        MyUri.Editor.Rename();
                    }
                    return;
                }

                //Ctrl+C 复制
                if ((device.IsKeyDown(Key.LeftCtrl) || device.IsKeyDown(Key.RightCtrl)) && device.IsKeyDown(Key.C))
                {
                    if (element != null && element.GetType().Name == "TextBox") return;
                    CommandCopy();
                }

                //Ctrl+X 剪切
                if ((device.IsKeyDown(Key.LeftCtrl) || device.IsKeyDown(Key.RightCtrl)) && device.IsKeyDown(Key.X))
                {
                    if (IsTextBox(element))
                    {
                        CommandCut();
                    }
                }
                //Ctrl+V 粘贴
                if ((device.IsKeyDown(Key.LeftCtrl) || device.IsKeyDown(Key.RightCtrl)) && device.IsKeyDown(Key.V))
                {
                    if (IsTextBox(element))
                    {
                        CommandPaste();
                    }
                }

                //Ctrl+A 全选
                if ((device.IsKeyDown(Key.LeftCtrl) || device.IsKeyDown(Key.RightCtrl)) && device.IsKeyDown(Key.A))
                {
                    CommandSelectAll();
                }
                //Shift+D 删除
                if ((device.IsKeyDown(Key.LeftShift) || device.IsKeyDown(Key.RightShift)) && device.IsKeyDown(Key.Delete))
                {
                    CommandDelete();
                }
            }
            catch (Exception exp)
            {
            }
        }

        /// <summary>
        /// 重命名
        /// </summary>
        private void CommandRename()
        {
        }

        /// <summary>
        /// 剪切
        /// </summary>
        private void CommandCut()
        {
        }

        /// <summary>
        /// 复制
        /// </summary>
        private void CommandCopy()
        {
        }

        /// <summary>
        /// 粘贴
        /// </summary>
        private void CommandPaste()
        {
        }

        /// <summary>
        /// 删除
        /// </summary>
        private void CommandDelete()
        {
        }

        /// <summary>
        /// 全选
        /// </summary>
        private void CommandSelectAll()
        {
        }

        private bool IsTextBox(IInputElement element)
        {
            return false;
        }

        private void BtnCfg_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new CfgWindow();
            dialog.Owner = this;
            dialog.Init(User);
            dialog.ShowDialog();
        }

        private void BtnUser_Click(object sender, RoutedEventArgs e)
        {
            //new FolderDialog().Show();
        }

        public void ShowUri(string uri)
        {
            MyUri.ShowUri(uri);
        }
    }
}
