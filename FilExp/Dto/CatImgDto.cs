namespace Me.Amon.FilExp.Dto
{
    public class CatImgDto : CatDto
    {
        public CatImgDto()
        {
            _PathDat = "img";
            _PathPre = "img:/";
            _PathUri = _PathPre + "/";

            types = TYPE_60_CODE;
            pid = TYPE_60_CODE;
        }
    }
}
