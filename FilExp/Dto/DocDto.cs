using Me.Amon.Dao;
using Me.Amon.Dto;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace Me.Amon.FilExp.Dto
{
    /// <summary>
    /// 文档摘要信息
    /// </summary>
    public class DocDto : ScmDto
    {
        private UserCfg _Cfg;

        #region 全局属性
        /// <summary>
        /// 虚拟协议
        /// </summary>
        protected string _PathPre { get; set; }
        /// <summary>
        /// 虚拟路径
        /// </summary>
        protected string _PathUri { get; set; }
        /// <summary>
        /// 数据目录
        /// </summary>
        protected string _PathDat { get; set; }
        /// <summary>
        /// 
        /// </summary>
        protected string _PathDir { get; set; }
        #endregion
        /// <summary>
        /// 上级id
        /// </summary>
        public long pid { get; set; }

        public long key { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string icon
        {
            get
            {
                var pre = "/Amon.me;component";
                return pre + "/images/folder.png";
                //if (modes == MODE_10_CODE)
                //{
                //    return pre + "/images/folder.png";
                //}

                //var ext = System.IO.Path.GetExtension(file);
                //return pre + $"/images/{ext}.png";
            }
        }

        private string _path;
        /// <summary>
        /// 文件路径
        /// </summary>
        public string path
        {
            get { return _path; }
            set
            {
                _path = ReplaceStart(value, _PathDir, _PathUri);
                FullRelativeFile = ReplaceStart(_path, _PathDir, _PathPre);
                FullPhysicalFile = ReplaceStart(_path, _PathPre, _PathDir);
            }
        }

        /// <summary>
        /// 文件创建日期
        /// </summary>
        public string file_date { get; set; }
        /// <summary>
        /// 文件创建时间
        /// </summary>
        public string file_time { get; set; }
        /// <summary>
        /// 文件类型
        /// </summary>
        public string file_type { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }

        public DocDto Parent { get; set; }
        /// <summary>
        /// 目录列表
        /// </summary>
        private ObservableCollection<DocDto> _CatList;
        public ObservableCollection<DocDto> CatList
        {
            get
            {
                if (_CatList == null)
                {
                    _CatList = new ObservableCollection<DocDto>();

                    var docDao = new DocImgDao();
                    var list = docDao.ListDir(this);
                    foreach (var doc in list)
                    {
                        doc.Init(_Cfg);
                        doc.Parent = this;
                        _CatList.Add(doc);
                    }
                }
                return _CatList;
            }
        }
        private Dictionary<string, int> CatDict { get; set; }
        /// <summary>
        /// 文件列表
        /// </summary>
        public ObservableCollection<DocDto> DocList { get; set; }
        private Dictionary<string, int> DocDict { get; set; }

        /// <summary>
        /// 子节点是否读取过
        /// </summary>
        public bool Loaded { get; set; }

        public DocDto()
        {
            modes = MODE_20_CODE;
        }

        public void Init(UserCfg cfg)
        {
            _Cfg = cfg;
            var tmp = cfg.RootDir.Replace("\\", "/");
            _PathDir = Combine(tmp, _PathDat);
            FullRelativeFile = ReplaceStart(_path, _PathDir, _PathPre);
            FullPhysicalFile = ReplaceStart(_path, _PathPre, _PathDir);
        }

        public virtual void Prepare()
        {

        }

        /// <summary>
        /// 相对路径
        /// </summary>
        public string FullRelativeFile { get; protected set; }

        /// <summary>
        /// 物理路径
        /// </summary>
        public string FullPhysicalFile { get; protected set; }

        /// <summary>
        /// 界面显示专用
        /// </summary>
        private ImageSource _ImageSource;
        public ImageSource ImageSource
        {
            get { return _ImageSource; }
            set
            {
                _ImageSource = value;
                OnPropertyChanged();
            }
        }

        #region 公共方法
        /// <summary>
        /// 路径合并
        /// </summary>
        /// <param name="path"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string Combine(string path, string file)
        {
            //return System.IO.Path.Combine(path ?? "", file ?? "");
            return (path ?? "") + "/" + file;
        }

        /// <summary>
        /// 替换首字符串
        /// </summary>
        /// <param name="text"></param>
        /// <param name="src"></param>
        /// <param name="dst"></param>
        /// <returns></returns>
        protected string ReplaceStart(string text, string src, string dst)
        {
            var err = string.IsNullOrEmpty(text) || string.IsNullOrEmpty(src) || string.IsNullOrEmpty(dst);
            if (!err)
            {
                text = text.Replace("\\", "/");
                if (text.IndexOf(src) == 0)
                {
                    text = dst + text.Substring(src.Length);
                }
            }
            return text;
        }
        #endregion

        /// <summary>
        /// 创建链接
        /// </summary>
        /// <returns></returns>
        public LnkDto CreateLink(DocDto cat)
        {
            var lnk = new LnkDto();
            lnk.SetType(types);
            lnk.file_type = file_type;
            lnk.file_date = file_date;
            lnk.file_time = file_time;
            lnk.key = key;
            lnk.names = names;
            lnk.pid = cat.id;
            lnk.path = cat.path;
            return lnk;
        }

        public void AppendDocItem(DocDto docDto)
        {
            if (DocList == null)
            {
                DocList = new ObservableCollection<DocDto>();
            }
            docDto.Parent = this;
            DocList.Add(docDto);
        }

        public void AppendCatItem(DocDto docDto)
        {
            if (_CatList == null)
            {
                _CatList = new ObservableCollection<DocDto>();
            }
            docDto.Parent = this;
            CatList.Add(docDto);
        }

        public void BuildCatKeys()
        {
            CatDict = BuildKeys(CatList);
        }

        /// <summary>
        /// 生成子目录名称
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string GenCatName(string path)
        {
            return GenCatName(CatDict, path);
        }

        public bool CatNameExist(string name)
        {
            if (CatDict != null)
            {
                name = name.ToUpper();
                return CatDict.ContainsKey(name);
            }
            return false;
        }

        /// <summary>
        /// 生成子文件名称
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string GenDocName(string path)
        {
            return GenDocName(DocDict, path);
        }

        public bool DocNameExist(string name)
        {
            if (DocDict != null)
            {
                name = name.ToUpper();
                return DocDict.ContainsKey(name);
            }
            return false;
        }

        public void BuildDocKeys()
        {
            DocDict = BuildKeys(DocList);
        }

        private Dictionary<string, int> BuildKeys(IEnumerable<DocDto> docs)
        {
            var dict = new Dictionary<string, int>();
            if (docs != null)
            {
                foreach (var doc in docs)
                {
                    dict[doc.names.ToUpper()] = 1;
                }
            }
            return dict;
        }

        private string GenCatName(Dictionary<string, int> dict, string path)
        {
            var tmp = System.IO.Path.GetFileName(path);

            if (dict != null)
            {
                if (dict.ContainsKey(tmp.ToUpper()))
                {
                    var name = System.IO.Path.GetFileNameWithoutExtension(path);
                    var upper = name.ToUpper();

                    var idx = 0;
                    while (true)
                    {
                        idx += 1;
                        if (!dict.ContainsKey(upper + $" ({idx})"))
                        {
                            break;
                        }
                    }

                    tmp = name + $" ({idx})";
                }
            }

            return tmp;
        }

        private string GenDocName(Dictionary<string, int> dict, string path)
        {
            var tmp = System.IO.Path.GetFileName(path);

            if (dict != null)
            {
                if (dict.ContainsKey(tmp.ToUpper()))
                {
                    var name = System.IO.Path.GetFileNameWithoutExtension(path);
                    var exts = System.IO.Path.GetExtension(path).ToUpper();
                    var upper = name.ToUpper();

                    var idx = 0;
                    while (true)
                    {
                        idx += 1;
                        if (!dict.ContainsKey(upper + $" ({idx})" + exts))
                        {
                            break;
                        }
                    }

                    tmp = name + $" ({idx})" + exts;
                }
            }

            return tmp;
        }
    }
}
