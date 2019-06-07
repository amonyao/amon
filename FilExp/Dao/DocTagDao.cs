using Me.Amon.FilExp.Dto;
using System;
using System.Collections.Generic;

namespace Me.Amon.Dao
{
    public class DocTagDao : ScmDba<DocTagDto>
    {
        public override IEnumerable<K> List<K>(DocTagDto dto, Orderby orderby = null)
        {
            if (orderby == null)
            {
                orderby = new Orderby();
            }
            var sql = $"select a.[id],a.[tag_id],a.[doc_id],a.[update_time],a.[create_time] from [tag_doc] a where a.[doc_id] = {dto.id}";
            //sql += orderby.GenerateOrderBy("a.");
            return Query<K>(sql, dto);
        }

        public override K Read<K>(long id)
        {
            var sql = $"select a.[id],a.[tag_id],a.[doc_id],a.[update_time],a.[create_time] from [tag_doc] a where a.[id] = {id}";
            return QueryFirst<K>(sql);
        }

        public override void Save(DocTagDto dto)
        {
            dto.update_time = DateTime.Now;
            var sql = "";
            if (!IsValidId(dto.id))
            {
                dto.create_time = dto.update_time;
                sql = "INSERT INTO [tag_doc] ([tag_id],[doc_id],[update_time],[create_time]) VALUES (@tag_id,@doc_id,@update_time,@create_time)";
                ExecuteInsert(sql, dto);
            }
        }

        public override void Delete(long id)
        {
            ExecuteDelete($"DELETE FROM [tag_doc] WHERE [id]={id}");
        }

        public void Delete(long docId, long tagId)
        {
            ExecuteDelete($"DELETE FROM [tag_doc] WHERE [doc_id] = {docId} and [tag_id] = {tagId}");
        }

        public DocTagDto Read(long docId, long tagId)
        {
            var sql = $"select a.[id],a.[tag_id],a.[doc_id],a.[update_time],a.[create_time] from [tag_doc] a where a.[doc_id] = {docId} and a.[tag_id] = {tagId}";
            return QueryFirst<DocTagDto>(sql);
        }
    }
}
