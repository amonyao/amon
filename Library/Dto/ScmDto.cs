using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Me.Amon.Dto
{
    public abstract class ScmDto : INotifyPropertyChanged
    {
        #region 类型
        /// <summary>
        /// 记事
        /// </summary>
        public const int TYPE_10_CODE = 10;
        public const string TYPE_10_NAME = "记事";

        /// <summary>
        /// 脑图
        /// </summary>
        public const int TYPE_20_CODE = 20;
        public const string TYPE_20_NAME = "脑图";

        /// <summary>
        /// 待办
        /// </summary>
        public const int TYPE_30_CODE = 30;
        public const string TYPE_30_NAME = "待办";

        /// <summary>
        /// 图像
        /// </summary>
        public const int TYPE_60_CODE = 60;
        public const string TYPE_60_NAME = "图像";

        /// <summary>
        /// 文件
        /// </summary>
        public const int TYPE_90_CODE = 90;
        public const string TYPE_90_NAME = "文件";
        #endregion

        #region 模式
        /// <summary>
        /// 目录
        /// </summary>
        public const int MODE_10_CODE = 10;
        public const string MODE_10_NAME = "目录";

        /// <summary>
        /// 文件
        /// </summary>
        public const int MODE_20_CODE = 20;
        public const string MODE_20_NAME = "文件";

        /// <summary>
        /// 链接
        /// </summary>
        public const int MODE_30_CODE = 30;
        public const string MODE_30_NAME = "链接";
        #endregion

        #region
        /// <summary>
        /// 未知
        /// </summary>
        public const int STATUS_0 = 0;
        /// <summary>
        /// 正常
        /// </summary>
        public const int STATUS_1 = 1;
        /// <summary>
        /// 禁用
        /// </summary>
        public const int STATUS_2 = 2;
        /// <summary>
        /// 删除
        /// </summary>
        public const int STATUS_3 = 3;
        #endregion

        /// <summary>
        /// 主键
        /// </summary>
        public long id { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public int types { get; protected set; }
        /// <summary>
        /// 模式
        /// </summary>
        public int modes { get; protected set; }

        /// <summary>
        /// 名称
        /// </summary>
        private string _names;
        /// <summary>
        /// 文档标题
        /// </summary>
        public virtual string names
        {
            get { return _names; }
            set
            {
                _names = value;
                OnPropertyChanged();
            }
        }

        public int status { get; set; }

        public DateTime update_time { get; set; }
        public DateTime create_time { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
