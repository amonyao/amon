using Me.Amon.FilExp.Dto;
using System;
using System.Collections.Generic;

namespace Me.Amon.Dao
{
    public class KeyDao : ScmDba<KeyDto>
    {
        public override IEnumerable<K> List<K>(KeyDto dto, Orderby orderby = null)
        {
            if (orderby == null)
            {
                orderby = new Orderby();
            }
            var sql = @"SELECT [id],[qty],[key],[file],[create_time] FROM [doc_key] a " + orderby.GenerateOrderBy("a.");
            return Query<K>(sql, dto);
        }

        public override K Read<K>(long id)
        {
            var sql = $"SELECT [id],[qty],[key],[file],[create_time] FROM [doc_key] a WHERE a.[id] = {id}";
            return QueryFirst<K>(sql);
        }

        public KeyDto Read(string key)
        {
            var sql = $"SELECT [id],[qty],[key],[file],[create_time] FROM [doc_key] a WHERE a.[key] = '{key}'";
            return QueryFirst<KeyDto>(sql);
        }

        public override void Save(KeyDto dto)
        {
            dto.update_time = DateTime.Now;
            var sql = "";
            if (IsValidId(dto.id))
            {
                sql = "UPDATE [doc_key] SET [qty] = @qty,[key] = @key,[file] = @file WHERE [id] = @id";
                ExecuteUpdate(sql, dto);
            }
            else
            {
                dto.create_time = dto.update_time;
                sql = "INSERT INTO [doc_key] ([qty],[key],[file],[create_time]) VALUES (@qty,@key,@file,@create_time)";
                ExecuteInsert(sql, dto);
            }
        }

        public override void Delete(long id)
        {
            ExecuteDelete($"DELETE FROM [doc_key] WHERE [id]={id}");
        }

        /// <summary>
        /// 改变引用数量
        /// </summary>
        /// <param name="id"></param>
        /// <param name="step"></param>
        public void Change(long id, int step)
        {
            ExecuteUpdate($"update [doc_key] set[qty] = [qty] + ({step}) where[id] = {id}");
        }
    }
}
