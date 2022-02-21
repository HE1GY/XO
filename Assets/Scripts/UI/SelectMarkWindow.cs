using System;
using DefaultNamespace;
using GameLogic;
using Utilities.Events;

namespace UI
{
    public class SelectMarkWindow : SelectWindow
    {
        private GameSetup _receivedGameSetup;
        private new void OnEnable()
        {
            EventsControllerXo.AddListener<GameSetup>(EventsTypeXo.SelectMode,OnSelectedMode);
            base.OnEnable();
        }

        private void OnDisable()
        {
            EventsControllerXo.RemoveListener<GameSetup>(EventsTypeXo.SelectMode,OnSelectedMode);
        }


        protected override void OnFirstButtonClick()
        {
            EventsControllerXo.Broadcast<GameSetup>(EventsTypeXo.SelectMark, _receivedGameSetup);
            TurnOff();
        }

        protected override void OnSecondButtonClick()
        {
            EventsControllerXo.Broadcast<GameSetup>(EventsTypeXo.SelectMark, GetGameSetup(Mark.O));
            TurnOff();
        }

        private void OnSelectedMode(GameSetup gameSetup)
        {
            _receivedGameSetup = gameSetup;

            if (!gameSetup.IsTwoPlayer)
            {
                TurnOn();
            }
            else
            {
                EventsControllerXo.Broadcast<GameSetup>(EventsTypeXo.SelectMark, _receivedGameSetup);
            }
        }
        
        private GameSetup GetGameSetup(Mark mark)
        {
            _receivedGameSetup.PlayerMark = mark;
            return _receivedGameSetup;
        }
    }
}