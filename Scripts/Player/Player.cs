using Scripts.Theater;
using UniRx;

namespace Scripts.Player;

public class Player
{
    public ReactiveCollection<SongCard> SongCardHand = new ReactiveCollection<SongCard>();
    public ReactiveProperty<int> Score = new ReactiveProperty<int>();

    public global::Scripts.Theater.Theater? Theater;
    public Subject<bool> GameInitalized { get; set; }
}