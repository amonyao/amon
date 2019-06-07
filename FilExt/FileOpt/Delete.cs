using Me.Amon.FilExt.Dto;
using Me.Amon.FilExt.Dvo;

namespace Me.Amon.FilExt.FileOpt
{
    public class Delete : AFileOpt
    {
        public override string Code { get { return MethodDto.METHOD_DELETE; } }

        public override string ToString()
        {
            return "删除";
        }

        public override void Deal(RuleDto rule, FileDvo file)
        {
        }
    }
}
