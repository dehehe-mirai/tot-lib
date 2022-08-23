using UniRx;

namespace Scripts.Theater;

public class Theater
{
    public readonly ReactiveCollection<IdolTile> IdolTiles = new ReactiveCollection<IdolTile>();
    public readonly ReactiveCollection<SongCard> SongCards = new ReactiveCollection<SongCard>();
    public readonly ReactiveCollection<List<string?>> InteractedUnitIdol = new ReactiveCollection<List<string?>>(); 
    
    public Theater()
    {
        IdolTiles.ObserveAdd().Subscribe(OnAddIdol);
    }

    private void OnAddIdol(CollectionAddEvent<IdolTile> item)
    {
        var interactedIdol = new List<string?>();
        var idols = IdolTiles.ToList();
        var songs = SongCards.ToList();
        
        foreach (var songCard in songs)
        {
            if (!songCard.Unit.Contains(item.Value.Name)) continue;
            
            foreach (var idol in idols)
            {
                
                if (!songCard.Unit.Contains(idol.Name)) continue;
                
                if (Math.Abs(idol.X - item.Value.X) == 1
                    && Math.Abs(idol.Y - item.Value.Y) == 1) continue;
            
                if (Math.Abs(idol.X - item.Value.X) == 1 
                    || Math.Abs(idol.X - item.Value.Y) == 1)
                {
                    if (!interactedIdol.Contains(item.Value.Name)) interactedIdol.Add(item.Value.Name);
                    
                    if (!interactedIdol.Contains(idol.Name)) interactedIdol.Add(idol.Name);
                }     
            }
            
        }
        
        if (interactedIdol.Count > 1) InteractedUnitIdol.Add(interactedIdol);
    }

    public void AddIdolTile(IdolTile idolTile)
    {
        IdolTiles.Add(idolTile);
    }

    public void AddSongCard(SongCard songCard)
    {
        SongCards.Add(songCard);
    }
}