using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Marketplace.App.Helpers
{
    public static class SessionHelpers
    {
        public static TModel GetObjectFromJson<TModel>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(TModel) : JsonConvert.DeserializeObject<TModel>(value);
        }
        
        public static void SetObjectToJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value, new JsonSerializerSettings
            {
            }));
        }
    }
}
