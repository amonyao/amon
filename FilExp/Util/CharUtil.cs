using System.Text;

namespace Me.Amon
{
    public class CharUtil
    {
        public static string ToHexString(byte[] bytes)
        {
            if (bytes == null)
            {
                return "";
            }

            var builder = new StringBuilder();
            foreach (var b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
