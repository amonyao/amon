using Me.Amon.FilExe.Dto;
using System.Collections.Generic;
using System.Windows.Documents;

namespace Me.Amon.FilExe.Dvo
{
    public class AppDvo : AppDto
    {
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

        public string[] kkkk { get; set; }

        public void Decode()
        {
            if (keys != null)
            {
                kkkk = keys.Split(';');
            }
        }
    }
}
