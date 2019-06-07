using Me.Amon.FilExt.Dto;
using Me.Amon.FilExt.Dvo;

namespace Me.Amon.FilExt.FileOpt
{
    public class Default : AFileOpt
    {
        public override string Code { get { return MethodDto.METHOD_NONE; } }

        public override string ToString()
        {
            return "";
        }

        public override void Deal(RuleDto rule, FileDvo file)
        {
        }
    }
}
