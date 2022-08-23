namespace Scripts.Theater;

public class SongCard
{
    public string? SongName { get; }
    public string? UnitName { get; }
    public List<string?>? Unit { get; }

    public SongCard()
    {
    }

    private SongCard(string? songName, string? unitName, List<string?>? unit)
    {
        SongName = songName;
        UnitName = unitName;
        Unit = unit;
    }

    public static SongCard Of(string? songName, string? unitName, List<string?>? unit)
    {
        return new SongCard(songName, unitName, unit);
    }
}