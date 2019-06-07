using Newtonsoft.Json;

namespace Me.Amon
{
    public static partial class Ext
    {
        #region Json
        public static string ToJsonString(this object obj, bool keepNullValue = false)
        {
            return obj == null ? null : JsonConvert.SerializeObject(obj, GetSettings(keepNullValue));
        }

        public static T AsJsonObject<T>(this string json, bool keepNullValue = false)
        {
            return string.IsNullOrWhiteSpace(json) ? default(T) : JsonConvert.DeserializeObject<T>(json, GetSettings(keepNullValue));
        }

        private static JsonSerializerSettings _JSettings;
        private static JsonSerializerSettings GetSettings(bool keepNullValue)
        {
            if (_JSettings == null)
            {
                _JSettings = new JsonSerializerSettings();
                //_Settings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                _JSettings.DateFormatString = Env.FORMAT_DATE_TIME;
                if (keepNullValue)
                {
                    //Nothing
                }
                else
                {
                    _JSettings.NullValueHandling = NullValueHandling.Ignore;
                }
            }
            return _JSettings;
        }
        #endregion
    }
}
