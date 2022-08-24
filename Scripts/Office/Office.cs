using Scripts.Theater;
using UniRx;

namespace Scripts.Office;

public class Office
{
    public readonly ReactiveCollection<IdolTile> OpenedIdolTiles = new ReactiveCollection<IdolTile>();
    public readonly ReactiveCollection<SongCard> OpenedSongCards = new ReactiveCollection<SongCard>();

    public readonly ReactiveCollection<IdolTile> IdolDeck = new ReactiveCollection<IdolTile>();
    public readonly ReactiveCollection<SongCard> SongDeck = new ReactiveCollection<SongCard>();

    public readonly ReactiveCollection<IdolTile> RemovedIdolTiles = new ReactiveCollection<IdolTile>();
    public ReactiveCollection<SongCard> RemovedSongCards = new ReactiveCollection<SongCard>();

    public ReactiveCollection<IdolTile> OpenedIdolFromDeck = new ReactiveCollection<IdolTile>(); 

    public static void Shuffle<T>(List<T> list)
    {
        var rnd = new Random();
        var randomized = list.OrderBy(_ => rnd.Next());
        
        list.Clear();
        list.AddRange(randomized);
    }
    public void SetInitialIdolTile(List<IdolTile> allIdolTileInfo)
    {
        allIdolTileInfo.ToObservable().Subscribe(x =>
        {
            IdolDeck.Add(x);
        }, onCompleted: () =>
        {
            Shuffle(IdolDeck.ToList());    
        });
    }

    public void SetInitialSongCard(List<SongCard> allSongCardsInfo)
    {
        allSongCardsInfo.ToObservable().Subscribe(x=>SongDeck.Add(x), onCompleted: () =>
        {
            Shuffle(SongDeck.ToList());
        });
    }

    public void OpenIdolTileFromDeck(int i)
    {
        if (i > IdolDeck.Count)
        {
            RemovedIdolTiles.ToObservable().Subscribe(
                x=>IdolDeck.Add(x), 
                onCompleted:()=>
                {
                    RemovedIdolTiles.Clear();
                    Shuffle(IdolDeck.ToList());
                });
        }
        
        IdolDeck.ToObservable().Take(i - OpenedIdolTiles.Count).Subscribe(item =>
        {
            OpenedIdolFromDeck.Add(item);
            OpenedIdolTiles.Add(item);
            
        }, onCompleted: () =>
        {
            OpenedIdolTiles.ToObservable().Subscribe(x => IdolDeck.Remove(x));
        });
    }
}