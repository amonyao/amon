using System;
using System.IO;

namespace Me.Amon.FilExp.Uc
{
    public class SysCfg
    {
        private const string CFG_FILE = "FilExp.cfg";

        private static SysCfg _Instance;

        private static string ROOT_DIR = AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// 数据根目录
        /// </summary>
        private string _RootDir;
        /// <summary>
        /// 数据文件目录
        /// </summary>
        private string DataPath = "data";
        /// <summary>
        /// 临时文件目录
        /// </summary>
        private string TempPath = "temp";

        public string RootDir
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_RootDir))
                {
                    _RootDir = ROOT_DIR;
                }
                return _RootDir;
            }
            set
            {
                _RootDir = value;
            }
        }

        public static bool Load()
        {
            var file = CFG_FILE;
            if (File.Exists(file))
            {
                var text = File.ReadAllText(file);
                _Instance = text.AsJsonObject<SysCfg>();
            }
            else
            {
                _Instance = new SysCfg();
                _Instance.LoadDef();
            }

            return true;
        }

        private void LoadDef()
        {
            _RootDir = @"D:\Temp";
        }

        public bool Save()
        {
            var file = CFG_FILE;
            var text = _Instance.ToJsonString();
            File.WriteAllText(file, text);

            return true;
        }
    }
}
