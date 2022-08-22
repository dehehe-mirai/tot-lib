using Moq;
using Scripts;
using Scripts.Theater;

namespace Tests;

public class TheaterTests
{
    private PlayerPresenter _playerPresenter;
    private TheaterPresenter _theaterPresenter;
    private Mock<ITheaterView> _theaterView;

    [SetUp]
    public void Setup()
    {
        _theaterView = new Mock<ITheaterView>();
        _theaterPresenter = new TheaterPresenter(_theaterView.Object);
    }

    [Test]
    public void Theater_FirstIdolTileAdded()
    {
        var theater = new Theater();
        _theaterPresenter.Theater = theater;
        Assert.NotNull(_theaterPresenter.Theater);
        
        var idolTile = new IdolTile();
        
        theater.AddIdolTile(idolTile);
        Thread.Sleep(1);
        _theaterView.Verify(x=>x.AddIdol(idolTile));
    }
    
}