using EasyNetQ;

namespace PNH_Queue_Processor
{
    internal class EmailMessage
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string[] Attachments { get; set; }

        public static void MessageHandler(EmailMessage message, MessageReceivedInfo metadata)
        {
            Console.WriteLine(message);
        }
    }
}