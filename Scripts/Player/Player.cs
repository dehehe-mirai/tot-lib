using UniRx;

namespace Scripts.Player;

public class Player
{
    public ReactiveProperty<int> Score = new ReactiveProperty<int>();

    public global::Theater Theater;
}