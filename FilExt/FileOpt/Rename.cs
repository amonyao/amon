using Me.Amon.FilExt.Dto;
using Me.Amon.FilExt.Dvo;

namespace Me.Amon.FilExt.FileOpt
{
    public class Rename : AFileOpt
    {
        public override string Code { get { return MethodDto.METHOD_RENAME; } }

        public override string ToString()
        {
            return "更名";
        }

        public override void Deal(RuleDto rule, FileDvo file)
        {
        }
    }
}
