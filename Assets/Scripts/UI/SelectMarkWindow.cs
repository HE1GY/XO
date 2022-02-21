using System;
using GameLogic;
using Utilities.Events;

namespace UI
{
    public class SelectMarkWindow : SelectWindow
    {
        private new void OnEnable()
        {
            EventsControllerXo.AddListener<bool>(EventsTypeXo.SelectMode,OnSelectedMode);
            base.OnEnable();
        }

        private void OnDisable()
        {
            EventsControllerXo.RemoveListener<bool>(EventsTypeXo.SelectMode,OnSelectedMode);
        }


        protected override void OnFirstButtonClick()
        {
            EventsControllerXo.Broadcast<bool>(EventsTypeXo.SelectMark, true);
            TurnOff();
        }

        protected override void OnSecondButtonClick()
        {
            EventsControllerXo.Broadcast<bool>(EventsTypeXo.SelectMark, false);
            TurnOff();
        }

        private void OnSelectedMode(bool playersPlaying)
        {
            if (!playersPlaying)
            {
                TurnOn();
            }
        }
    }
}