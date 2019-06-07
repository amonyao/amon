using Me.Amon.FilExt;
using System;
using System.Collections.Generic;

namespace Me.Amon.Dao
{
    public class RuleDao : ScmDba<RuleDto>
    {
        public override IEnumerable<K> List<K>(RuleDto dto, Orderby orderby = null)
        {
            if (orderby == null)
            {
                orderby = new Orderby();
            }
            var sql = @"SELECT a.[id], a.[name], a.[method], a.[src_file], a.[src_path], a.[dst_path], a.[dst_file], a.[repeat], a.[remark], a.[update_time], a.[create_time] FROM [fms_rule] a WHERE a.[status] = 1";
            return Query<K>(sql, dto);
        }

        public override K Read<K>(long id)
        {
            var sql = @"SELECT a.[id], a.[name], a.[method], a.[src_file], a.[src_path], a.[dst_path], a.[dst_file], a.[repeat], a.[remark], a.[status], a.[update_time], a.[create_time] FROM [fms_rule] a WHERE a.[id] = {id}";
            return QueryFirst<K>(sql);
        }

        public override void Save(RuleDto dto)
        {
            dto.update_time = DateTime.Now;
            var sql = "";
            if (IsValidId(dto.id))
            {
                sql = "UPDATE [fms_rule] SET [name] = @name,[method] = @method,[src_file] = @src_file,[src_path] = @src_path,[dst_path] = @dst_path,[dst_file] = @dst_file,[repeat] = @repeat,[remark] = @remark,[update_time] = @update_time WHERE[id] = @id";
                ExecuteUpdate(sql, dto);
            }
            else
            {
                dto.create_time = dto.update_time;
                sql = "INSERT INTO [fms_rule] ([name],[method],[src_file],[src_path],[dst_path],[dst_file],[repeat],[remark],[status],[update_time],[create_time]) VALUES (@name,@method,@src_file,@src_path,@dst_path,@dst_file,@repeat,@remark,@status,@update_time,@create_time)";
                ExecuteInsert(sql, dto);
            }
        }

        public override void Delete(long id)
        {
            ExecuteDelete($"DELETE FROM [fms_rule] WHERE [id]={id}");
        }
    }
}
