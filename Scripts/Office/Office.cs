
using System.Collections;
using System.Collections.ObjectModel;
using UniRx;

namespace Scripts.Office;

public class Office
{
    public ReactiveCollection<IdolTile> OpenedIdolTiles = new ReactiveCollection<IdolTile>();
    public ReactiveCollection<SongCard> OpenedSongCards = new ReactiveCollection<SongCard>();

    public ReactiveCollection<IdolTile> IdolDeck = new ReactiveCollection<IdolTile>();
    public ReactiveCollection<SongCard> SongDeck = new ReactiveCollection<SongCard>();

    public ReactiveCollection<IdolTile> RemovedIdolTiles = new ReactiveCollection<IdolTile>();
    public ReactiveCollection<SongCard> RemovedSongCards = new ReactiveCollection<SongCard>();

    public void Shuffle<T>(List<T> list)
    {
        var rnd = new Random();
        var randomized = list.OrderBy(item => rnd.Next());
        
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
            OpenedIdolTiles.Add(item);
        }, onCompleted: () =>
        {
            OpenedIdolTiles.ToObservable().Subscribe(x => IdolDeck.Remove(x));
        });
    }
}