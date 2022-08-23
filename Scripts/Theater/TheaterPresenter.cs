using UniRx;

namespace Scripts.Theater;

public class TheaterPresenter
{
    private readonly ITheaterView _view;
    private global::Theater? _theater;

    public global::Theater? Theater
    {
        get => _theater;
        set
        {
            _theater = value;

            if (_theater == null) return;
            
            _theater.IdolTiles.ObserveAdd().Subscribe(item => { _view.AddIdol(item.Value); });
            _theater.SongCards.ObserveAdd().Subscribe(item => { _view.AddSongCard(item.Value); });
            _theater.InteractedUnitIdol.ObserveAdd().Subscribe(item =>
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