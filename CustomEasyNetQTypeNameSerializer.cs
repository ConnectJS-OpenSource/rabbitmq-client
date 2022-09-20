using EasyNetQ;
using Newtonsoft.Json.Linq;
using System.Collections.Concurrent;

namespace PNH_Queue_Processor
{
    public class TypeSerializer : ITypeNameSerializer
    {
        public static readonly ConcurrentDictionary<string, Type> TypeNames = new ConcurrentDictionary<string, Type>();

        public TypeSerializer()
        {
            TypeNames.Clear();
            TypeNames.TryAdd("email", typeof(EmailMessage));
        }

        public Type DeSerialize(string typeName)
        {
            if (!string.IsNullOrEmpty(typeName) && TypeNames.ContainsKey(typeName))
                return TypeNames[typeName];

            return typeof(JObject);
        }

        public string Serialize(Type type)
        {
            return "emailMessage";
        }
    }
}