using Me.Amon.FilExp.Dto;
using System.Collections.Generic;

namespace Me.Amon.Dao
{
    public class DocImgDao : DocDao
    {
        public IEnumerable<DocImgDto> ListDir(DocDto dto, Orderby orderby = null)
        {
            if (orderby == null)
            {
                orderby = new Orderby();
            }
            var sql = @"SELECT a.[id], a.[pid], a.[key], a.[types], a.[modes], a.[names], a.[path], a.[remark], a.[update_time], a.[create_time] FROM[doc] a WHERE a.[pid] = @id AND a.[types] = @types";
            sql += " AND a.[modes] = " + DocDto.MODE_10_CODE;
            sql += orderby.GenerateOrderBy("a.");
            return Query<DocImgDto>(sql, dto);
        }

        public IEnumerable<DocImgDto> ListDoc(DocDto dto, Orderby orderby = null)
        {
            if (orderby == null)
            {
                orderby = new Orderby();
            }
            var sql = @"SELECT a.[id], a.[pid], a.[key], a.[types], a.[modes], a.[names], a.[path], a.[remark], a.[update_time], a.[create_time] FROM[doc] a WHERE a.[pid] = @id AND a.[types] = @types";
            sql += " AND a.[modes] = " + DocDto.MODE_20_CODE;
            sql += orderby.GenerateOrderBy("a.");
            return Query<DocImgDto>(sql, dto);
        }
    }
}
