using UniRx;

public class Theater
{
    public ReactiveCollection<IdolTile> IdolTiles = new ReactiveCollection<IdolTile>();
    public ReactiveCollection<SongCard> SongCards = new ReactiveCollection<SongCard>();
    
    public void AddIdolTile(IdolTile idolTile)
    {
        IdolTiles.Add(idolTile);
    }
}