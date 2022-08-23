using Moq;
using Scripts;
using Scripts.Office;
using Scripts.Theater;

namespace Tests;

public class OfficeTests
{
    private Mock<IOfficeView> _officeView;
    private List<IdolTile> _allIdolTileInfo;
    private List<SongCard> _allSongTileInfo;

    [SetUp]
    public void Setup()
    {
        _officeView = new Mock<IOfficeView>();
        
        _allIdolTileInfo = new List<IdolTile>();
        
        _allIdolTileInfo.Add(IdolTile.Of("mirai"));
        _allIdolTileInfo.Add(IdolTile.Of("shizuka"));
        _allIdolTileInfo.Add(IdolTile.Of("tsubasa"));
        _allIdolTileInfo.Add(IdolTile.Of("kotoha"));
        _allIdolTileInfo.Add(IdolTile.Of("kana"));
        _allIdolTileInfo.Add(IdolTile.Of("julia"));
        
        _allSongTileInfo = new List<SongCard>();
        
        _allSongTileInfo.Add(SongCard.Of("Absolute Run", "Strawberry Pop Moon",new List<string?> {"mirai", "shizuka", "tsubasa"}));
        _allSongTileInfo.Add(SongCard.Of("Episode Tiara", "Star Elements",new List<string?> {"mirai", "kotoha", "kana"}));
        _allSongTileInfo.Add(SongCard.Of("Give Me Metaphor", "Star Elements",new List<string?> {"mirai", "kotoha", "kana"}));
        _allSongTileInfo.Add(SongCard.Of("Harmonics", "D/Zeal",new List<string?> {"shizuka", "julia"}));

    }

    [Test]
    public void Test_OfficeInitialized()
    {
        var office = new Office();
        var officePresenter = new OfficePresenter(_officeView.Object)
        {
            Office = office
        };
        
        office.SetInitialIdolTile(_allIdolTileInfo);
        office.SetInitialSongCard(_allSongTileInfo);

        foreach (var idolTile in _allIdolTileInfo)
        {
            _officeView.Verify(x=>x.IdolDeckAdded(idolTile));    
        }
        
        foreach (var song in _allSongTileInfo)
        {
            _officeView.Verify(x=>x.SongDeckAdded(song));
        }
    }

    [Test]
    public void Test_OpenIdolTileAdded()
    {
        var office = new Office();
        var officePresenter = new OfficePresenter(_officeView.Object)
        {
            Office = office
        };
        
        office.SetInitialIdolTile(_allIdolTileInfo);
        office.SetInitialSongCard(_allSongTileInfo);

        var firstThreeIdols = office.IdolDeck.Take(3).ToList();
            
        office.OpenIdolTileFromDeck(4);
        
        Assert.That(office.IdolDeck.Intersect(firstThreeIdols), Is.Empty);
        Assert.That(firstThreeIdols.Intersect(office.OpenedIdolTiles), Is.EquivalentTo(firstThreeIdols));
    }  
    
    [Test]
    public void Test_OpenIdolTileAdded_RemoveTileAdds()
    {
        var office = new Office();
        var officePresenter = new OfficePresenter(_officeView.Object)
        {
            Office = office
        };
        
        office.SetInitialIdolTile(_allIdolTileInfo);
        office.SetInitialSongCard(_allSongTileInfo);

        var removed = IdolTile.Of("removed");
        office.RemovedIdolTiles.Add(removed);

        var allIdolsInDeck = office.IdolDeck.Take(office.IdolDeck.Count).ToList();
            
        office.OpenIdolTileFromDeck(office.IdolDeck.Count + 1);
        
        Assert.That(office.IdolDeck.Intersect(allIdolsInDeck), Is.Empty);
        Assert.That(allIdolsInDeck.Intersect(office.OpenedIdolTiles), Is.EquivalentTo(allIdolsInDeck));
        
        Assert.Contains(removed, office.OpenedIdolTiles);
        Assert.That(office.RemovedIdolTiles, Is.Empty);
    }
}