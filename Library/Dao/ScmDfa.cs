using Me.Amon.Dto;
using Newtonsoft.Json;
using System.IO;

namespace Me.Amon.Dao
{
    public class ScmDfa<T> : ScmDao<T> where T : ScmDto
    {
        private string _File;

        public override T Load()
        {
            var text = File.ReadAllText(_File);
            return JsonConvert.DeserializeObject<T>(text);
        }

        public override void Save(T obj)
        {
            var text = JsonConvert.SerializeObject(obj, GetSettings());
            File.WriteAllText(_File, text);
        }

        private static JsonSerializerSettings _Settings;

        private static JsonSerializerSettings GetSettings()
        {
            if (_Settings == null)
            {
                _Settings = new JsonSerializerSettings();
            }
            return _Settings;
        }
    }
}
