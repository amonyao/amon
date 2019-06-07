using Me.Amon.Dto;

namespace Me.Amon.Dao
{
    public abstract class ScmDao<T> where T : ScmDto
    {
        /// <summary>
        /// 读取对象
        /// </summary>
        public abstract T Load();

        /// <summary>
        /// 保存对象
        /// </summary>
        /// <param name="dto"></param>
        public abstract void Save(T dto);
    }
}
