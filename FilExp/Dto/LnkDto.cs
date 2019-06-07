namespace Me.Amon.FilExp.Dto
{
    public class LnkDto : DocDto
    {
        public LnkDto()
        {
            modes = MODE_30_CODE;
        }

        public void SetType(int type)
        {
            this.types = type;
        }
    }
}
