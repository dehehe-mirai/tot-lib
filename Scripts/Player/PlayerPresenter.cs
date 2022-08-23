using UniRx;

namespace Scripts.Player;

public class PlayerPresenter
{
    private Player? _player;
    private readonly IPlayerInfoView _infoView;

    public Player? Player
    {
        get => _player;
        set
        {
            this._player = value;

            value?.Score.Subscribe(i => { _infoView.PlayerScoreChanged(i); });
        }
    }

    public PlayerPresenter(IPlayerInfoView infoView)
    {
        _infoView = infoView;
        
    }

    public void InitializeWithPlayer(Player? player)
    {
        Player = player;
        
        _infoView.SetPlayer(Player);

        Player?.SongCardHand.ObserveAdd().Subscribe(onNext: item => { _infoView.SongCardAddedInHand(item.Value); });
    }
}