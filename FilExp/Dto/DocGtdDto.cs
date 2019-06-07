namespace Me.Amon.FilExp.Dto
{
    public class DocGtdDto : DocDto
    {
        public DocGtdDto()
        {
            types = TYPE_30_CODE;

            _PathPre = "gtd:/";
            _PathUri = _PathPre + "/";
        }
    }
}
