using System;
using System.Collections.Generic;

namespace Me.Amon.FilExp.Dto
{
    public class DtoMemo
    {
        public List<DtoMemoItem> items { get; set; }
    }

    public class DtoMemoItem
    {
        public DateTime time { get; set; }
        public string memo { get; set; }
    }
}
