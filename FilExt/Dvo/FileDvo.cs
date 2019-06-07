using Me.Amon.Dvo;
using System.Collections.Generic;

namespace Me.Amon.FilExt.Dvo
{
    public class FileDvo : ScmDvo
    {
        /// <summary>
        /// 当前规则
        /// </summary>
        public RuleDto CurrentRule
        {
            get { return _Rule; }
            set
            {
                _Rule = value;
                RuleName = _Rule != null ? _Rule.name : "-";
            }
        }
        private RuleDto _Rule;

        /// <summary>
        /// 候选规则
        /// </summary>
        public List<RuleDto> OptionalList { get; set; }

        public string RuleName
        {
            get { return _RuleName ?? "-"; }
            set
            {
                _RuleName = value;
                OnPropertyChanged();
            }
        }
        private string _RuleName;

        /// <summary>
        /// 来源文件
        /// </summary>
        public string SrcFile
        {
            get { return _SrcFile; }
            set
            {
                _SrcFile = value;
                OnPropertyChanged();
            }
        }
        private string _SrcFile;

        /// <summary>
        /// 来源路径
        /// </summary>
        public string SrcPath { get; set; }

        public string SrcName { get { return _SrcFile; } }

        /// <summary>
        /// 目标路径
        /// </summary>
        public string DstPath
        {
            get { return _DstPath; }
            set
            {
                _DstPath = value;
                OnPropertyChanged();
            }
        }
        private string _DstPath;

        /// <summary>
        /// 目标文件
        /// </summary>
        public string DstFile
        {
            get { return _DstFile; }
            set
            {
                _DstFile = value;
                OnPropertyChanged();
            }
        }
        private string _DstFile;

        public string DstName
        {
            get
            {
                return _DstName;
            }
            set
            {
                _DstName = value;
                OnPropertyChanged();
            }
        }
        private string _DstName;

        private int _Rate;
        /// <summary>
        /// 处理进度
        /// </summary>
        public int Rate
        {
            get { return _Rate; }
            set
            {
                _Rate = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 处理结果
        /// </summary>
        public bool Result { get; set; }

        private string _Reason;
        /// <summary>
        /// 异常原因
        /// </summary>
        public string Reason
        {
            get { return _Reason; }
            set
            {
                _Reason = value;
                OnPropertyChanged();
            }
        }

        public void AppendRule(RuleDto rule)
        {
            if (OptionalList == null)
            {
                OptionalList = new List<RuleDto>();
            }
            OptionalList.Add(rule);
        }
    }
}
