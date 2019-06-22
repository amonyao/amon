using Me.Amon.FilExe.Dto;
using System.Collections.Generic;
using System.Windows.Documents;

namespace Me.Amon.FilExe.Dvo
{
    public class AppDvo : AppDto
    {
        public AppDvo()
        {
            status = IAppEnv.STATUS_1;
        }

        private List<Inline> rftName;
        public List<Inline> InLineName
        {
            get { return rftName; }
            set
            {
                rftName = value;
                OnPropertyChanged();
            }
        }

        private string _Tips;
        public string Tips
        {
            get { return _Tips; }
            set
            {
                _Tips = value;
                OnPropertyChanged();
            }
        }

        public bool Enabled
        {
            get
            {
                return status == IAppEnv.STATUS_1;
            }
            set
            {
                status = value ? IAppEnv.STATUS_1 : IAppEnv.STATUS_2;
            }
        }

        public string[] kkkk { get; set; }

        public void Decode()
        {
            if (keys != null)
            {
                kkkk = keys.Split(';');
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="list1">完全匹配列表</param>
        /// <param name="list2">模糊匹配列表</param>
        /// <returns></returns>
        public bool IsMatch(string pattern, List<AppDvo> list1, List<AppDvo> list2)
        {
            var tmp = text.ToLower();
            if (tmp.IndexOf(pattern) >= 0)
            {
                list1.Add(this);
                return true;
            }
            if (IsMatch(text, pattern))
            {
                list2.Add(this);
                return true;
            }

            if (kkkk != null)
            {
                foreach (var key in kkkk)
                {
                    if (key.IndexOf(pattern) >= 0)
                    {
                        list1.Add(this);
                        return true;
                    }
                    if (IsMatch(key, pattern))
                    {
                        list2.Add(this);
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// 按关键时顺序匹配
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        private static bool IsMatch(string input, string pattern)
        {
            if (string.IsNullOrWhiteSpace(input) || string.IsNullOrWhiteSpace(pattern))
            {
                return false;
            }

            var srcIdx = 0;
            var srcQty = input.Length;
            var dstIdx = 0;
            var dstQty = pattern.Length;

            while (true)
            {
                if (pattern[dstIdx] == input[srcIdx])
                {
                    dstIdx += 1;
                }
                srcIdx += 1;

                if (dstIdx >= dstQty)
                {
                    return true;
                }

                if (srcIdx >= srcQty)
                {
                    return false;
                }
            }
        }
    }
}
