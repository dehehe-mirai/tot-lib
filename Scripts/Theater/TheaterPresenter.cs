using Scripts.Theater;
using UniRx;

public class TheaterPresenter
{
    private readonly ITheaterView _view;
    private Theater _theater;

    public Theater Theater
    {
        get => _theater;
        set
        {
            _theater = value;
            
            _theater.IdolTiles.ObserveAdd().Subscribe(item=>
            {
                _view.AddIdol(item.Value);
            });
            _theater.SongCards.ObserveAdd().Subscribe(item=>
            {
                _view.AddSongCard(item.Value);
            });
            _theater.InteractedUnitIdol.ObserveAdd().Subscribe(item=>
            {
                if (item.Value.Count != 0) _view.GotScore(item.Value);
            });
        }
    }

    public TheaterPresenter(ITheaterView view)
    { 
        this._view = view;
    }
}