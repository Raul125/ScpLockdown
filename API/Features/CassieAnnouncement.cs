namespace SCPLockdown.API.Features;

public class CassieAnnouncement
{
    public string Content { get; set; }
    public int Delay { get; set; }

    public CassieAnnouncement(string content, int delay)
    {
        Content = content;
        Delay = delay;
    }

    public CassieAnnouncement() { }
}
