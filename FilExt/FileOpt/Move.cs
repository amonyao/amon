using Me.Amon.FilExt.Dto;
using Me.Amon.FilExt.Dvo;
using System.IO;

namespace Me.Amon.FilExt.FileOpt
{
    public class Move : AFileOpt
    {
        public override string Code { get { return MethodDto.METHOD_MOVE; } }

        public override string ToString()
        {
            return "移动";
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
                MoveTo(new DirectoryInfo(src), file.DstPath);
                return;
            }

            if (File.Exists(src))
            {
                MoveTo(new FileInfo(src), Path.Combine(file.DstPath, file.DstFile));
                return;
            }
        }

        private void MoveTo(DirectoryInfo src, string dst)
        {
            var files = src.GetFiles();
            foreach (var file in files)
            {
                MoveTo(file, Path.Combine(dst, file.Name));
            }

            var dirs = src.GetDirectories();
            foreach (var dir in dirs)
            {
                MoveTo(dir, Path.Combine(dst, dir.Name));
            }
        }

        private void MoveTo(FileInfo src, string dst)
        {
            if (File.Exists(dst))
            {
                File.Delete(dst);
            }
            src.MoveTo(dst);
        }
    }
}
