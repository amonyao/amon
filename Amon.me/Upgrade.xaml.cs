using System.IO;
using System.Windows;

namespace Me.Amon
{
    /// <summary>
    /// Upgrade.xaml 的交互逻辑
    /// </summary>
    public partial class Upgrade : Window
    {
        /// <summary>
        /// 数据库更新
        /// </summary>
        public const string UPGRADE_SQL = "Setup\\upgrade.sql";
        /// <summary>
        /// 更新日志
        /// </summary>
        public const string UPGRADE_LOG = "Setup\\upgrade.log";

        public Upgrade()
        {
            InitializeComponent();
        }

        public void Init()
        {
            var file = UPGRADE_LOG;
            var text = "";
            using (var stream = File.OpenRead(file))
            {
                using (var reader = new StreamReader(stream))
                {
                    text = reader.ReadToEnd();
                }
            }
            TbInfo.Text = text;
        }

        private void BtAccept_Click(object sender, RoutedEventArgs e)
        {
            var file = UPGRADE_LOG;
            try
            {
                File.Delete(file);
            }
            catch
            {

            }

            Close();
        }
    }
}
