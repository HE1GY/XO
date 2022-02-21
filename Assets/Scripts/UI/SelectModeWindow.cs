using DefaultNamespace;
using Utilities.Events;

namespace UI
{
    public class SelectModeWindow : SelectWindow
    {
        private void Start()
        {
            TurnOn();
        }
        protected override void OnFirstButtonClick()
        {
            EventsControllerXo.Broadcast<GameSetup>(EventsTypeXo.SelectMode, GetGameSetup(isTwoPlayer:true));
            TurnOff();
        }

        protected override void OnSecondButtonClick()
        {
            EventsControllerXo.Broadcast<GameSetup>(EventsTypeXo.SelectMode, GetGameSetup(isTwoPlayer:false));
            TurnOff();
        }
        private GameSetup GetGameSetup(bool isTwoPlayer)
        {
            return new GameSetup(isTwoPlayer);
        }
    }
}