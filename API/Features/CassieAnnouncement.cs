namespace ScpLockdown.API.Features;

public class CassieAnnouncement
{
    public CassieAnnouncement()
    {
    }

    public CassieAnnouncement(string content, int delay)
    {
        Content = content;
        Delay = delay;
    }

    public string Content { get; init; }
    public int Delay { get; init; }
}