using Me.Amon.Dto;
using System.Collections.Generic;

namespace Me.Amon.FilExt.Dto
{
    public class FileDto : ScmDto
    {
        public string srcPath { get; set; }

        private string _srcName;
        /// <summary>
        /// 来源路径
        /// </summary>
        public string srcName
        {
            get { return _srcName; }
            set
            {
                _srcName = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 对应规则
        /// </summary>
        public RuleDto rule { get; set; }
        public List<RuleDto> ruleList { get; set; }

        private string _dstFile;
        public string dstFile
        {
            get { return _dstFile; }
            set
            {
                _dstFile = value;
                OnPropertyChanged();
            }
        }

        private string _dstPath;
        /// <summary>
        /// 目标路径
        /// </summary>
        public string dstPath
        {
            get { return _dstPath; }
            set
            {
                _dstPath = value;
                OnPropertyChanged();
            }
        }

        private int _rate;
        /// <summary>
        /// 处理进度
        /// </summary>
        public int rate
        {
            get { return _rate; }
            set
            {
                _rate = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 处理结果
        /// </summary>
        public bool result { get; set; }

        private string _reason;
        /// <summary>
        /// 异常原因
        /// </summary>
        public string reason
        {
            get { return _reason; }
            set
            {
                _reason = value;
                OnPropertyChanged();
            }
        }
    }
}
