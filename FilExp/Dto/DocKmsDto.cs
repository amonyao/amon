namespace Me.Amon.FilExp.Dto
{
    public class DocKmsDto : DocDto
    {
        public DocKmsDto()
        {
            types = TYPE_10_CODE;

            _PathPre = "kms:/";
            _PathUri = _PathPre + "/";
            _PathDir = Combine(_PathDir, "kms");
        }
    }
}
