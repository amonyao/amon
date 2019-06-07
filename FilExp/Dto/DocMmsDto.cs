namespace Me.Amon.FilExp.Dto
{
    public class DocMmsDto : DocDto
    {
        public DocMmsDto()
        {
            types = TYPE_20_CODE;

            _PathDat = "mms";
            _PathPre = _PathDat + ":/";
            _PathUri = _PathPre + "/";
        }
    }
}
