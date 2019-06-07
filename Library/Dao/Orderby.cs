namespace Me.Amon.Dao
{
    public class Orderby
    {
        public const int TYPE_1_CODE = 1;
        public const string TYPE_1_NAME = "按更新时间";

        public const int TYPE_2_CODE = 2;
        public const string TYPE_2_NAME = "按创建时间";

        public const int TYPE_3_CODE = 3;
        public const string TYPE_3_NAME = "按标题";

        public const int MODE_1_CODE = 1;
        public const string MODE_1_NAME = "降序";

        public const int MODE_2_CODE = 2;
        public const string MODE_2_NAME = "升序";

        /// <summary>
        /// 排序方式
        /// </summary>
        public int type;
        /// <summary>
        /// 排序模式
        /// </summary>
        public int mode;

        public Orderby()
        {
            type = TYPE_1_CODE;
            mode = TYPE_1_CODE;
        }

        public string GenerateOrderBy(string pre = "")
        {
            var sql = " ORDER BY " + pre;
            switch (type)
            {
                case TYPE_1_CODE:
                    sql += " update_time";
                    break;
                case TYPE_2_CODE:
                    sql += " create_time";
                    break;
                case TYPE_3_CODE:
                    sql += " title";
                    break;
                default:
                    sql += " id";
                    break;
            }

            switch (mode)
            {
                case MODE_1_CODE:
                    sql += " desc";
                    break;
                case MODE_2_CODE:
                    sql += " asc";
                    break;
            }

            return sql;
        }
    }
}
