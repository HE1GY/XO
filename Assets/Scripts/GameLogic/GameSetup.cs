using GameLogic;

namespace DefaultNamespace
{
    public class GameSetup
    {
        public Mark PlayerMark { get; set; }

        public bool IsTwoPlayer { get; set; }

        public GameSetup(bool isTwoPlayer, Mark playerMark=Mark.X)
        {
            IsTwoPlayer = isTwoPlayer;
            PlayerMark = playerMark;
        }
    }
}