using Me.Amon.FilExt.Dto;
using Me.Amon.FilExt.Dvo;
using System.Text.RegularExpressions;

namespace Me.Amon.FilExt
{
    public class AFileOpt
    {
        public virtual string Code { get { return MethodDto.METHOD_NONE; } }

        public static AFileOpt GetInstance(string method)
        {
            switch (method)
            {
                case MethodDto.METHOD_MOVE:
                    return new FileOpt.Move();
                case MethodDto.METHOD_COPY:
                    return new FileOpt.Copy();
                case MethodDto.METHOD_RENAME:
                    return new FileOpt.Rename();
                case MethodDto.METHOD_DELETE:
                    return new FileOpt.Delete();
                default:
                    return new FileOpt.Default();
            }
        }

        /// <summary>
        /// 通配符查询
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        private static bool IsMatch(string input, string pattern)
        {
            pattern = "^" + Regex.Escape(pattern).Replace("\\*", ".*").Replace("\\?", ".") + "$";
            return input != null ? Regex.IsMatch(input, pattern) : false;
        }

        private string GenFileName(string input, string pattern)
        {
            return input;
        }

        public virtual bool Test(RuleDto rule, FileDvo file)
        {
            var fileMatch = !string.IsNullOrWhiteSpace(rule.src_file) ? IsMatch(file.SrcFile, rule.src_file) : true;
            var pathMatch = !string.IsNullOrWhiteSpace(rule.src_path) ? IsMatch(file.SrcPath, rule.src_path) : true;

            if (fileMatch && pathMatch)
            {
                file.DstPath = rule.dst_path;
                file.DstFile = file.SrcFile;
                file.DstName = System.IO.Path.Combine(file.DstPath, file.DstFile);
                return true;
            }

            return false;
        }

        public virtual void Deal(RuleDto rule, FileDvo file)
        {

        }
    }
}
