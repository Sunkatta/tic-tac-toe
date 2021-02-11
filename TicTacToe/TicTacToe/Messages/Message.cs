namespace TicTacToe.Messages
{
    public class Message
    {
        public MessageType Type { get; private set; }

        public object Content { get; private set; }

        public Message(MessageType type, object content)
        {
            Type = type;
            Content = content;
        }
    }
}
