using System.IO;
using System.Reflection;

namespace Me.Amon
{
    public class APlugin
    {
        public void LoadPlugin(string path)
        {
            if (!Directory.Exists(path))
            {
                return;
            }

            var files = Directory.GetFiles(path);
            foreach (var file in files)
            {
                Assembly assembly = Assembly.LoadFrom(file);
            }
        }
    }
}
