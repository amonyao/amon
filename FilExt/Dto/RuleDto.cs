using Me.Amon.Dto;

namespace Me.Amon.FilExt
{
    public class RuleDto : ScmDto
    {
        /// <summary>
        /// 规则名称
        /// </summary>
        public virtual string name { get { return _name; } set { _name = value; } }
        protected string _name;

        /// <summary>
        /// 操作
        /// </summary>
        public virtual string method { get { return _method; } set { _method = value; } }
        private string _method;

        /// <summary>
        /// 来源规则
        /// </summary>
        public string src_file { get; set; }
        public string src_path { get; set; }

        /// <summary>
        /// 目标规则
        /// </summary>
        public string dst_path { get; set; }
        public string dst_file { get; set; }

        /// <summary>
        /// 重复方案
        /// </summary>
        public string repeat { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }

        public bool IsMatch(string file)
        {
            return true;
        }
    }
}
