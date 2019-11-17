using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

namespace Example__05.Homework
{
    public interface IChatClient
    {
        void SendMessage(Message message);
        Message[] GetLastMessages();
    }

    public class HideTextDecorator : IChatClient
    {
        private readonly IChatClient _chatClient;

        public HideTextDecorator(IChatClient chatClient)
        {
            _chatClient = chatClient;
        }
        
        public void SendMessage(Message message)
        {
            message.Text = $"#зашифровано: {message.Text}";
            _chatClient.SendMessage(message);
        }

        public Message[] GetLastMessages()
        {
            var result = _chatClient.GetLastMessages();
            foreach (var message in result)
            {
                message.Text = message.Text.Split(' ').Last();
            }
            
            return result;
        }
    }

    public class HideDudesDecorator : IChatClient
    {
        private IChatClient client;

        public HideDudesDecorator(IChatClient chatClient)
        {
            client = chatClient;
        }
        
        public void SendMessage(Message message)
        {
            message.Author.Name = Encode(message.Author.Name);
            message.Addressee.Name = Encode(message.Addressee.Name);
            client.SendMessage(message);
        }

        public Message[] GetLastMessages()
        {
            return client.GetLastMessages();
        }

        private string Encode(string text)
        {
            
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(text);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }

    public class SimpleChatClient : IChatClient
    {
        private readonly Dude me;
        private long lastTimestamp;
        private List<Message> messages = new List<Message>();
        
        public void SendMessage(Message message)
        {
            if (message.Author.Name == me.Name)
            {
                lastTimestamp = DateTime.UtcNow.Ticks;
            }
            messages.Add(message);
        }

        public Message[] GetLastMessages()
        {
            return messages.Where(x => x.Timestamp > lastTimestamp).ToArray();
        }
    }
    
    
    
    
}