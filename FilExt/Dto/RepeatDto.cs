using Me.Amon.Dto;

namespace Me.Amon.FilExt.Dto
{
    public class RepeatDto : DictDto
    {
        public const string CAT_NAME = "fms_repeat";

        /// <summary>
        /// 不处理
        /// </summary>
        public const string KEY_1 = "1";
        /// <summary>
        /// 覆盖
        /// </summary>
        public const string KEY_2 = "2";
        /// <summary>
        /// 追加
        /// </summary>
        public const string KEY_3 = "3";
        /// <summary>
        /// 去重
        /// </summary>
        public const string KEY_4 = "4";

        public RepeatDto()
        {
            cat = "fms_repeat";
        }
    }
}
