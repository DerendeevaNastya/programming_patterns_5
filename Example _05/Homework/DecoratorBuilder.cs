using System.Text;

namespace Example__05.Homework
{
    public class DecoratorBuilder
    {
        private IChatClient chatClient;

        public DecoratorBuilder()
        {
            chatClient = new SimpleChatClient();
        }
        
        public DecoratorBuilder WithHideDudes()
        {
            chatClient = new HideDudesDecorator(chatClient);
            return this;
        }

        public DecoratorBuilder WithHideText()
        {
            chatClient = new HideTextDecorator(chatClient);
            return this;
        }
    }
}