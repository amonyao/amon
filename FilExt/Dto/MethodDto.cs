using Me.Amon.Dto;

namespace Me.Amon.FilExt.Dto
{
    public class MethodDto : DictDto
    {
        public const string CAT_NAME = "fms_method";

        /// <summary>
        /// 不处理
        /// </summary>
        public const string METHOD_NONE = "0";
        /// <summary>
        /// 移动
        /// </summary>
        public const string METHOD_MOVE = "1";
        /// <summary>
        /// 复制
        /// </summary>
        public const string METHOD_COPY = "2";
        /// <summary>
        /// 更名
        /// </summary>
        public const string METHOD_RENAME = "3";
        /// <summary>
        /// 删除
        /// </summary>
        public const string METHOD_DELETE = "4";

        public MethodDto()
        {
            cat = "fms_method";
        }
    }
}
