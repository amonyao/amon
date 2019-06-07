using System;

namespace Me.Amon
{
    public class Env
    {
        public const string FORMAT_DATE = "yyyy-MM-dd";
        public const string FORMAT_TIME = "HH:mm:ss";
        public const string FORMAT_DATE_TIME = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// 启动目录
        /// </summary>
        public static string StartUpDir = AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// 剪贴板格式
        /// </summary>
        public static string ClipDataFormat = "FilExp_Doc";
    }
}
