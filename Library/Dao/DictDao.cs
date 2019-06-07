using Me.Amon.Dto;
using System.Collections.Generic;

namespace Me.Amon.Dao
{
    public class DictDao : ScmDba<DictDto>
    {
        /// <summary>
        /// 字典缓存
        /// </summary>
        private static IEnumerable<DictDto> _List;
        private static DictDto _Default = new DictDto { text = "-" };

        /// <summary>
        /// 获取字典对象
        /// </summary>
        /// <param name="cat"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public DictDto GetDict(string cat, string key)
        {
            if (_List == null)
            {
                _List = ListAll<DictDto>();
            }

            foreach (var item in _List)
            {
                if (item.cat == cat && item.key == key)
                {
                    return item;
                }
            }

            return _Default;
        }

        public override System.Collections.Generic.IEnumerable<K> List<K>(DictDto dto, Orderby orderby = null)
        {
            if (orderby == null)
            {
                orderby = new Orderby();
            }
            var sql = $"SELECT a.[id],a.[key],a.[text],a.[tips],a.[create_time] FROM [sys_dict] a WHERE a.[cat] = '{dto.cat}'";
            return Query<K>(sql, dto);
        }

        public System.Collections.Generic.IEnumerable<K> ListAll<K>()
        {
            var sql = $"SELECT a.[id],a.[cat],a.[key],a.[text],a.[tips],a.[remark],a.[create_time] FROM [sys_dict] a WHERE a.[status] = 1";
            return Query<K>(sql);
        }

        public override K Read<K>(long id)
        {
            var sql = $"SELECT a.[id],a.[cat],a.[key],a.[text],a.[tips],a.[create_time] FROM [sys_dict] a WHERE a.[cat] = '{id}'";
            return QueryFirst<K>(sql);
        }

        public override void Save(DictDto dto)
        {
            var sql = "";
            if (IsValidId(dto.id))
            {
                sql = "UPDATE [sys_dict] SET [cat] = @cat,[key] = @key,[text] = @text,[tips] = @tips WHERE [id] = @id";
                ExecuteUpdate(sql, dto);
            }
            else
            {
                dto.create_time = dto.update_time;
                sql = "INSERT INTO [sys_dict] ([cat],[key],[text],[tips],[create_time]) VALUES (@cat,@key,@text,@tips,@create_time)";
                ExecuteInsert(sql, dto);
            }
        }

        public override void Delete(long id)
        {
            ExecuteDelete($"DELETE FROM [sys_dict] WHERE [id]={id}");
        }
    }
}
