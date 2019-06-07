using Me.Amon.FilExp.Dto;
using System;
using System.Collections.Generic;

namespace Me.Amon.Dao
{
    public class TagDao : ScmDba<TagDto>
    {
        public override IEnumerable<K> List<K>(TagDto dto, Orderby orderby = null)
        {
            if (orderby == null)
            {
                orderby = new Orderby();
            }
            var sql = @"SELECT [id],[qty],[names],[update_time],[create_time] FROM[tag] a " + orderby.GenerateOrderBy("a.");
            return Query<K>(sql, dto);
        }

        public IEnumerable<TagDto> List(DocDto dto)
        {
            var sql = $"SELECT b.[id],b.[qty],b.[names],a.[update_time],a.[create_time] FROM [tag_doc] a,[tag] b WHERE a.[tag_id] = b.[id] AND a.[doc_id] = {dto.id} ORDER BY b.[qty] desc";
            return Query<TagDto>(sql);
        }

        public override K Read<K>(long id)
        {
            var sql = $"SELECT [id],[qty],[names],[update_time],[create_time] FROM[tag] a WHERE a.[id] = {id}";
            return QueryFirst<K>(sql);
        }

        public override void Save(TagDto dto)
        {
            dto.update_time = DateTime.Now;
            var sql = "";
            if (IsValidId(dto.id))
            {
                sql = "UPDATE [tag] SET [qty] = @qty,[names] = @names,[update_time] = @update_time WHERE [id]=@id";
                ExecuteUpdate(sql, dto);
            }
            else
            {
                dto.create_time = dto.update_time;
                sql = "INSERT INTO [tag]([qty],[names],[update_time],[create_time]) VALUES (@qty,@names,@update_time,@create_time)";
                ExecuteInsert(sql, dto);
            }
        }

        public override void Delete(long id)
        {
            ExecuteDelete($"DELETE FROM [tag] WHERE [id]={id}");
        }

        public TagDto Read(string names)
        {
            var sql = $"SELECT [id],[qty],[names],[update_time],[create_time] FROM[tag] a WHERE a.[names] = {names}";
            return QueryFirst<TagDto>(sql);
        }

        /// <summary>
        /// 更新标签引用数量
        /// </summary>
        /// <param name="id"></param>
        /// <param name="step">变化量</param>
        public void UpdateQty(long id, int step)
        {
            var now = DateTime.Now;
            var sql = $"UPDATE [tag] SET [qty] = [qty] + ({step}),[update_time] = '{now}' WHERE [id] = {id}";
            ExecuteUpdate(sql);
        }
    }
}
