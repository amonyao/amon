using Dapper;
using Me.Amon.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace Me.Amon.Dao
{
    public abstract class ScmDba<T> : ScmDao<T> where T : ScmDto
    {
        private static string _DbFile = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "amon.dbo");

        private static SQLiteConnection _Connection;
        private IDbConnection Connection
        {
            get
            {
                if (_Connection != null)
                {
                    return _Connection;
                }

                var isNew = !File.Exists(_DbFile);
                _Connection = new SQLiteConnection($"Data Source={_DbFile};Version=3;");
                _Connection.Open();
                if (isNew)
                {
                    InitDb();
                }

                return _Connection;
            }
        }

        /// <summary>
        /// 数据库初始化
        /// </summary>
        private void InitDb()
        {
            var file = "Data\\Init.sql";
            if (!File.Exists(file))
            {
                return;
            }

            var lines = File.ReadAllLines(file);

            var command = new SQLiteCommand();
            command.CommandType = CommandType.Text;
            command.Connection = _Connection;
            var sql = "";
            foreach (var line in lines)
            {
                var tmp = line.Trim();
                if (string.IsNullOrWhiteSpace(tmp) || tmp.StartsWith("/*") || tmp.StartsWith("//"))
                {
                    sql = "";
                    continue;
                }

                sql += " " + tmp;

                if (tmp.EndsWith(";"))
                {
                    command.CommandText = sql.Substring(0, sql.Length - 1);
                    command.ExecuteNonQuery();
                    sql = "";
                }
            }
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public abstract IEnumerable<K> List<K>(T dto, Orderby orderby = null);

        /// <summary>
        /// 读取单个对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public abstract K Read<K>(long id);

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="id"></param>
        public abstract void Delete(long id);

        public override T Load()
        {
            return null;
        }

        protected bool IsValidId(long id)
        {
            return id > 0;
        }

        protected IEnumerable<K> Query<K>(string sql, ScmDto dto)
        {
            return Connection.Query<K>(sql, dto);
        }

        protected IEnumerable<K> Query<K>(string sql)
        {
            return Connection.Query<K>(sql);
        }

        protected K QueryFirst<K>(string sql)
        {
            return Connection.QueryFirstOrDefault<K>(sql);
        }

        protected int ExecuteDelete(string sql)
        {
            return Connection.Execute(sql);
        }

        /// <summary>
        /// 更新操作
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="obj"></param>
        protected void ExecuteUpdate(string sql, ScmDto obj)
        {
            Connection.Execute(sql, obj);
        }

        protected void ExecuteUpdate(string sql)
        {
            Connection.Execute(sql);
        }

        /// <summary>
        /// 插入操作，返回主键ID
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="obj"></param>
        protected void ExecuteInsert(string sql, ScmDto obj)
        {
            sql += ";SELECT last_insert_rowid()";

            obj.id = Connection.QueryFirst<long>(sql, obj);
            //var p = new DynamicParameters();
            //p.Add("@id", dbType: DbType.Int32, direction: ParameterDirection.Output);
            //var i = Connection.Execute(sql, p);
            //SQLiteCommand d;
            //return p.Get<long>("@id");
        }

        /// <summary>
        /// 不返回主键ID
        /// </summary>
        /// <param name="sql"></param>
        protected void ExecuteInsert(string sql)
        {
            Connection.Execute(sql);
        }
    }
}
