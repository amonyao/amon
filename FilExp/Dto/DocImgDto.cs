using System.IO;
using System.Windows.Media.Imaging;

namespace Me.Amon.FilExp.Dto
{
    public class DocImgDto : DocDto
    {
        public DocImgDto()
        {
            _PathDat = "img";
            _PathPre = "img:/";
            _PathUri = _PathPre + "/";

            types = TYPE_60_CODE;
            modes = MODE_20_CODE;
        }

        public override void Prepare()
        {
            var path = FullPhysicalFile;
            if (File.Exists(path))
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new System.Uri(path);
                image.DecodePixelWidth = 200;
                image.EndInit();
                image.Freeze();

                ImageSource = image;
            }
        }

        public string GetFileHash()
        {
            return GetFileHash(FullPhysicalFile);
        }

        public static string GetFileHash(string file)
        {
            var hash = "0";
            if (File.Exists(file))
            {
                var alg = System.Security.Cryptography.HashAlgorithm.Create("MD5");
                using (var stream = File.OpenRead(file))
                {
                    var bytes = alg.ComputeHash(stream);
                    hash = CharUtil.ToHexString(bytes);
                }
            }

            return hash;
        }

        public bool ImportFile(string srcFile, bool removeSrc)
        {
            if (!File.Exists(FullPhysicalFile))
            {
                File.Copy(srcFile, FullPhysicalFile);
            }

            if (removeSrc)
            {
                File.Delete(srcFile);
            }

            return true;
        }
    }
}
