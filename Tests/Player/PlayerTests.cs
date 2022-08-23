using Moq;

namespace Tests.Player;
using Scripts.Player;
public class PlayerTests
{
    private Mock<IPlayerInfoView> _playerInfoView;

    [SetUp]
    public void Setup()
    {
        _playerInfoView = new Mock<IPlayerInfoView>();
    }

    [Test]
    public void Test_PlayerInitialized()
    {
        var player = new Player();
        var playerPresenter = new PlayerPresenter(_playerInfoView.Object);
        
        playerPresenter.InitializeWithPlayer(player);
        
        Thread.Sleep(1);
        
        _playerInfoView.Verify(x=>x.SetPlayer(player));
    }
    
    [Test]
    public void Test_Player_ScoreChanged()
    {
        var player = new Player();
        var playerPresenter = new PlayerPresenter(_playerInfoView.Object);
        
        playerPresenter.InitializeWithPlayer(player);

        player.Score.Value += 1;
        
        Thread.Sleep(1);
        
        _playerInfoView.Verify(x=>x.PlayerScoreChanged(player.Score.Value));
    }

    [Test]
    public void Test_Player_Got_SongCard()
    {
        var player = new Player();
        var playerPresenter = new PlayerPresenter(_playerInfoView.Object);
        
        playerPresenter.InitializeWithPlayer(player);
        var songCard = SongCard.Of("Absolute Run", "Strawberry Pop Moon",
            new List<string> { "mirai", "shizuka", "tsubasa" });

        player.SongCardHand.Add(songCard);
        
        Thread.Sleep(1);
        
        _playerInfoView.Verify(x=>x.SongCardAddedInHand(songCard));
    }
}