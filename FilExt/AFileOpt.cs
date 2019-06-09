using Me.Amon.FilExt.Dto;
using Me.Amon.FilExt.Dvo;
using System.IO;
using System.Text;
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

        public virtual bool Test(RuleDto rule, FileDvo file)
        {
            var fileMatch = !string.IsNullOrWhiteSpace(rule.src_file) ? IsMatch(file.SrcFile, rule.src_file, true) : true;
            var srcFile = Path.Combine(file.SrcPath, file.SrcFile);
            var pathMatch = !string.IsNullOrWhiteSpace(rule.src_path) ? IsMatch(file.SrcPath + Path.DirectorySeparatorChar, rule.src_path, false) : true;

            if (fileMatch && pathMatch)
            {
                file.DstPath = rule.dst_path;
                file.DstFile = GenFileName(srcFile, rule.dst_file);
                file.DstName = System.IO.Path.Combine(file.DstPath, file.DstFile);
                return true;
            }

            return false;
        }

        public virtual void Deal(RuleDto rule, FileDvo file)
        {

        }

        /// <summary>
        /// 通配符查询
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        private static bool IsMatch(string input, string pattern, bool allword)
        {
            if (string.IsNullOrWhiteSpace(pattern))
            {
                return false;
            }

            var builder = new StringBuilder();
            if (allword)
            {
                builder.Append("^");
            }
            foreach (var item in pattern.Split(';'))
            {
                builder.Append("(");
                builder.Append(Regex.Escape(item).Replace("\\*", ".*").Replace("\\?", "."));
                builder.Append(")|");
            }
            if (builder.Length > 1)
            {
                builder.Remove(builder.Length - 1, 1);
            }
            if (allword)
            {
                builder.Append("$");
            }

            return input != null ? Regex.IsMatch(input, builder.ToString(), RegexOptions.IgnoreCase) : false;
        }

        /// <summary>
        /// 根据模式生成文件名
        /// </summary>
        /// <param name="srcFile">来源文件路径（全）</param>
        /// <param name="dstName">卡件文件名称</param>
        /// <returns></returns>
        private string GenFileName(string srcFile, string dstName)
        {
            if (string.IsNullOrWhiteSpace(dstName))
            {
                return srcFile;
            }

            var fileInfo = new FileInfo(srcFile);
            var fileName = Path.GetFileNameWithoutExtension(srcFile);
            var fileExt = Path.GetExtension(srcFile);

            foreach (Match match in Regex.Matches(dstName, "<[^>]+>"))
            {
                if (!match.Success)
                {
                    continue;
                }

                var src = match.Value;
                switch (src.ToLower())
                {
                    case "<filename>":
                        dstName = dstName.Replace(src, fileName);
                        break;
                    case "<fileext>":
                        dstName = dstName.Replace(src, fileExt);
                        break;
                    case "<createdate>":
                        dstName = dstName.Replace(src, fileInfo.CreationTime.ToString("yyyyMMdd"));
                        break;
                    case "<createtime>":
                        dstName = dstName.Replace(src, fileInfo.CreationTime.ToString("HHmmss"));
                        break;
                    case "<createdatetime>":
                        dstName = dstName.Replace(src, fileInfo.CreationTime.ToString("yyyyMMddHHmmss"));
                        break;
                    case "<createyear>":
                        dstName = dstName.Replace(src, fileInfo.CreationTime.Year.ToString());
                        break;
                    case "<createmonth>":
                        dstName = dstName.Replace(src, fileInfo.CreationTime.Month.ToString());
                        break;
                    case "<createday>":
                        dstName = dstName.Replace(src, fileInfo.CreationTime.Day.ToString());
                        break;
                    case "<createhour>":
                        dstName = dstName.Replace(src, fileInfo.CreationTime.Hour.ToString());
                        break;
                    case "<createminute>":
                        dstName = dstName.Replace(src, fileInfo.CreationTime.Minute.ToString());
                        break;
                    case "<createsecond>":
                        dstName = dstName.Replace(src, fileInfo.CreationTime.Second.ToString());
                        break;
                    case "<updatedate>":
                        dstName = dstName.Replace(src, fileInfo.LastWriteTime.ToString("yyyyMMdd"));
                        break;
                    case "<updatetime>":
                        dstName = dstName.Replace(src, fileInfo.LastWriteTime.ToString("HHmmss"));
                        break;
                    case "<updatedatetime>":
                        dstName = dstName.Replace(src, fileInfo.LastWriteTime.ToString("yyyyMMddHHmmss"));
                        break;
                    case "<updateyear>":
                        dstName = dstName.Replace(src, fileInfo.LastWriteTime.Year.ToString());
                        break;
                    case "<updatemonth>":
                        dstName = dstName.Replace(src, fileInfo.LastWriteTime.Month.ToString());
                        break;
                    case "<updateday>":
                        dstName = dstName.Replace(src, fileInfo.LastWriteTime.Day.ToString());
                        break;
                    case "<updatehour>":
                        dstName = dstName.Replace(src, fileInfo.LastWriteTime.Hour.ToString());
                        break;
                    case "<updateminute>":
                        dstName = dstName.Replace(src, fileInfo.LastWriteTime.Minute.ToString());
                        break;
                    case "<updatesecond>":
                        dstName = dstName.Replace(src, fileInfo.LastWriteTime.Second.ToString());
                        break;
                    case "<accessdate>":
                        dstName = dstName.Replace(src, fileInfo.LastAccessTime.ToString("yyyyMMdd"));
                        break;
                    case "<accesstime>":
                        dstName = dstName.Replace(src, fileInfo.LastAccessTime.ToString("HHmmss"));
                        break;
                    case "<accessdatetime>":
                        dstName = dstName.Replace(src, fileInfo.LastAccessTime.ToString("yyyyMMddHHmmss"));
                        break;
                    case "<accessyear>":
                        dstName = dstName.Replace(src, fileInfo.LastAccessTime.Year.ToString());
                        break;
                    case "<accessmonth>":
                        dstName = dstName.Replace(src, fileInfo.LastAccessTime.Month.ToString());
                        break;
                    case "<accessday>":
                        dstName = dstName.Replace(src, fileInfo.LastAccessTime.Day.ToString());
                        break;
                    case "<accesshour>":
                        dstName = dstName.Replace(src, fileInfo.LastAccessTime.Hour.ToString());
                        break;
                    case "<accessminute>":
                        dstName = dstName.Replace(src, fileInfo.LastAccessTime.Minute.ToString());
                        break;
                    case "<accesssecond>":
                        dstName = dstName.Replace(src, fileInfo.LastAccessTime.Second.ToString());
                        break;
                    default:
                        break;
                }
            }

            return dstName;
        }
    }
}
