using Models;

namespace System.Runtime.CompilerServices
{
    internal static class IsExternalInit {}
}
public class Message
{
    public int DayCount { get; init; }
    public bool IsDay { get; init; }
    public string Text { get; init; }
    public Player Receiver { get; init; }
    public bool IsPublic { get; init; }

    public Message(int dayCount, bool isDay, string text, Player receiver, bool isPublic)
    {
        DayCount = dayCount;
        IsDay = isDay;
        Text = text;
        Receiver = receiver;
        IsPublic = isPublic;
    }
}