public class GamePresenter
{
    private IPhaseView _phaseView;

    public GamePresenter(IPhaseView phaseView, GameState state)
    {
        _phaseView = phaseView;
    }
}