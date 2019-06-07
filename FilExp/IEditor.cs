namespace Me.Amon.FilExp
{
    public interface IEditor
    {
        /// <summary>
        /// 首页
        /// </summary>
        void Home();

        /// <summary>
        /// 新增
        /// </summary>
        void Create();

        /// <summary>
        /// 删除
        /// </summary>
        void Delete();

        /// <summary>
        /// 剪切
        /// </summary>
        void Cut();

        /// <summary>
        /// 复制
        /// </summary>
        void Copy();

        /// <summary>
        /// 粘贴
        /// </summary>
        void Paste();

        /// <summary>
        /// 跳转
        /// </summary>
        /// <param name="uri"></param>
        void Goto(string uri);

        void ImportByDoc();

        void ImportByCat();

        /// <summary>
        /// 重命名
        /// </summary>
        void Rename();
    }
}
