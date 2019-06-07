using Me.Amon.Dto;

namespace Me.Amon.FilExp.Dto
{
    public class DocTagDto : ScmDto
    {
        public long doc_id { get; set; }
        public long tag_id { get; set; }
        public TagDto tag { get; set; }
    }
}
