using Me.Amon.FilExt.Dto;
using Me.Amon.FilExt.Dvo;
using System.IO;

namespace Me.Amon.FilExt.FileOpt
{
    public class Copy : AFileOpt
    {
        public override string Code { get { return MethodDto.METHOD_COPY; } }

        public override string ToString()
        {
            return "复制";
        }

        public override void Deal(RuleDto rule, FileDvo file)
        {
            var src = System.IO.Path.Combine(file.SrcPath, file.SrcFile);

            if (!Directory.Exists(file.DstPath))
            {
                Directory.CreateDirectory(file.DstPath);
            }

            if (Directory.Exists(src))
            {
                CopyTo(new DirectoryInfo(src), file.DstPath);
                return;
            }

            if (File.Exists(src))
            {
                CopyTo(new FileInfo(src), Path.Combine(file.DstPath, file.DstFile));
                return;
            }
        }

        private void CopyTo(DirectoryInfo src, string dst)
        {
            var files = src.GetFiles();
            foreach (var file in files)
            {
                CopyTo(file, Path.Combine(dst, file.Name));
            }

            var dirs = src.GetDirectories();
            foreach (var dir in dirs)
            {
                CopyTo(dir, Path.Combine(dst, dir.Name));
            }
        }

        private void CopyTo(FileInfo src, string dst)
        {
            src.CopyTo(dst, true);
        }
    }
}
