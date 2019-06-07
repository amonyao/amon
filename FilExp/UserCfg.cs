using System.Collections.Generic;

namespace Me.Amon.FilExp.Dto
{
    public class UserCfg
    {
        /// <summary>
        /// 数据根目录
        /// </summary>
        private string _RootDir;
        public string RootDir
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_RootDir))
                {
                    //_RootDir = ".dat\\";
                    _RootDir = "D:\\Temp";
                }
                return _RootDir;
            }
            set
            {
                _RootDir = value;
            }
        }

        public void Load()
        {
        }

        /// <summary>
        /// 包含的文件
        /// </summary>
        public List<Files> Include { get; set; }
        /// <summary>
        /// 排除的文件
        /// </summary>
        public List<Files> Exclude { get; set; }
    }

    public class Files
    {
        public string name { get; set; }
        public List<string> exts { get; set; }
    }
}
