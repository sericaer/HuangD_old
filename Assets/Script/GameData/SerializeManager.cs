using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq.Expressions;

using UnityEngine;
using Newtonsoft.Json;
using System.Linq;
using System.IO;
using Newtonsoft.Json.Linq;

public partial class MyGame
{
    public class SerializeManager
    {
        internal static string Serial()
        {

            var query = AppDomain.CurrentDomain.GetAssemblies()
                       .SelectMany(assembly => assembly.GetTypes())
                                 .Where(type => type.IsSubclassOf(typeof(SerializeManager)));

            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);

            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                writer.Formatting = Formatting.Indented;

                writer.WriteStartObject();

                foreach (Type type in query)
                {
                    writer.WritePropertyName(type.Name);
                    writer.WriteRawValue(GetTypeJsonValue(type));

                }

                List<SERIAL_EVENT> serialEvent = new List<SERIAL_EVENT>();
                foreach (var elem in StreamManager.eventDict)
                {
                    if (elem.Value.LastTriggleDay != null)
                    {
                        serialEvent.Add(new SERIAL_EVENT{ name = elem.Key, lasttriggle = elem.Value.LastTriggleDay });
                    }
                }

                writer.WritePropertyName("EVENT");
                writer.WriteRawValue(JsonConvert.SerializeObject(serialEvent, settings));

                writer.WriteEndObject();
            }



            return JToken.Parse(sb.ToString()).ToString();
        }

        public static void Deserial(string json)
        {
            JObject jsonObj = JObject.Parse(json);

            var query = AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes())
                     .Where(type => type.IsSubclassOf(typeof(SerializeManager)));

            foreach (Type type in query)
            {
                Deserial(type, jsonObj.GetValue(type.Name).ToString());
            }

            IList<JToken> results =jsonObj["EVENT"].Children().ToList();

            foreach (JToken result in results)
            {
                dynamic searchResult = result.ToObject<SERIAL_EVENT>();
                StreamManager.eventDict[searchResult.name].LastTriggleDay = searchResult.lasttriggle;
            }

        }

        public static void Deserial(Type type, string json)
        {
            JObject jsonObj = JObject.Parse(json);

            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            foreach (FieldInfo field in fields)
            {
                if (!Attribute.IsDefined(field, typeof(SerializeField)))
                {
                    continue;
                }

                field.SetValue(null, JsonConvert.DeserializeObject(jsonObj.GetValue(field.Name).ToString(), field.FieldType, settings));
            }

            MethodInfo method = type.GetMethod("AfterDeserial", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            if(method != null)
            {
                method.Invoke(null, null);
            }
        }

        static string GetTypeJsonValue(Type type)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);

            using (JsonWriter writer = new JsonTextWriter(sw))
            {
                writer.Formatting = Formatting.Indented;
                writer.WriteStartObject();

                FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
                foreach (FieldInfo field in fields)
                {
                    if (!Attribute.IsDefined(field, typeof(SerializeField)))
                    {
                        continue;
                    }

                    writer.WritePropertyName(field.Name);
                    writer.WriteRawValue(JsonConvert.SerializeObject(field.GetValue(null), settings));
                }

                writer.WriteEndObject();
            }

            return sb.ToString();
        }

        static JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };

        struct SERIAL_EVENT
        {
            public string name;
            public GameTime lasttriggle;
        }

    }
}
