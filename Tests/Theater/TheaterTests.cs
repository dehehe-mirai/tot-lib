using Moq;
using Scripts;
using Scripts.Theater;

namespace Tests;

public class TheaterTests
{
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
    
    [Test]
    public void Theater_SongCardAdded()
    {
        var theater = new Theater();
        _theaterPresenter.Theater = theater;
        Assert.NotNull(_theaterPresenter.Theater);
        
        var songCard = new SongCard();
        
        theater.AddSongCard(songCard);
        Thread.Sleep(1);
        _theaterView.Verify(x=>x.AddSongCard(songCard));
    }
    
    [Test]
    public void Theater_GotScore()
    {
        var theater = new Theater();
        _theaterPresenter.Theater = theater;
        Assert.NotNull(_theaterPresenter.Theater);
        
        theater.AddSongCard(SongCard.Of("Absolute Run", "Strawberry Pop Moon",new List<string?> {"mirai", "shizuka", "tsubasa"}));
        
        theater.AddIdolTile(IdolTile.Of("mirai").SetXY(0, 0));
        theater.AddIdolTile(IdolTile.Of("shizuka").SetXY(1, 0));
        theater.AddIdolTile(IdolTile.Of("shizuka").SetXY(2, 0));
        
        Thread.Sleep(1);
        
        _theaterView.Verify(x=>x.GotScore(new List<string?> {"shizuka", "mirai"}));
    }
    [Test]
    public void Theater_GotScore_ThreeIdols()
    {
        var theater = new Theater();
        _theaterPresenter.Theater = theater;
        Assert.NotNull(_theaterPresenter.Theater);
        
        theater.AddSongCard(SongCard.Of("Absolute Run", "Strawberry Pop Moon",new List<string?> {"mirai", "shizuka", "tsubasa"}));
        
        theater.AddIdolTile(IdolTile.Of("mirai").SetXY(0, 0));
        theater.AddIdolTile(IdolTile.Of("shizuka").SetXY(1, 0));
        theater.AddIdolTile(IdolTile.Of("shizuka").SetXY(2, 0));
        
        Thread.Sleep(1);
        
        _theaterView.Verify(x=>x.GotScore(new List<string?> {"shizuka", "mirai"}));
        
        theater.AddIdolTile(IdolTile.Of("tsubasa").SetXY(-1, 0));
        theater.AddIdolTile(IdolTile.Of("tsubasa").SetXY(-1, 1));
        Thread.Sleep(1);
        
        _theaterView.Verify(x=>x.GotScore(new List<string?> {"tsubasa", "mirai", "shizuka"}));
    }
}