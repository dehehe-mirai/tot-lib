using Scripts.Player;

public interface IPlayerInfoView
{
    void PlayerScoreChanged(int i);
    void SetPlayer(Player player);
    void SongCardAddedInHand(SongCard songCard);
}