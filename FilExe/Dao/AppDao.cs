using Me.Amon.Dao;
using Me.Amon.FilExe.Dto;
using System;

namespace Me.Amon.FilExe.Dao
{
    public class AppDao : ScmDba<AppDto>
    {
        public override System.Collections.Generic.IEnumerable<K> List<K>(AppDto dto, Orderby orderby = null)
        {
            if (orderby == null)
            {
                orderby = new Orderby();
            }
            var sql = @"SELECT a.[id],a.[od],a.[os],a.[text],a.[tips],a.[keys],a.[file],a.[path],a.[status],a.[update_time],a.[create_time] FROM [cmd_file] a WHERE a.[status] = 1";
            return Query<K>(sql, dto);
        }

        public override K Read<K>(long id)
        {
            var sql = $"SELECT a.[id],a.[od],a.[os],a.[text],a.[tips],a.[keys],a.[file],a.[path],a.[status],a.[update_time],a.[create_time] FROM [cmd_file] a WHERE a.[id] = {id}";
            return QueryFirst<K>(sql);
        }

        public override void Save(AppDto dto)
        {
            dto.update_time = DateTime.Now;
            var sql = "";
            if (IsValidId(dto.id))
            {
                sql = "UPDATE [cmd_file] SET [od] = @od,[os] = @os,[text] = @text,[tips] = @tips,[keys] = @keys,[file] = @file,[path] = @path,[update_time] = @update_time WHERE [id] = @id";
                ExecuteUpdate(sql, dto);
            }
            else
            {
                dto.create_time = dto.update_time;
                sql = "INSERT INTO [cmd_file] ([od],[os],[text],[tips],[keys],[file],[path],[status],[create_time],[update_time]) VALUES (@od,@os,@text,@tips,@keys,@file,@path,@status,@create_time,@update_time)";
                ExecuteInsert(sql, dto);
            }
        }

        public void UpdateStep(AppDto dto)
        {
            dto.update_time = DateTime.Now;
            var sql = $"UPDATE [cmd_file] SET [od] = [od] + 1,[update_time] = @update_time WHERE [id] = @id";
            ExecuteUpdate(sql, dto);
        }

        public override void Delete(long id)
        {
            ExecuteDelete($"DELETE FROM [cmd_file] WHERE [id]={id}");
        }
    }
}
