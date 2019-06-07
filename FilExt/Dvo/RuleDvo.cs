using Me.Amon.Dao;

namespace Me.Amon.FilExt.Dvo
{
    public class RuleDvo : RuleDto
    {
        public override string name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public string method_name { get { return new DictDao().GetDict(MethodDvo.CAT_NAME, method).text; } }

        public string repeat_name { get { return new DictDao().GetDict(RepeatDvo.CAT_NAME, repeat).text; } }
    }
}
