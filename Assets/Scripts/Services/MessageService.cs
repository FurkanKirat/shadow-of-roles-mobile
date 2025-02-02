using System.Collections.Generic;
using Models;

namespace Services
{
    public class MessageService
    {
        private static LinkedList<Message> _messages = new LinkedList<Message>();


        public static void ResetMessages() {
            _messages.Clear();
        }


        public static void SendMessage(string message, Player receiver, bool isPublic, bool isDay) {
            _messages.AddLast(new Message(GameScreenController.getGameService().getTimeService().getDayCount(),
                isDay, message, receiver, isPublic));
        }


        public static LinkedList<Message> GetMessages() {
            return _messages;
        }
    }
}