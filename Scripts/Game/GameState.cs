using Scripts.Player;
using UniRx;

public class GameState
{
    public Subject<GamePhase> Phase = new Subject<GamePhase>();
    
    public List<Player> players = new List<Player>();

    public ReactiveProperty<int> round;

    public void StartGame()
    {
        Phase.OnNext(GamePhase.Initial);
        var playerReadiness = Observable.Empty<bool>();
        
        foreach (var player in players)
        {
            playerReadiness.Merge(player.GameInitalized);
        }

        playerReadiness.Take(players.Count).Subscribe(onNext: b => { }, 
            onCompleted:(() =>
            {
                GameRoutine();
            }));
    }

    private void GameRoutine()
    {
        throw new NotImplementedException();
    }
}