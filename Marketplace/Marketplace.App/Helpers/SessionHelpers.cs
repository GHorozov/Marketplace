using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.App.Helpers
{
    public static class SessionHelpers
    {
        public static TModel GetObjectFromJson<TModel>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(TModel) : JsonConvert.DeserializeObject<TModel>(value);

            //var value = session.GetString(key);
            //var result = value == null ? default(TModel) : JsonConvert.DeserializeObject<TModel>(value);

            //return result;
        }
        
        public static void SetObjectToJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value, new JsonSerializerSettings
            {
            }));

            // session.SetString(key, JsonConvert.SerializeObject(value, new JsonSerializerSettings() { }));
        }
    }
}
