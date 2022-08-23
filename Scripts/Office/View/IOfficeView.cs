using Scripts.Theater;
using UniRx;

public interface IOfficeView
{
    void InitializeIdolTileDeckWith(List<IdolTile> allIdolTileInfo);
    void InitializeSongCardWith(List<SongCard> allSongTileInfo);
    void IdolTileOpened(IdolTile item);
    void SongCardOpened(SongCard item);
    void IdolDeckAdded(IdolTile item);
    void SongDeckAdded(SongCard item);
}