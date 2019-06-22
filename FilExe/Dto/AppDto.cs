using Me.Amon.Dto;

namespace Me.Amon.FilExe.Dto
{
    public class AppDto : ScmDto
    {
        public int od { get; set; }

        public string os { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        public string text { get; set; }

        /// <summary>
        /// 提示信息
        /// </summary>
        public string tips { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string path { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string file { get; set; }

        /// <summary>
        /// 快捷命令
        /// </summary>
        public string keys { get; set; }
    }
}
