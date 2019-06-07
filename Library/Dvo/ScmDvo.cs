using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Me.Amon.Dvo
{
    public class ScmDvo : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
