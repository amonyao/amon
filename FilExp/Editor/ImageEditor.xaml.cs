using Me.Amon.Dao;
using Me.Amon.FilExp.Dto;
using Me.Amon.FilExp.Uc;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace Me.Amon.FilExp.Editor
{
    /// <summary>
    /// ImageEditor.xaml 的交互逻辑
    /// </summary>
    public partial class ImageEditor : UserControl, IEditor
    {
        private MainWindow _Main;
        /// <summary>
        /// 当前目录
        /// </summary>
        private DocDto _CurrCat;
        /// <summary>
        /// 根目录
        /// </summary>
        private CatDto _RootCat;
        private UserCfg _Cfg;
        private ObservableCollection<DocDto> _Docs = new ObservableCollection<DocDto>();
        private object _LockObj = new object();

        public ImageEditor()
        {
            InitializeComponent();
        }

        public void Init(MainWindow main, UserCfg cfg)
        {
            _Main = main;
            _Cfg = cfg;

            MyView.ItemsSource = _Docs;
            //MyView.SetBinding(ItemsControl.ItemsSourceProperty, _Docs);
            BindingOperations.EnableCollectionSynchronization(_Docs, _LockObj);

            var path = Path.Combine(cfg.RootDir, "img");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            _RootCat = new CatImgDto();
            _RootCat.names = "我的照片";
            _RootCat.path = path;
            _RootCat.id = CatDto.TYPE_60_CODE;
            _RootCat.Init(cfg);
            _CurrCat = _RootCat;

            MyCat.ItemsSource = _RootCat.CatList;
        }

        #region 接口实现
        public void Create()
        {
            CreateCat(_CurrCat);
        }

        public void Rename()
        {
            if (MyCat.IsFocused)
            {
                RenameCat();
                return;
            }

            if (MyView.IsFocused)
            {
                RenameDoc();
                return;
            }
        }

        public void Copy()
        {
            var item = MyView.SelectedItem;
            if (item == null)
            {
                return;
            }

            var doc = item as DocDto;
            if (doc == null)
            {
                return;
            }

            Clipboard.SetData(Env.ClipDataFormat, doc);
        }

        public void Paste()
        {
            var obj = GetDataObjectFromClipboard();
            if (obj == null)
            {
                return;
            }

            if (obj.GetDataPresent(DataFormats.FileDrop))
            {
                PasteFile(obj);
                return;
            }

            if (obj.GetDataPresent(Env.ClipDataFormat))
            {
                PasteDoc(obj);
                return;
            }
        }

        public void Delete()
        {
            var items = MyView.SelectedItems;
            var idx = items.Count;
            for (var i = items.Count - 1; i >= 0; i -= 1)
            {
                var item = items[i];
                if (!(item is DocDto))
                {
                    continue;
                }

                var doc = item as DocDto;
                DeleteImg(doc);

                _Docs.Remove(doc);
            }
        }

        public void Goto(string uri)
        {
        }

        public void Home()
        {
            var item = MyCat.Items[0] as TreeViewItem;
            item.IsSelected = true;
        }

        public void Cut()
        {
        }

        public void ImportByDoc()
        {
            var dialog = new OpenFileDialog();
            dialog.Multiselect = true;
            dialog.Filter = "图像文件|*.*";
            if (dialog.ShowDialog() != true)
            {
                return;
            }

            var files = dialog.FileNames;
            ImportImg(_CurrCat, files);
        }

        public void ImportByCat()
        {
            var dialog = new FolderDialog();
            dialog.Owner = _Main;
            dialog.Multiselect = true;
            if (dialog.ShowDialog() != true)
            {
                return;
            }

            var file = dialog.FileName;
            ImportCat(_CurrCat, file);
        }
        #endregion

        #region 事件处理
        private void MyCat_SelectedItemChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<object> e)
        {
            var doc = MyCat.SelectedItem as DocDto;
            if (doc == null)
            {
                return;
            }

            _CurrCat = doc;
            _Main.ShowUri(doc.FullRelativeFile);

            ListImgAsync();
        }

        private void MyCat_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var cat = MyCat.SelectedItem as DocDto;
            if (cat == null)
            {
                return;
            }
        }

        private void MyView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = MyView.SelectedItem;
            if (item == null)
            {
                return;
            }

            var doc = item as DocDto;
            if (doc == null)
            {
                return;
            }

            MyTag.ListTag(doc);
        }

        private void TreeViewItem_MouseRightButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        private void TreeViewItem_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var treeViewItem = VisualUpwardSearch<TreeViewItem>(e.OriginalSource as DependencyObject) as TreeViewItem;
            if (treeViewItem != null)
            {
                treeViewItem.Focus();
                e.Handled = true;
            }
        }

        private void CatMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var item = sender as MenuItem;
            if (item == null)
            {
                return;
            }

            var cat = MyCat.SelectedItem as DocDto;
            if (cat == null)
            {
                return;
            }

            var tag = item.Tag as string;
            if (tag == "append")
            {
                CreateCat(cat);
                return;
            }
            if (tag == "update")
            {
                UpdateCat(cat);
                return;
            }
            if (tag == "remove")
            {
                DeleteCat(cat);
                return;
            }
        }

        private void ImgMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var item = sender as MenuItem;
            if (item == null)
            {
                return;
            }

            var doc = MyView.SelectedItem as DocDto;
            if (doc == null)
            {
                return;
            }

            var tag = item.Tag as string;
            if (tag == "append")
            {
                //CreateCat(doc);
                return;
            }
            if (tag == "update")
            {
                UpdateImg(doc);
                return;
            }
            if (tag == "remove")
            {
                DeleteImg(doc);
                return;
            }
        }
        #endregion

        #region 目录操作
        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="parent"></param>
        public void CreateCat(DocDto parent)
        {
            if (parent == null)
            {
                return;
            }

            parent.BuildCatKeys();

            var dialog = new InputDialog();
            var result = dialog.ShowDialog(_Main, (input, error) =>
            {
                input = (input ?? "").Trim();
                if (input == null)
                {
                    error.Text = "请输入一个有效的目录名称：";
                    return false;
                }
                if (parent.CatNameExist(input))
                {
                    error.Text = $"已存在名为 {input} 的目录！";
                    return false;
                }

                var cat = new CatImgDto();
                cat.names = input;
                cat.path = parent.FullRelativeFile + "/" + input;
                cat.pid = parent.id;
                cat.Init(_Cfg);

                parent.AppendCatItem(cat);

                new DocDao().Save(cat);

                if (!Directory.Exists(cat.FullPhysicalFile))
                {
                    Directory.CreateDirectory(cat.FullPhysicalFile);
                }
                return true;
            });
        }

        private void RenameCat()
        {
            var item = MyCat.SelectedItem as TreeViewItem;
            if (item == null)
            {
                return;
            }

            var doc = item.Tag as DocDto;
            if (doc == null)
            {
                return;
            }

            var dialog = new InputDialog();
            dialog.Text = doc.names;
            var result = dialog.ShowDialog(_Main, (input, error) =>
            {
                return true;
            });

            var text = dialog.Text;
            item.Header = text;
            doc.names = text;
            new DocDao().Save(doc);
        }

        /// <summary>
        /// 以目录为单位导入
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="path"></param>
        private int ImportCat(DocDto dto, string path)
        {
            var qty = 0;

            var dirs = Directory.GetDirectories(path);
            if (dirs != null)
            {
                var docDao = new DocDao();
                foreach (var dir in dirs)
                {
                    var catDto = new CatImgDto();
                    catDto.path = dir;
                    catDto.names = Path.GetFileName(dir);
                    catDto.pid = dto.id;
                    docDao.Save(catDto);

                    qty += ImportCat(catDto, path);
                }
            }

            var files = System.IO.Directory.GetFiles(path);
            qty += ImportImg(dto, files);

            return qty;
        }
        #endregion

        /// <summary>
        /// 更新目录
        /// </summary>
        /// <param name="node"></param>
        /// <param name="doc"></param>
        public void UpdateCat(DocDto doc)
        {
            var parent = doc.Parent;
            if (parent == null)
            {
                return;
            }
            parent.BuildCatKeys();

            var dialog = new InputDialog();
            dialog.Text = doc.names;
            var result = dialog.ShowDialog(_Main, (input, error) =>
            {
                input = (input ?? "").Trim();
                if (input == null)
                {
                    error.Text = "请输入一个有效的目录名称：";
                    return false;
                }
                if (input.ToUpper() != doc.names.ToUpper())
                {
                    if (parent.CatNameExist(input))
                    {
                        error.Text = $"已存在名为 {input} 的目录！";
                        return false;
                    }
                }

                doc.path = parent.FullRelativeFile + "/" + input;
                doc.names = input;

                new DocDao().Save(doc);
                return true;
            });
        }

        /// <summary>
        /// 删除目录
        /// </summary>
        /// <param name="doc"></param>
        private void DeleteCat(DocDto doc)
        {
            var parent = doc.Parent;
            if (parent == null)
            {
                return;
            }

            var docDao = new DocDao();
            var items = docDao.List<DocDto>(doc);
            if (items != null && items.GetEnumerator().MoveNext())
            {
                MessageBox.Show("目录不为空，不能删除！");
                return;
            }

            docDao.Delete(doc.id);
            parent.CatList.Remove(doc);
        }

        #region 图片操作
        private void ListImgAsync()
        {
            Task.Run(() =>
            {
                // 代码写在 lock 块中
                lock (_LockObj)
                {
                    ListDoc(_CurrCat);
                }
            });
        }

        private void ListDoc(DocDto doc)
        {
            _Docs.Clear();

            var list = new DocImgDao().ListDoc(doc);
            var keyDao = new KeyDao();
            foreach (var item in list)
            {
                item.Init(_Cfg);
                doc.AppendDocItem(item);
                var key = keyDao.Read<KeyDto>(item.key);
                if (key != null)
                {
                    item.path = key.file;
                }
                item.Prepare();

                _Docs.Add(item);
            }
        }
        private void RenameDoc()
        {
            var item = MyView.SelectedItem;
            if (item == null)
            {
                return;
            }

            var doc = item as DocDto;
            if (doc == null)
            {
                return;
            }

            var dialog = new InputDialog();
            dialog.Text = doc.names;
            var result = dialog.ShowDialog();
            if (result == null || !result.Value)
            {
                return;
            }

            var text = dialog.Text;
            //item. = text;
            doc.names = text;
            new DocDao().Save(doc);
        }

        /// <summary>
        /// 修改文件
        /// </summary>
        /// <param name="doc"></param>
        private void UpdateImg(DocDto doc)
        {
            var parent = doc.Parent;
            if (parent == null)
            {
                return;
            }
            parent.BuildDocKeys();

            var dialog = new InputDialog();
            dialog.Text = doc.names;
            var result = dialog.ShowDialog(_Main, (input, error) =>
            {
                input = (input ?? "").Trim();
                if (input == null)
                {
                    error.Text = "请输入一个有效的文件名称：";
                    return false;
                }
                if (input.ToUpper() != doc.names.ToUpper())
                {
                    if (parent.CatNameExist(input))
                    {
                        error.Text = $"已存在名为 {input} 的文件！";
                        return false;
                    }
                }

                doc.names = input;

                new DocDao().Save(doc);

                return true;
            });
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="doc"></param>
        private void DeleteImg(DocDto doc)
        {
            if (doc == null)
            {
                return;
            }
            new DocDao().Delete(doc.id);
            new KeyDao().Change(doc.key, -1);
        }

        /// <summary>
        /// 以文件为单位导入
        /// </summary>
        /// <param name="cat"></param>
        /// <param name="files"></param>
        /// <returns>成功导入的数量</returns>
        private int ImportImg(DocDto cat, string[] files)
        {
            if (files == null)
            {
                return 0;
            }

            var qty = 0;
            var docDao = new DocImgDao();
            var keyDao = new KeyDao();
            foreach (var srcFile in files)
            {
                var docDto = ImportImg(docDao, keyDao, cat, srcFile);
                if (docDto == null)
                {
                    continue;
                }

                _Docs.Add(docDto);
                qty += 1;
            }

            return qty;
        }

        /// <summary>
        /// 执行文件导入
        /// </summary>
        /// <param name="docDao"></param>
        /// <param name="keyDao"></param>
        /// <param name="cat"></param>
        /// <param name="srcFile"></param>
        /// <returns></returns>
        private DocDto ImportImg(DocDao docDao, KeyDao keyDao, DocDto cat, string srcFile)
        {
            var name = Path.GetFileName(srcFile);

            var dstFile = DocDto.Combine(cat.FullRelativeFile, name);

            var docDto = new DocImgDto();
            docDto.names = cat.GenDocName(name);
            docDto.path = dstFile;
            docDto.pid = cat.id;
            docDto.Init(_Cfg);

            var key = DocImgDto.GetFileHash(srcFile);
            if (string.IsNullOrEmpty(key))
            {
                if (!docDto.ImportFile(srcFile, false))
                {
                    return null;
                }
            }

            var keyDto = keyDao.Read(key);
            if (keyDto == null)
            {
                keyDto = new KeyDto();
                keyDto.names = name;
                keyDto.key = key;
                keyDto.qty = 1;
                keyDto.file = dstFile;
                keyDao.Save(keyDto);

                if (!docDto.ImportFile(srcFile, false))
                {
                    return null;
                }
            }
            else
            {
                keyDto.qty += 1;
                keyDao.Change(keyDto.id, 1);
            }

            docDto.key = keyDto.id;
            docDao.Save(docDto);

            return docDto;
        }
        #endregion

        static DependencyObject VisualUpwardSearch<T>(DependencyObject source)
        {
            while (source != null && source.GetType() != typeof(T))
                source = VisualTreeHelper.GetParent(source);

            return source;
        }

        #region 剪贴板
        private IDataObject GetDataObjectFromClipboard()
        {
            IDataObject dataObject;

            try
            {
                dataObject = Clipboard.GetDataObject();
            }
            catch (COMException)
            {
                // Clipboard.GetDataObject can be failed by opening the system clipboard 
                // from other or processing clipboard operation like as setting data on clipboard
                dataObject = null;
            }

            return dataObject;
        }

        /// <summary>
        /// 粘贴文件
        /// </summary>
        private void PasteFile(IDataObject obj)
        {
            var files = obj.GetData(DataFormats.FileDrop) as string[];
            if (files == null)
            {
                return;
            }

            var docDao = new DocDao();
            var keyDao = new KeyDao();
            foreach (var file in files)
            {
                if (Directory.Exists(file))
                {
                    ImportCat(_CurrCat, file);
                    continue;
                }

                if (File.Exists(file))
                {
                    var docDto = ImportImg(docDao, keyDao, _CurrCat, file);
                    if (docDto != null)
                    {
                        docDto.Prepare();
                        _Docs.Add(docDto);
                    }
                    continue;
                }
            }
        }

        /// <summary>
        /// 粘贴对象
        /// </summary>
        private void PasteDoc(IDataObject obj)
        {
            try
            {
                var tmp = obj.GetData(Env.ClipDataFormat);
                var doc = tmp as DocDto;
                foreach (var item in _Docs)
                {
                    if (item.key == doc.key)
                    {
                        return;
                    }
                }

                doc = doc.CreateLink(_CurrCat);
                new DocDao().Save(doc);

                _Docs.Add(doc);
            }
            catch (Exception exp)
            {
                ;
            }
        }
        #endregion
    }
}
