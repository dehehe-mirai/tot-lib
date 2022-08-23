namespace Scripts.Theater;

public interface ITheaterView
{
    void AddIdol(IdolTile itemValue);
    void RemoveIdol(IdolTile itemValue);
    void AddSongCard(SongCard songCard);
    void GotScore(List<string> list);
}