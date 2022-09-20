using EasyNetQ;
using Newtonsoft.Json.Linq;

namespace PNH_Queue_Processor
{
    internal class JObjectMessage
    {
        public static void MessageHandler(JObject message, MessageReceivedInfo metadata)
        {
            Console.WriteLine(message);
        }
    }
}