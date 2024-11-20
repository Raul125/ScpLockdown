namespace ScpLockdown.API.Features;

public class CassieAnnouncement
{
    public CassieAnnouncement()
    {
    }

    public CassieAnnouncement(string content, string subtitle, int delay)
    {
        Content = content;
        Subtitle = subtitle;
        Delay = delay;
    }

    public string Content { get; init; }
    public string Subtitle { get; init; }
    public int Delay { get; init; }
}