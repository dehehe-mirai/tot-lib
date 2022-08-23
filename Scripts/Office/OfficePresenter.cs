using Scripts.Office;
using UniRx;

public class OfficePresenter
{
    private IOfficeView _officeView;
    private Office _office;

    public Office Office
    {
        get => this._office;
        set
        {
            this._office = value;
            
            _office.IdolDeck.ObserveAdd().Subscribe(item =>
            {
                _officeView.IdolDeckAdded(item.Value);
            });
            
            _office.SongDeck.ObserveAdd().Subscribe(item =>
            {
                _officeView.SongDeckAdded(item.Value);
            });
            
            _office.OpenedIdolTiles.ObserveAdd().Subscribe(item =>
            {
                _officeView.IdolTileOpened(item.Value);
            });
            
            _office.OpenedSongCards.ObserveAdd().Subscribe(item =>
            {
                _officeView.SongCardOpened(item.Value);
            });
        }
    }

    public OfficePresenter(IOfficeView officeView)
    {
        _officeView = officeView;

    }

}