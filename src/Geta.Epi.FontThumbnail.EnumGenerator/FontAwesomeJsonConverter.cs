using Geta.Epi.FontThumbnail.EnumGenerator.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Geta.Epi.FontThumbnail.EnumGenerator
{
    public class FontAwesomeJsonConverter : JsonConverter
    {
        public override bool CanWrite => false;
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => throw new NotImplementedException();

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(List<MetadataIcon>);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var token = JToken.Load(reader);
            var list = Activator.CreateInstance(objectType) as IList;
            var itemType = objectType.GenericTypeArguments[0];
            foreach (var child in token.Values())
            {
                var newObject = Activator.CreateInstance(itemType);
                serializer.Populate(child.CreateReader(), newObject);
                ((MetadataIcon)newObject).Name = child.Path;
                list.Add(newObject);
            }
            return list;
        }
    }
}
