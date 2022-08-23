using Scripts.Player;
using Scripts.Theater;

public interface IPlayerInfoView
{
    void PlayerScoreChanged(int i);
    void SetPlayer(Player? player);
    void SongCardAddedInHand(SongCard songCard);
}