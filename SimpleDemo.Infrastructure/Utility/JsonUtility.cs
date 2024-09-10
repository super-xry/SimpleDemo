using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace SimpleDemo.Infrastructure.Utility
{
    public static class JsonUtility
    {
        public static string? SerializeObject<T>(T data, IContractResolver? resolver = default)
        {
            var settings = CreateJsonSerializerSettings(resolver);

            return data == null ? null : JsonConvert.SerializeObject(data, settings);
        }

        public static T? DeserializeObject<T>(string? data, IContractResolver? resolver = default)
        {
            var settings = CreateJsonSerializerSettings(resolver);

            return data == null ? default : JsonConvert.DeserializeObject<T>(data, settings);
        }

        private static JsonSerializerSettings CreateJsonSerializerSettings(IContractResolver? resolver = default)
        {
            var defaultJsonSerializerSettings = new JsonSerializerSettings()
            {
                ContractResolver = resolver ?? new CamelCasePropertyNamesContractResolver()
            };

            defaultJsonSerializerSettings.Converters.Add(new StringEnumConverter());

            return defaultJsonSerializerSettings;
        }
    }
}