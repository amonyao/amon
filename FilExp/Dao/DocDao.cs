using Me.Amon.FilExp.Dto;
using System;
using System.Collections.Generic;

namespace Me.Amon.Dao
{
    public class DocDao : ScmDba<DocDto>
    {
        public override IEnumerable<K> List<K>(DocDto dto, Orderby orderby = null)
        {
            if (orderby == null)
            {
                orderby = new Orderby();
            }
            var sql = @"SELECT a.[id], a.[pid], a.[key], a.[types], a.[modes], a.[names], a.[path], a.[remark], a.[file_type], a.[file_date], a.[file_time], a.[update_time], a.[create_time] FROM[doc] a WHERE a.[pid] = @id AND a.[types] = @types";
            sql += orderby.GenerateOrderBy("a.");
            return Query<K>(sql, dto);
        }

        public override K Read<K>(long id)
        {
            var sql = $"SELECT a.[id], a.[pid], a.[key], a.[types], a.[modes], a.[names], a.[path], a.[remark], a.[file_type], a.[file_date], a.[file_time], a.[update_time], a.[create_time] FROM[doc] a WHERE a.[id] = {id}";
            return QueryFirst<K>(sql);
        }

        public override void Save(DocDto dto)
        {
            dto.update_time = DateTime.Now;
            var sql = "";
            if (IsValidId(dto.id))
            {
                sql = "UPDATE [doc] SET[pid] = @pid,[key] = @key,[types] = @types,[modes] = @modes,[names] = @names,[path] = @path,[remark] = @remark,[update_time] = @update_time WHERE[id] = @id";
                ExecuteUpdate(sql, dto);
            }
            else
            {
                dto.create_time = dto.update_time;
                sql = "INSERT INTO [doc] ([pid],[key],[types],[modes],[names],[path],[remark],[file_type],[file_date],[file_time],[update_time],[create_time]) VALUES (@pid,@key,@types,@modes,@names,@path,@remark,@file_type,@file_date,@file_time,@update_time,@create_time)";
                ExecuteInsert(sql, dto);
            }
        }

        public override void Delete(long id)
        {
            ExecuteDelete($"DELETE FROM [doc] WHERE [id]={id}");
        }

        public void AppendTag(long docId, long tagId)
        {
            var now = DateTime.Now;
            var sql = $"INSERT INTO [tag_doc] ([tag_id],[doc_id],[update_time],[create_time]) VALUES ({tagId},{docId},{now},{now})";
            ExecuteInsert(sql);

            new TagDao().UpdateQty(tagId, 1);
        }

        public void AppendTagOnly(long docId, long tagId)
        {
            var now = DateTime.Now;
            var sql = $"INSERT INTO [tag_doc] ([tag_id],[doc_id],[update_time],[create_time]) VALUES ({tagId},{docId},{now},{now})";
            ExecuteInsert(sql);
        }

        public void RemoveTag(long docId, long tagId)
        {
            var now = DateTime.Now;
            var sql = $"DELETE FROM [tag_doc] WHERE [doc_id] = {docId} AND [tag_id] = {tagId}";
            ExecuteDelete(sql);
        }
    }
}
