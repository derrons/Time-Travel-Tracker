using Tavel_Tracker_API.Enums;

namespace Tavel_Tracker_API.Models;

public class Location
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Address1 { get; set; } = string.Empty;
    public string Address2 { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public int Zip { get; set; }
    public DateTime Time { get; set; }
    public LocationEntryType EntryType { get; set; }
}
